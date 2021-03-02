using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PlayCollector.Business;
using PlayCollector.Business.Model;

namespace PlayCollector.Test
{

    [TestClass]
    public class ImportTest
    {

        public ImportTest()
        {
            log4net.Config.XmlConfigurator.Configure();
        }


        [TestMethod]
        public void Import()
        {

            string fileToImport = Properties.Settings.Default.ExcelFileToImport;

            ImportManager manager = new ImportManager(fileToImport);
            manager.ImportStarted += Manager_ImportStarted;
            manager.ImportFinished += Manager_ImportFinished;
            manager.ImportError += Manager_ImportError;
                       

            manager.StartImport();
                       
        }

        private void Manager_ImportError(object sender, ImportErrorEventArgs e)
        {
            Debug.WriteLine(string.Format("Import error {0}",e.Exception.Message));
        }

        private void Manager_ImportStarted(object sender, EventArgs e)
        {
            Debug.WriteLine("Import started");
        }

        private void Manager_ImportFinished(object sender, EventArgs e)
        {
            Debug.WriteLine("Import finished");
        }


    }
}
