﻿using PlayCollector.Business.Model;
using PlayCollector.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayCollector.Business
{
    public interface IThemeRepository : IGenericRepository<Theme>
    {
        Theme SelectByName(string name);
        Task<Theme> SelectByNameAsync(string name);

    }
}
