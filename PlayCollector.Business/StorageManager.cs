using PlayCollector.Business.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayCollector.Business
{



    public class StorageManager
    {
        private IStorageRepository _IRepository;
        

        public StorageManager()
        {
            _IRepository = new StorageRepository();
           
        }


        public ICollection<Storage> Select()
        {
            return _IRepository.Select();
        }
        public async Task<ICollection<Storage>> SelectAsync()
        {
            return await _IRepository.SelectAsync();
        }

        public Storage SelectById(object key)
        {
            return _IRepository.SelectById(key);
        }
        public async Task<Storage> SelectByIdAsync(object key)
        {
            return await _IRepository.SelectByIdAsync(key);
        }


        public Storage SelectByName(string name)
        {
            return _IRepository.SelectByName(name);
        }
        public async Task<Storage> SelectByNameAsync(string name)
        {
            return await _IRepository.SelectByNameAsync(name);
        }



        public Storage Insert(Storage item)
        {
            return _IRepository.Insert(item);
        }

        public async Task<Storage> InsertAsync(Storage item)
        {
            return await _IRepository.InsertAsync(item);
        }


        public Storage Update(Storage item, long key)
        {
            return _IRepository.Update(item, key);
        }

        public async Task<Storage> UpdateAsync(Storage item, long key)
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
