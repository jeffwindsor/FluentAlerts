 using System.Collections.Generic;
using System.Linq;
using FluentAlerts.Transformers.Formatters;
using FluentAlerts.Transformers.Strategies;
using FluentAlerts.Transformers.TypeInformers;

namespace FluentAlerts.Transformers
{
    public class NameTypeValueRowTransformer : BaseRowTransformer 
    {
        public NameTypeValueRowTransformer(ITransformStrategy strategy,
            ITypeInfoSelector selector, 
            IObjectFormatter<string> formatter):base(strategy,selector,formatter){}
        
        protected override IEnumerable<object[]> GetPropertyRowValues(object o, IEnumerable<string> objectMemberPath)
        {
            return from t in GetPropertyInfoValues(o, objectMemberPath)
                   orderby t.Info.Name
                   select new[]
                       {
                           t.Info.Name,
                           DefaultToStringFormatter.PrettyTypeName(t.Info.PropertyType),
                           t.Value
                       };
        }

        protected override IEnumerable<object[]> GetFieldRowValues(object o, IEnumerable<string> objectMemberPath)
        {
            return from t in GetFieldInfoValues(o, objectMemberPath)
                   orderby t.Info.Name
                   select new[]
                       {
                           t.Info.Name,
                           DefaultToStringFormatter.PrettyTypeName(t.Info.FieldType),
                           t.Value
                       };
        }
    }
}
