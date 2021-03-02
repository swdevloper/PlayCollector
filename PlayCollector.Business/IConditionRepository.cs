﻿using PlayCollector.Business.Model;
using PlayCollector.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayCollector.Business
{
    public interface IConditionRepository : IGenericRepository<Condition>
    {
        Condition SelectByName(string name);
        Task<Condition> SelectByNameAsync(string name);

    }
}
