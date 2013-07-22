using System.Collections.Generic;
using System.Linq;
using FluentAlerts.Formatters;
using FluentAlerts.TypeInformers;

namespace FluentAlerts.Transformers
{
    public abstract class BaseRowTransformer: BaseTransformer<string>
    {
        private readonly IAlertBuilderFactory _alertBuilderFactory;
        protected BaseRowTransformer(ITransformStrategy transformStrategy,
                                     ITypeInfoSelector typeInfoSelector,
                                     IValueFormatter<string> formatter,
                                     IAlertBuilderFactory alertBuilderFactory)
            : base(transformStrategy, typeInfoSelector, formatter)
        {
            _alertBuilderFactory = alertBuilderFactory;
        }

        protected override IAlert TransformToAlertAtPath(object o, MemberPath objectMemberPath)
        {
            //Create Alert with a property and fields section containing name-values pairs  
            var result = _alertBuilderFactory.Create(Formatter.FormatAsTitle(o, objectMemberPath));
            var typeInfo = TypeInfoSelector.Find(o, objectMemberPath);

            //Add property section 
            var ps = GetPropertyRowValues(o, typeInfo).ToList();
            if (ps.Any())
                result.With(ps);  //.WithEmphasized("Properties")

            //Add fields section
            var fs = GetFieldRowValues(o, typeInfo).ToList();
            if (fs.Any())
                result.With(fs); //.WithEmphasized("Fields")

            //build alert and return
            return result.ToAlert();
        }

        protected abstract IEnumerable<object[]> GetPropertyRowValues(object o,TypeInfo typeInfo);
        protected abstract IEnumerable<object[]> GetFieldRowValues(object o, TypeInfo typeInfo);
    }
}
 