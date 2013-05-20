using System.Collections.Generic;
using FluentAlerts.Transformers.Formatters;
using FluentAlerts.Transformers.Strategies;
using FluentAlerts.Transformers.TypeInformers;

namespace FluentAlerts.Transformers
{
    public abstract class BaseRowTransformer: BaseTransformer<string> 
    {
        protected BaseRowTransformer(ITransformStrategy strategy,
            ITypeInformer informer, 
            IObjectFormatter<string> formatter):base(strategy,informer,formatter){}

        protected override IAlert Transform(object o, IEnumerable<string> objectMemberPath)
        {
            //TODO: use formatter to get name 
            //Create Alert with a property and fields section containing name-values pairs  
            return Alerts.Create( o.GetType().Name)
                .WithHeader("Properties")
                .WithRows(GetPropertyRowValues(o, objectMemberPath))
                .WithHeader("Fields")
                .WithRows(GetFieldRowValues(o, objectMemberPath))
                .ToAlert();
        }

        protected abstract IEnumerable<object[]> GetPropertyRowValues(object o, IEnumerable<string> objectMemberPath);
        protected abstract IEnumerable<object[]> GetFieldRowValues(object o, IEnumerable<string> objectMemberPath);
    }
}
