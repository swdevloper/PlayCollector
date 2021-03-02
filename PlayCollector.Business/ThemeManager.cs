using PlayCollector.Business.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayCollector.Business
{



    public class ThemeManager
    {
        private IThemeRepository _IRepository;
        

        public ThemeManager()
        {
            _IRepository = new ThemeRepository();
           
        }


        public ICollection<Theme> Select()
        {
            return _IRepository.Select();
        }
        public async Task<ICollection<Theme>> SelectAsync()
        {
            return await _IRepository.SelectAsync();
        }

        public Theme SelectById(object key)
        {
            return _IRepository.SelectById(key);
        }
        public async Task<Theme> SelectByIdAsync(object key)
        {
            return await _IRepository.SelectByIdAsync(key);
        }



        public Theme Insert(Theme item)
        {
            return _IRepository.Insert(item);
        }

        public async Task<Theme> InsertAsync(Theme item)
        {
            return await _IRepository.InsertAsync(item);
        }


        public Theme Update(Theme item, long key)
        {
            return _IRepository.Update(item, key);
        }

        public async Task<Theme> UpdateAsync(Theme item, long key)
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
