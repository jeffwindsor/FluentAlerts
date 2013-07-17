using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FluentAlerts.Transformers.Formatters;
using FluentAlerts.Transformers.Strategies;
using FluentAlerts.Transformers.TypeInformers;

namespace FluentAlerts.Transformers
{
    //Allows for the transformation of objects to an alert
    public interface ITransformer<out TResult>
    {
        IAlert Transform(IAlert alert);
        object Transform(object o);
    }

    public abstract class BaseTransformer<TResult> : ITransformer<TResult>
    {
        private readonly string[] EMPTY_PATH = new string[] {};

        private readonly ITypeInfoSelector _selector;
        private readonly ITransformStrategy _strategy;
        private readonly IObjectFormatter<TResult> _formatter;

        protected BaseTransformer(ITransformStrategy strategy, ITypeInfoSelector selector,
                                  IObjectFormatter<TResult> formatter)
        {
            _selector = selector;
            _strategy = strategy;
            _formatter = formatter;
        }

        public object Transform(object o)
        {
            //Make sure all internal alerts are fully transformed
            var alert = o as IAlert;
            if (alert != null)
                return Transform(alert);

            //Optimization, no need to transform or format result types 
             if (IsResultType(o))
                return o;

            //If the strategy requires it, transform the object to an IAlert
            //TODO: Check this transofrms objects below itself...
            if ( _strategy.IsTransformRequired(o, EMPTY_PATH))
                return Transform(o, EMPTY_PATH);
             
            //Default: Return formatted object
            return _formatter.Format(o, EMPTY_PATH);
        }

        //Recurse IAlert and tansform all group values
        public IAlert Transform(IAlert alert)
        {
            foreach (var g in alert.OfType<AlertItem>())
            {
                for (var i = 0; i < g.Values.Length; ++i)
                {
                    g.Values[i] = Transform(g.Values[i]);
                }
            }
            return alert;
        }

        protected abstract bool IsResultType(object value);

        protected TResult GetFormmatted(object o)
        {
            return _formatter.Format(o, EMPTY_PATH);
        }

        protected TResult FormatAsTitle(object o)
        {
            return _formatter.FormatAsTitle(o, EMPTY_PATH);
        }

        protected abstract IAlert Transform(object o, IEnumerable<string> objectMemberPath);

        protected IEnumerable<InfoValue<PropertyInfo>> GetPropertyInfoValues(object o, IEnumerable<string> objectMemberPath)
        {
            var typeInfo = _selector.Find(o,objectMemberPath);
            return from info in typeInfo.PropertyInfos
                   select new InfoValue<PropertyInfo>
                       {
                           Info = info,
                           Value = GetValue(() => info.Name, () => info.GetValue(o, null), objectMemberPath)
                       };
        }

        protected IEnumerable<InfoValue<FieldInfo>> GetFieldInfoValues(object o, IEnumerable<string> objectMemberPath)
        {
            var typeInfo = _selector.Find(o, objectMemberPath);
            return from info in typeInfo.FieldInfos
                   select new InfoValue<FieldInfo>
                       {
                           Info = info,
                           Value = GetValue(() => info.Name, () => info.GetValue(o), objectMemberPath)
                       };
        }

        private object GetValue(Func<string> getName, Func<object> getValue, IEnumerable<string> objectMemberPath)
        {
            try
            {
                //Check Tranform strategy and transform if required
                var name = getName();
                var value = getValue();
                var valueMemberPath = CreateCopyWithAppendedElement(objectMemberPath, name);
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
