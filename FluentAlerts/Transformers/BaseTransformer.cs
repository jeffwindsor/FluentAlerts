using System;
using System.Collections;
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
        protected readonly Type FormatterResultType = typeof (TFormatterResult);
        protected readonly IAlertBuilderFactory AlertBuilderFactory;

        protected BaseTransformer(ITransformStrategy transformStrategy,
                                  ITypeInfoSelector typeInfoSelector,
                                  IValueFormatter<TFormatterResult> formatter,
                                  IAlertBuilderFactory alertBuilderFactory)
        {
            TypeInfoSelector = typeInfoSelector;
            TransformStrategy = transformStrategy;
            Formatter = formatter;
            AlertBuilderFactory = alertBuilderFactory;
        }

        public object Transform(object o)
        {
            return Route(o, MemberPath.Empty);
        }

        protected object Route(object o, MemberPath objectMemberPath)
        {
            //Checks
            if(o == null) return "No Value"; //TODO: Use Issue Handler, or config value
            if (o.GetType() == FormatterResultType)  return o;

            //Check any other type against transformation rules
            //  if transformation required by rules then transform object to Alert and then re-curse for sub objects
            //  otherwise just format object as output format and return
            return TransformStrategy.IsTransformRequired(o, objectMemberPath)
                       ? Tranform(o, objectMemberPath)
                       : Formatter.Format(o, objectMemberPath);
        }

        private object Tranform(object o, MemberPath objectMemberPath)
        {
            //If an IEnumerable is encountered, route each item
            if (o is IEnumerable)
            {
                var items = o as IEnumerable;
                var listAlert = AlertBuilderFactory.Create();
                foreach (var item in items)
                {
                    listAlert.With(Route(item, objectMemberPath));
                }
                return listAlert.ToAlert();
            }

            return InnerTransform(o, objectMemberPath);
        }

        protected abstract object InnerTransform(object o, MemberPath objectMemberPath);


    }
}
