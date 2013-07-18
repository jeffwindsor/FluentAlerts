using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace FluentAlerts.Transformers.TypeInformers
{
    public class TypeInfo 
    {
        public TypeInfo()
        {
            PropertyInfos = Enumerable.Empty<PropertyInfo>();
            FieldInfos = Enumerable.Empty<FieldInfo>();
        }

        public IEnumerable<PropertyInfo> PropertyInfos { get; set; }
        public IEnumerable<FieldInfo> FieldInfos { get; set; }
    }
}
