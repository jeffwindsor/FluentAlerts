using System;
using System.Collections.Generic;
using System.Linq;
using FluentAlerts.Transformers;

namespace FluentAlerts
{
    public static class ExtensionsToIAlert
    {
        public static IAlert Transform(this IAlert alert)
        {
            return alert.Transform(Alerts.Transformers.CreateDefault());
        }

        public static IAlert Transform<TResult>(this IAlert alert, ITransformer<TResult> transformer)
        {
            return transformer.Transform(alert);
        }

        public static void Throw(this IAlert alert)
        {
            throw new AlertException(alert);
        }

        public static void Throw(this IAlert alert,Exception inner)
        {
            throw new AlertException(alert, inner);
        }

        public static void ThrowAs<TAlertException>(this IAlert alert, Func<IAlert, TAlertException> constructor) where TAlertException : AlertException
        {
            throw constructor(alert);
        }

        public static void ThrowAs<TAlertException>(this IAlert alert, Func<IAlert, Exception, TAlertException> constructor, Exception inner) where TAlertException : AlertException
        {
            throw constructor(alert, inner);
        }

        public static IEnumerable<IAlertItem> AllItems(this IAlert alert)
        {
            //HACK: may not be the most efficent
            var stack = new Stack<IAlertItem>(alert);
            while (stack.Any())
            {
                var alertItem = stack.Pop();
                yield return alertItem;

                //Add children to stack
                var stackAlert = alertItem as IAlert;
                if (stackAlert != null)
                    foreach (var item in stackAlert) 
                        stack.Push(item);

                //Add group values
                var alertGroup = alertItem as AlertGroup;
                if(alertGroup != null)
                    foreach(var item in alertGroup.Values.Where(v => v is IAlertItem))
                        stack.Push(item as IAlertItem);

            }
        }

        public static IEnumerable<object> AllValues(this IAlert alert)
        {
            return from item in alert.AllItems()
                   where item is AlertGroup
                   from value in (item as AlertGroup).Values
                   where !(value is IAlert)
                   select value;
        }
    }
}
