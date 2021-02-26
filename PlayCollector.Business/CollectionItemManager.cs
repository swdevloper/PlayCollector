using PlayCollector.Business.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayCollector.Business
{
    public class RepositoryEventArgs : EventArgs
    {
        public string EntitySetname { get; set; }
        public string KeyValue { get; set; }
    }


    public class CollectionItemManager
    {
        private ICollectionItemRepository _IRepository;
        

        public CollectionItemManager()
        {
            _IRepository = new CollectionItemRepository();
            _IRepository.InsertEvent += _IRepository_InsertEvent;

            
        }

        private void _IRepository_InsertEvent(object sender, Repository.RepositoryEventArgs e)
        {
            var par = e;
        }

        public ICollection<CollectionItem> Select()
        {
            return _IRepository.Select();
        }
        public async Task<ICollection<CollectionItem>> SelectAsync()
        {
            return await _IRepository.SelectAsync();
        }

        public CollectionItem SelectById(object key)
        {
            return _IRepository.SelectById(key);
        }
        public async Task<CollectionItem> SelectByIdAsync(object key)
        {
            return await _IRepository.SelectByIdAsync(key);
        }


        public CollectionItem Insert(CollectionItem item)
        {
            return _IRepository.Insert(item);
        }

        public async Task<CollectionItem> InsertAsync(CollectionItem item)
        {
            return await _IRepository.InsertAsync(item);
        }


        public CollectionItem Update(CollectionItem item, long key)
        {
            return _IRepository.Update(item, key);
        }

        public async Task<CollectionItem> UpdateAsync(CollectionItem item, long key)
        {
            return await _IRepository.UpdateAsync(item, key);
        }


        public void Delete(long key)
        {
            _IRepository.Delete(key);
        }

        public async Task DeleteAsync(long key)
        {
            await _IRepository.DeleteAsync(key);
        }





    }
}
