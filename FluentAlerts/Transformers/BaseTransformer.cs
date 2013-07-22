using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FluentAlerts.Formatters;
using FluentAlerts.TypeInformers;

namespace FluentAlerts.Transformers
{
    //Allows for the transformation of objects to an alert

    public abstract class BaseTransformer<TFormatterResult> : ITransformer
    {
        protected readonly ITypeInfoSelector TypeInfoSelector;
        protected readonly ITransformStrategy TransformStrategy;
        protected readonly IValueFormatter<TFormatterResult> Formatter;
        protected readonly Type FormatResultType = typeof (TFormatterResult);


        protected BaseTransformer(ITransformStrategy transformStrategy, ITypeInfoSelector typeInfoSelector,
                                  IValueFormatter<TFormatterResult> formatter)
        {
            TypeInfoSelector = typeInfoSelector;
            TransformStrategy = transformStrategy;
            Formatter = formatter;
        }

        public object Transform(object o)
        {
            return TransformAtPath(o, MemberPath.Empty);
        }

        private object TransformAtPath(object o, MemberPath objectMemberPath)
        {
            //TODO: Use Issue Handler, or config value
            if(o == null)
                return "No Value";

            //Optimization, no need to transform or format result types 
            if (o.GetType() == FormatResultType) 
                return o;

            //Transform an alert's alert items, leaving the alert in place for rendering
            if (o is IAlert)
            {
                foreach (var ai in (o as IAlert).OfType<AlertItem>())
                {
                    TransformAtPath(ai, objectMemberPath);
                }
                return o;
            }

            //Transform an alert item's values, leaving the alert item in place for rendering
            if (o is AlertItem)
            {
                var ai = o as AlertItem;
                for (var i = 0; i < ai.Values.Count; ++i)
                {
                    //Replace value with transformation of value
                    ai.Values[i] = TransformAtPath(ai.Values[i],objectMemberPath);
                }
                return o;
            }

            //Check any other type against transformation rules
            //  if transformation required by rules then transform object to Alert and then re-curse for sub objects
            //  otherwise just format object as output format and return
            return TransformStrategy.IsTransformRequired(o, objectMemberPath) 
                ? Transform(TransformToAlertAtPath(o, objectMemberPath)) 
                : Formatter.Format(o, objectMemberPath);
        }

        protected abstract IAlert TransformToAlertAtPath(object o, MemberPath objectMemberPath);

        //protected IEnumerable<InfoValue<PropertyInfo>> GetPropertyInfoValues(object o, MemberPath objectMemberPath)
        //{
        //    var typeInfo = TypeInfoSelector.Find(o,objectMemberPath);
        //    return from info in typeInfo.PropertyInfos
        //           select new InfoValue<PropertyInfo>
        //               {
        //                   Info = info,
        //                   Value = GetValue(() => info.Name, () => info.GetValue(o, null), objectMemberPath)
        //               };
        //}
         
        //protected IEnumerable<InfoValue<FieldInfo>> GetFieldInfoValues(object o, MemberPath objectMemberPath)
        //{
        //    var typeInfo = TypeInfoSelector.Find(o, objectMemberPath);
        //    return from info in typeInfo.FieldInfos
        //           select new InfoValue<FieldInfo>
        //               {
        //                   Info = info,
        //                   Value = GetValue(() => info.Name, () => info.GetValue(o), objectMemberPath)
        //               };
        //}

        //private object GetValue(Func<string> getName, Func<object> getValue, MemberPath objectMemberPath)
        //{
        //    try
        //    {
        //        //Check Tranform transformStrategy and transform if required
        //        var name = getName();
        //        var value = getValue();
        //        var valueMemberPath = CreateCopyWithAppendedElement(objectMemberPath, name);
        //        return Route(value, valueMemberPath);
        //    }
        //    catch (Exception)
        //    {
        //        //TODO: move to Issue Handler
        //        return "No Value";
        //    }

        //}
        
        //private static string[] CreateCopyWithAppendedElement(IEnumerable<string> source, string appendElement)
        //{
        //    return (source ?? Enumerable.Empty<string>()).Concat(new[] {appendElement}).ToArray();
        //}

        //protected class InfoValue<TInfo>
        //{
        //    public TInfo Info;
        //    public object Value;
        //}
    }
}
