using System.Collections.Generic;
using FluentAlerts.Transformers.Strategies;
using FluentAlerts.Transformers.TypeFormatters;
using FluentAlerts.Transformers.TypeInformers;

namespace FluentAlerts.Transformers
{
    public abstract class BaseRowTransformer: BaseTransformer 
    {
        protected BaseRowTransformer(ITransformStrategy strategy,
            ITypeInformer informer, 
            ITypeFormatter<string> formatter):base(strategy,informer,formatter){}

        public override IAlert Transform(object o, IEnumerable<string> objectMemberPath)
        {
            //Establish object's type
            var type = o.GetType();

            //Create Alert with a property and fields section containing name-values pairs  
            return Alerts.Create(type.Name)
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
