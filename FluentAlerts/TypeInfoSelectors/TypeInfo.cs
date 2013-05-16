using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace FluentAlerts.TypeInfoSelectors
{
    public class TypeInfo 
    {
        public TypeInfo()
        {
            PropertyInfos = Enumerable.Empty<PropertyInfo>();
            FieldInfos = Enumerable.Empty<FieldInfo>();
            MethodInfos = Enumerable.Empty<MethodInfo>();
            EventInfos = Enumerable.Empty<EventInfo>();
        }

        public IEnumerable<PropertyInfo> PropertyInfos { get; set; }
        public IEnumerable<FieldInfo> FieldInfos { get; set; }
        public IEnumerable<MethodInfo> MethodInfos { get; set; }
        public IEnumerable<EventInfo> EventInfos { get; set; }
    }
}
