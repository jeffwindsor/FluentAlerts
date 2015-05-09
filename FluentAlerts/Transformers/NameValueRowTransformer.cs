using System.Collections.Generic;
using System.Linq;
using FluentAlerts.Formatters;
using FluentAlerts.TypeInformers;

namespace FluentAlerts.Transformers
{
    public class NameValueRowTransformer : BaseRowTransformer 
    {
        public NameValueRowTransformer(ITransformStrategy transformStrategy,
                                       ITypeInformerSelector typeInformerSelector,
                                       IValueFormatter<string> formatter,
                                       IAlertBuilderFactory alertBuilderFactory)
            : base(transformStrategy, typeInformerSelector, formatter, alertBuilderFactory)
        {}

        protected override IEnumerable<object[]> GetPropertyRowValues(object o, TypeInformer typeInfo, MemberPath objectMemberPath)
        {
            //Return a list of name value pairs (as object arrays)
            return from pi in typeInfo.PropertyInfos
                   select new[]
                       {
                           pi.Name, 
                           Route(typeInfo.GetValue(pi, o), objectMemberPath.Extend(pi.Name))
                       };
        }

        protected override IEnumerable<object[]> GetFieldRowValues(object o, TypeInformer typeInfo, MemberPath objectMemberPath)
        {
            //Return a list of name value pairs (as object arrays)
            return from fi in typeInfo.FieldInfos
                   select new[]
                       {
                           fi.Name, 
                           Route(typeInfo.GetValue(fi, o), objectMemberPath.Extend(fi.Name))
                       };
        }
    }
}
