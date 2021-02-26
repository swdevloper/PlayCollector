using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PlayCollector.Helper
{
    public static class EntityHelper
    {

        public static void SetObjectProperty(string propertyName, DateTime valueToSet, object obj)
        {
            PropertyInfo propertyInfo = obj.GetType().GetProperty(propertyName);
            if(propertyInfo!=null)
            {
                propertyInfo.SetValue(obj, valueToSet);
            }

        }


    }
}
