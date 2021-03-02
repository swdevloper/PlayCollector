using PlayCollector.Business.Model;
using PlayCollector.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayCollector.Business
{
    public interface IStorageRepository : IGenericRepository<Storage>
    {
        Storage SelectByName(string name);
        Task<Storage> SelectByNameAsync(string name);

    }
}
