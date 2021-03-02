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

            

            manager.StartImport();
            

            
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
