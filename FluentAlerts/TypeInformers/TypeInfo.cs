using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace FluentAlerts.TypeInformers
{
    public class TypeInfo 
    {
        public TypeInfo()
        {
            TypeName = string.Empty;
            ClearProperties();
            ClearFields();
        }

        public string TypeName { get; set; }
        public IEnumerable<PropertyInfo> PropertyInfos { get; set; }
        public IEnumerable<FieldInfo> FieldInfos { get; set; }

        public void ClearProperties()
        {
            PropertyInfos = Enumerable.Empty<PropertyInfo>();
        }
        public void ClearFields()
        {
            FieldInfos = Enumerable.Empty<FieldInfo>();
        }

        public object GetValue(PropertyInfo info, object o)
        {
            try
            {
                return info.GetValue(o, null);
            }
            catch (Exception)
            {
                return "No Value";
            }
        }

        public object GetValue(FieldInfo info, object o)
        {
            try
            {
                return info.GetValue(o);
            }
            catch (Exception)
            {
                return "No Value";
            }
        }
    }
}
