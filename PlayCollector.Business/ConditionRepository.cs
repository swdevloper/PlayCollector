using PlayCollector.Business.Model;
using PlayCollector.Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.HtmlControls;

namespace PlayCollector.Business
{
    public class ConditionRepository : GenericRepository<Condition, playcollectorDb>, IConditionRepository
    {

        public Condition SelectByName(string name)
        {
            return this.DbSet.Where(item => item.Name == name).FirstOrDefault(); 
        }

        public async Task<Condition> SelectByNameAsync(string name)
        {
            return await this.DbSet.Where(item => item.Name == name).FirstOrDefaultAsync();
        }
    }
}
