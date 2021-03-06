﻿using PlayCollector.Business.Model;
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
    public class ThemeRepository : GenericRepository<Theme, playcollectorDb>, IThemeRepository
    {

        public Theme SelectByName(string name)
        {
            return this.DbSet.Where(item => item.Name == name).FirstOrDefault(); 
        }

        public async Task<Theme> SelectByNameAsync(string name)
        {
            return await this.DbSet.Where(item => item.Name == name).FirstOrDefaultAsync();
        }
    }
}
