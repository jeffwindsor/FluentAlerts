using System.Collections.Generic;
using System.Linq;
using FluentAlerts.Formatters;
using FluentAlerts.TypeInformers;

namespace FluentAlerts.Transformers
{
    public abstract class BaseRowTransformer: BaseTransformer<string>
    {
        protected BaseRowTransformer(ITransformStrategy transformStrategy,
                                     ITypeInformerSelector typeInformerSelector,
                                     IValueFormatter<string> formatter,
                                     IAlertBuilderFactory alertBuilderFactory)
            : base(transformStrategy, typeInformerSelector, formatter, alertBuilderFactory){}

        protected override object InnerTransform(object o, MemberPath objectMemberPath)
        {
            //Create Alert with a property and fields section containing name-values pairs  
            var result = AlertBuilderFactory.Create(Formatter.FormatAsTitle(o, objectMemberPath));
            var typeInfo = TypeInformerSelector.Find(o, objectMemberPath);

            //Add property section 
            var ps = GetPropertyRowValues(o, typeInfo, objectMemberPath).ToList();
            if (ps.Any())
                result.With(ps);  //.WithEmphasized("Properties")

            //Add fields section
            var fs = GetFieldRowValues(o, typeInfo, objectMemberPath).ToList();
            if (fs.Any())
                result.With(fs); //.WithEmphasized("Fields")

            //build alert and return
            return result.ToAlert();
        }

        protected abstract IEnumerable<object[]> GetPropertyRowValues(object o, TypeInformer typeInfo, MemberPath objectMemberPath);
        protected abstract IEnumerable<object[]> GetFieldRowValues(object o, TypeInformer typeInfo, MemberPath objectMemberPath);
    }
}
 