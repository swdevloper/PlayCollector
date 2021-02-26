using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayCollector.Repository
{
    public class RepositoryEventArgs: EventArgs
    {
        public string EntityName { get; }
        public string KeyValue { get; }


        public RepositoryEventArgs(string entityName, string keyValue)
        {
            EntityName = entityName;
            KeyValue = keyValue;
        }
    }
}
