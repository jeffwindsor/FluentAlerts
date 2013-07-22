using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace FluentAlerts.Formatters
{
    public class DefaultValueToStringFormatter : BaseValueFormatter<string>
    {
        public DefaultValueToStringFormatter()
        {
            //Base rule: all objects return pretty type name
            FormatRules.Add((o, path) => true,
                            (o, path) =>
                                {
                                    //NULL
                                    if(o == null) 
                                        return "Null";

                                    //Exceptions
                                    if (o.IsFundamental())
                                        return o.ToString();
                                    
                                    //Otherwise Pretty Name
                                    return PrettyTypeName(o.GetType());
                                });

            //Base Title rule: all objects return pretty type name
            FormatAsTitleRules.Add((o, path) => true,
                                   (o, path) => (o == null) ? "Null" : PrettyTypeName(o.GetType()));
        }

        public override string Format(Type type)
        {
            return PrettyTypeName(type);
        }

        internal static string PrettyTypeName(Type type)
        {
            var typeName = type.Name;
            var genericArguments = type.GetGenericArguments();
            if (!type.GetGenericArguments().Any()) return typeName;


            var baseName = typeName.Substring(0, typeName.IndexOf("`"));
            var genericNames = genericArguments.Select(PrettyTypeName);
            var genericsNames = String.Join(",", genericNames);
            return string.Format("{0}<{1}>", baseName, genericsNames);
        }

    }
}
