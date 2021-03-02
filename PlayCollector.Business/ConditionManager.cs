using PlayCollector.Business.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayCollector.Business
{



    public class ConditionManager
    {
        private IConditionRepository _IRepository;
        

        public ConditionManager()
        {
            _IRepository = new ConditionRepository();
           
        }


        public ICollection<Condition> Select()
        {
            return _IRepository.Select();
        }
        public async Task<ICollection<Condition>> SelectAsync()
        {
            return await _IRepository.SelectAsync();
        }

        public Condition SelectById(object key)
        {
            return _IRepository.SelectById(key);
        }
        public async Task<Condition> SelectByIdAsync(object key)
        {
            return await _IRepository.SelectByIdAsync(key);
        }


        public Condition SelectByName(string name)
        {
            return _IRepository.SelectByName(name);
        }
        public async Task<Condition> SelectByNameAsync(string name)
        {
            return await _IRepository.SelectByNameAsync(name);
        }



        public Condition Insert(Condition item)
        {
            return _IRepository.Insert(item);
        }

        public async Task<Condition> InsertAsync(Condition item)
        {
            return await _IRepository.InsertAsync(item);
        }


        public Condition Update(Condition item, long key)
        {
            return _IRepository.Update(item, key);
        }

        public async Task<Condition> UpdateAsync(Condition item, long key)
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
