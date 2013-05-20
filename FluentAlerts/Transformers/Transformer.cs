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
        bool IsTransformRequired(object o);
        IAlert Transform(object o);
        TResult Format(object o);
    }

    public abstract class BaseTransformer<TResult>: ITransformer<TResult> 
    {
        private readonly string[] NO_PATH = new string[] { };

        private readonly ITypeInformer _informer;
        private readonly ITransformStrategy _strategy;
        private readonly IObjectFormatter<TResult> _formatter;

        protected BaseTransformer(ITransformStrategy strategy, ITypeInformer informer, IObjectFormatter<TResult> formatter)
        {
            _informer = informer;
            _strategy = strategy;
            _formatter = formatter;
        }

        #region ITransformer
        public bool IsTransformRequired(object o)
        {
            //pass through to injected strategy
            return _strategy.IsTransformRequired(o, NO_PATH);
        }

        public TResult Format(object o)
        {
            return _formatter.Format(o, NO_PATH);
        }
        
        public IAlert Transform(object o)
        {
            return Transform(o, NO_PATH);
        }
        #endregion

        protected abstract IAlert Transform(object o, IEnumerable<string> objectMemberPath);

        protected IEnumerable<InfoValue<PropertyInfo>> GetPropertyInfoValues(object o, IEnumerable<string> objectMemberPath)
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
            return (source ?? Enumerable.Empty<string>()).Concat(new[] { appendElement }).ToArray();
        }

        protected class InfoValue<TInfo>
        {
            public TInfo Info;
            public object Value;
        }
    }
}
