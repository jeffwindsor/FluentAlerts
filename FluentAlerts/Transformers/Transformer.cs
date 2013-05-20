using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using FluentAlerts.Transformers.Formatters;
using FluentAlerts.Transformers.Strategies;
using FluentAlerts.Transformers.TypeInformers;

namespace FluentAlerts.Transformers
{
    //Allows for the transformation of objects to an alert
    public interface ITransformer<out TResult>
    {
        //bool IsTransformRequired(object o);
        object Transform(object o);
        //TResult Format(object o);
    }

    public abstract class BaseTransformer<TResult> : ITransformer<TResult>
    {
        private readonly string[] EMPTY_PATH = new string[] {};

        private readonly ITypeInformer _informer;
        private readonly ITransformStrategy _strategy;
        private readonly IObjectFormatter<TResult> _formatter;

        protected BaseTransformer(ITransformStrategy strategy, ITypeInformer informer,
                                  IObjectFormatter<TResult> formatter)
        {
            _informer = informer;
            _strategy = strategy;
            _formatter = formatter;
        }

        public object Transform(object o)
        {
            //Transform to IAlert or Format object based on strategy, return as an object so serilaizer can re-route
            return _strategy.IsTransformRequired(o, EMPTY_PATH)
                       ? (object) Transform(o, EMPTY_PATH)
                       : (object) _formatter.Format(o, EMPTY_PATH);
        }

        protected abstract IAlert Transform(object o, IEnumerable<string> objectMemberPath);

        protected IEnumerable<InfoValue<PropertyInfo>> GetPropertyInfoValues(object o,
                                                                             IEnumerable<string> objectMemberPath)
        {
            var typeInfo = _informer.Find(o.GetType());
            return from info in typeInfo.PropertyInfos
                   select new InfoValue<PropertyInfo>()
                       {
                           Info = info,
                           Value = GetValue(() => info.Name, () => info.GetValue(o, null), objectMemberPath)
                       };
        }

        protected IEnumerable<InfoValue<FieldInfo>> GetFieldInfoValues(object o, IEnumerable<string> objectMemberPath)
        {
            var typeInfo = _informer.Find(o.GetType());
            return from info in typeInfo.FieldInfos
                   select new InfoValue<FieldInfo>()
                       {
                           Info = info,
                           Value = GetValue(() => info.Name, () => info.GetValue(o), objectMemberPath)
                       };
        }

        private object GetValue(Func<string> name, Func<object> value, IEnumerable<string> objectMemberPath)
        {
            try
            {
                //Check Tranform strategy and transform if required
                var valueMemberPath = CreateCopyWithAppendedElement(objectMemberPath, name());
                if (_strategy.IsTransformRequired(value, valueMemberPath))
                    return Transform(value, valueMemberPath);

                return _formatter.Format(value, objectMemberPath);
            }
            catch (Exception)
            {
                return "Failed to Obtain Value";
            }

        }

        private static string[] CreateCopyWithAppendedElement(IEnumerable<string> source, string appendElement)
        {
            return (source ?? Enumerable.Empty<string>()).Concat(new[] {appendElement}).ToArray();
        }

        protected class InfoValue<TInfo>
        {
            public TInfo Info;
            public object Value;
        }
    }
}
