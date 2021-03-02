using PlayCollector.Business.Model;
using PlayCollector.Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayCollector.Business
{
    public class ThemeRepository : GenericRepository<Theme, playcollectorDb>, IThemeRepository
    {


    }
}
