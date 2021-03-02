using ExcelDataReader;
using log4net;
using PlayCollector.Business.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PlayCollector.Business
{
    public class ImportErrorEventArgs: EventArgs
    {
        public Exception Exception { get; set; }
    }


    public class ImportManager
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(ImportManager));

        private string _fileToImport = Properties.Settings.Default.ExcelFileToImport;

        public event EventHandler ImportStarted;
        public event EventHandler ImportFinished;
        public event EventHandler<ImportErrorEventArgs> ImportError;


        public ImportManager()
        {


        }


        public ImportManager(string fileToImport)
        {
            _fileToImport = fileToImport;

        }


        public void StartImport()
        {

            //Start protokollieren und event auslösen
            log.Info(string.Format("{0}: Import started", MethodBase.GetCurrentMethod().Name));
            OnImportStarted(new EventArgs());

            //Excel Daten lesen
            DataSet excelData = GetExcelData(_fileToImport);
            foreach (DataTable tab in excelData.Tables)
            {
                switch (tab.TableName)
                {
                    case "Theme":
                        ImportTheme(tab);
                        break;

                    case "Storage":
                        ImportStorage(tab);
                        break;

                    case "Condition":
                        ImportCondition(tab);
                        break;

                    default:
                        break;
                }



            }


            //Alle Tabelle aus der Excel Mappe durchlaufen und Daten importieren


            //Ende protokollieren und event auslösen
            log.Info(string.Format("{0}: Import finished", MethodBase.GetCurrentMethod().Name));
            OnImportFinished(new EventArgs());
        }



        //Methode liest Daten aus einer Excel Mappe und liefer die Daten als DataSet zurück
        /// <summary>
        /// Reads all data from excel file
        /// </summary>
        /// <param name="fileToImport">Path and filename of file to import</param>
        /// <returns>Dataset</returns>
        private DataSet GetExcelData(string fileToImport)
        {
            DataSet excelData = new DataSet();
            try
            {
                using (var stream = File.Open(fileToImport, FileMode.Open, FileAccess.Read))
                {
                    using (var reader = ExcelReaderFactory.CreateReader(stream))
                    {
                        excelData = reader.AsDataSet();
                    }
                }
            }
            catch (FileNotFoundException exFnf)
            {
                log.Error(string.Format("{0}: Import finished", MethodBase.GetCurrentMethod().Name),exFnf);
                OnImportError(new ImportErrorEventArgs { Exception = exFnf });
            }
            catch (IOException exIo)
            {
                log.Error(string.Format("{0}: Import finished", MethodBase.GetCurrentMethod().Name), exIo);
                OnImportError(new ImportErrorEventArgs { Exception = exIo });
            }
            catch (Exception ex)
            {
                log.Error(string.Format("{0}: Import finished", MethodBase.GetCurrentMethod().Name), ex);
                OnImportError(new ImportErrorEventArgs { Exception = ex });
            }
            return excelData;


        }


        //Importiert Daten aus Excel Themen Tabelle und aktualisiert Daten bzw fügt Daten in EntitySet ein
        private void ImportTheme(DataTable tab)
        {
            ThemeManager manager = new ThemeManager();
            for (int i = 1; i < tab.Rows.Count; i++)
            {
                DataRow dataRow = tab.Rows[i];
                string name = dataRow.ItemArray[0].ToString();
                string description = dataRow.ItemArray[1].ToString();
                Theme theme = manager.SelectByName(name);
                if (theme != null)
                {
                    theme.Description = description;
                    manager.Update(theme, theme.Id);
                }
                else
                {
                    theme = new Theme { Name = name, Description = description };
                    manager.Insert(theme);
                }
            }

        }

        //Importiert Daten aus Excel Storage Tabelle und aktualisiert Daten bzw fügt Daten in EntitySet ein
        private void ImportStorage(DataTable tab)
        {
            StorageManager manager = new StorageManager();
            for (int i = 1; i < tab.Rows.Count; i++)
            {
                DataRow dataRow = tab.Rows[i];
                string name = dataRow.ItemArray[0].ToString();
                string description = dataRow.ItemArray[1].ToString();
                Storage storage = manager.SelectByName(name);
                if (storage != null)
                {
                    storage.Description = description;
                    manager.Update(storage, storage.Id);
                }
                else
                {
                    storage = new Storage { Name = name, Description = description };
                    manager.Insert(storage);
                }
            }
        }


        //Importiert Daten aus Excel Condition Tabelle und aktualisiert Daten bzw fügt Daten in EntitySet ein
        private void ImportCondition(DataTable tab)
        {
            for (int i = 1; i < tab.Rows.Count; i++)
            {
                DataRow dataRow = tab.Rows[i];
                int id = Convert.ToInt32(dataRow.ItemArray[0]);
                string name = dataRow.ItemArray[1].ToString();
                string description = dataRow.ItemArray[2].ToString();
            }
        }



        protected virtual void OnImportStarted(EventArgs e)
        {
            EventHandler handler = ImportStarted;
            handler?.Invoke(this, e);
        }

        protected virtual void OnImportFinished(EventArgs e)
        {
            EventHandler handler = ImportFinished;
            handler?.Invoke(this, e);
        }


        protected virtual void OnImportError(ImportErrorEventArgs e)
        {
            EventHandler<ImportErrorEventArgs> handler = ImportError;
            handler?.Invoke(this, e);
        }
    }
}
