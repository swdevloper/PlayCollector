using ExcelDataReader;
using log4net;
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
            GetExcelData(_fileToImport);


            //Alle Tabelle aus der Excel Mappe durchlaufen und Daten importieren


            //Ende protokollieren und event auslösen
            log.Info(string.Format("{0}: Import finished", MethodBase.GetCurrentMethod().Name));
            OnImportFinished(new EventArgs());
        }



        //Methode liest Daten aus einer Excel Mappe und liefer die Daten als DataSet zurück
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

            }
            catch (IOException exIo)
            {
                log.Error(string.Format("{0}: Import finished", MethodBase.GetCurrentMethod().Name), exIo);

            }
            catch (Exception ex)
            {
                log.Error(string.Format("{0}: Import finished", MethodBase.GetCurrentMethod().Name), ex);

            }




            return excelData;


        }

        //Importiert Daten aus Excel Themen Tabelle und aktualisiert Daten bzw fügt Daten in EntitySet ein
        private void ImportTheme()
        {

        }

        //Importiert Daten aus Excel Storage Tabelle und aktualisiert Daten bzw fügt Daten in EntitySet ein
        private void ImportStorage()
        {

        }


        //Importiert Daten aus Excel Condition Tabelle und aktualisiert Daten bzw fügt Daten in EntitySet ein
        private void ImportCondition()
        {

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
    }
}
