using System;
using System.Collections;
using System.Linq;

namespace FluentAlerts.Transformers.Formatters
{
    public class DefaultToStringFormatter : BaseObjectFormatter<string>
    {
        public DefaultToStringFormatter()
        {
            //Base rule: all objects return pretty type name
            FormatRules.Add((o, path) => true,
                            (o, path) =>
                                {
                                    //NULL
                                    if(o == null) return "Null";
                                    
                                    //Collection Type Name with Count
                                    if (o is ICollection)
                                        return string.Format("{0} [{1}]", PrettyTypeName(o.GetType()),
                                                             ((ICollection)o).Count); 
                                    
                                    //Otherwise value to string
                                    return o.ToString();
                                });

            //Base rule: all objects return pretty type name
            FormatAsTitleRules.Add((o, path) => true,
                                   (o, path) => (o == null) ? "Null" : PrettyTypeName(o.GetType()));
        }
         
        internal static string PrettyTypeName(Type type)
        {
            var genericArguments = type.GetGenericArguments();
            var typeName = type.Name;

            if (!type.GetGenericArguments().Any()) return typeName;

            var unmangledName = typeName.Substring(0, typeName.IndexOf("`"));
            return string.Format("{0}<{1}>", unmangledName, String.Join(",", genericArguments.Select(PrettyTypeName)));
        }

    }
}
