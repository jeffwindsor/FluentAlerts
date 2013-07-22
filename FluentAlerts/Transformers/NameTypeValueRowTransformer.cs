 using System.Collections.Generic;
using System.Linq;
 using FluentAlerts.Formatters;
 using FluentAlerts.TypeInformers;

namespace FluentAlerts.Transformers
{
    public class NameTypeValueRowTransformer : BaseRowTransformer 
    {
        public NameTypeValueRowTransformer(ITransformStrategy transformStrategy,
                                           ITypeInfoSelector typeInfoSelector,
                                           IValueFormatter<string> formatter,
                                           IAlertBuilderFactory alertBuilderFactory)
            : base(transformStrategy, typeInfoSelector, formatter, alertBuilderFactory)
        {
        }

        protected override IEnumerable<object[]> GetPropertyRowValues(object o, TypeInfo typeInfo)
        {
            //Return a list of name value pairs (as object arrays)
            return from pi in typeInfo.PropertyInfos
                   select new[]
                       {
                           pi.Name, 
                           Formatter.Format(pi.PropertyType),
                           typeInfo.GetValue(pi, o)
                       };
        }

        protected override IEnumerable<object[]> GetFieldRowValues(object o, TypeInfo typeInfo)
        {
            //Return a list of name value pairs (as object arrays)
            return from fi in typeInfo.FieldInfos
                   select new[]
                       {
                           fi.Name, 
                           Formatter.Format(fi.FieldType),
                           typeInfo.GetValue(fi, o)
                       };
        }

    }
}
