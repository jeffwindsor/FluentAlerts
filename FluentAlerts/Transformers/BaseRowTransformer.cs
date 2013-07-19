using System.Collections.Generic;
using FluentAlerts.Transformers.Formatters;
using FluentAlerts.Transformers.TypeInformers;

namespace FluentAlerts.Transformers
{
    public abstract class BaseRowTransformer: BaseTransformer<string>
    {
        private readonly IAlertBuilderFactory _alertBuilderFactory;
        protected BaseRowTransformer(ITransformStrategy strategy,
                                     ITypeInfoSelector selector,
                                     IObjectFormatter<string> formatter,
                                     IAlertBuilderFactory alertBuilderFactory)
            : base(strategy, selector, formatter)
        {
            _alertBuilderFactory = alertBuilderFactory;
        }

        protected override IAlert Transform(object o, IEnumerable<string> objectMemberPath)
        {
            //Create Alert with a property and fields section containing name-values pairs  
            return _alertBuilderFactory.Create(FormatAsTitle(o))
                .WithHeaderOne("Properties")
                .WithRows(GetPropertyRowValues(o, objectMemberPath))
                .WithHeaderOne("Fields")
                .WithRows(GetFieldRowValues(o, objectMemberPath))
                .ToAlert();
        }

        protected override bool IsResultType(object value)
        {
            return (value as string) != null;
        }

        protected abstract IEnumerable<object[]> GetPropertyRowValues(object o, IEnumerable<string> objectMemberPath);
        protected abstract IEnumerable<object[]> GetFieldRowValues(object o, IEnumerable<string> objectMemberPath);
    }
}
 