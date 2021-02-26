using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PlayCollector.Business;
using PlayCollector.Business.Model;

namespace PlayCollector.Test
{

    [TestClass]
    public class CollectionItemTest
    {

        public CollectionItemTest()
        {
            log4net.Config.XmlConfigurator.Configure();
        }


        [TestMethod]
        public void Insert()
        {

            CollectionItem newItem = new CollectionItem();
            newItem.Name = string.Format("CollectionItem {0}", DateTime.Now);
            newItem.Setnumber = "99999";
            

            CollectionItemManager manager = new CollectionItemManager();
            manager.Insert(newItem);
            Assert.AreNotEqual(newItem.Id, 0);
            
        }


        [TestMethod]
        public void InsertAsync()
        {
            CollectionItem newItem = new CollectionItem();
            newItem.Name = string.Format("CollectionItem Async {0}", DateTime.Now);
            newItem.Setnumber = "99999";

            CollectionItemManager manager = new CollectionItemManager();
            manager.InsertAsync(newItem).Wait();
            Assert.AreNotEqual(newItem.Id, 0);
        }


        [TestMethod]
        public void Select()
        {
            CollectionItemManager manager = new CollectionItemManager();
            var itemList = manager.Select().ToList();
            Assert.AreNotEqual(itemList.Count, 0);
        }

        [TestMethod]
        public void SelectAsync()
        {
            CollectionItemManager manager = new CollectionItemManager();
            var asyncResult = manager.SelectAsync();
            asyncResult.Wait();
            List<CollectionItem> itemList = asyncResult.Result.ToList();
            Assert.AreNotEqual(itemList.Count,0);
        }


        [TestMethod]
        public void SelectById()
        {
            CollectionItemManager manager = new CollectionItemManager();
            
            //Variante 1: Alle Datensätze lesen und erstes Element auswählen und wieder lesen
            List<CollectionItem> itemList = manager.Select().ToList();
            CollectionItem firstItem = itemList.FirstOrDefault();
            if(firstItem!=null)
            {
                CollectionItem item = manager.SelectById(firstItem.Id);
                Assert.IsNotNull(item);
            }
            else
            {
                Assert.IsNotNull(firstItem);
            }

            //Variante 2: Neues Element einfügen, ID feststellen und wieder lesen
            CollectionItem newItem = new CollectionItem();
            newItem.Name = string.Format("CollectionItem SelectById {0}", DateTime.Now);
            newItem.Setnumber = "99999";
            manager.Insert(newItem);
            Assert.AreNotEqual(newItem.Id, 0);
            if(newItem.Id!=0)
            {
                CollectionItem item = manager.SelectById(newItem.Id);
                Assert.IsNotNull(item);
            }

        }


        [TestMethod]
        public void Update()
        {
            CollectionItemManager manager = new CollectionItemManager();

            List<CollectionItem> itemList = manager.Select().ToList();
            CollectionItem firstItem = itemList.FirstOrDefault();
            if (firstItem != null)
            {
                string oldName = firstItem.Name;
                firstItem.Name = string.Format("{0} Updated",oldName);
                manager.Update(firstItem, firstItem.Id);
                CollectionItem savedItem = manager.SelectById(firstItem.Id);
                Assert.AreNotEqual(savedItem.Name, oldName);
            }
            else
            {
                Assert.IsNotNull(firstItem);
            }

        }



        [TestMethod]
        public void Delete()
        {
            CollectionItemManager manager = new CollectionItemManager();

            List<CollectionItem> itemList = manager.Select().ToList();
            CollectionItem firstItem = itemList.FirstOrDefault();
            if (firstItem != null)
            {
                long id = firstItem.Id;
                manager.Delete(id);
                CollectionItem deletedItem = manager.SelectById(id);
                Assert.IsNull(deletedItem);
            }
            else
            {
                Assert.Fail();
            }

        }

    }
}
