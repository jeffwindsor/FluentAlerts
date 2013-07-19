using System;
using System.Collections.Generic;
using System.Linq;
using FluentAlerts.Transformers;
using FluentAlerts.Renderers;

namespace FluentAlerts
{
    public static class ExtensionsToIAlert
    {
        public static string Render(this IAlert alert, IAlertRenderer renderer)
        {
            return renderer.Render(alert);
        } 

        public static IAlert Transform<TResult>(this IAlert alert, ITransformer<TResult> transformer)
        {
            return transformer.Transform(alert);
        }
        
        public static void Throw<TAlertException>(this IAlert alert,
                                                  Func<IAlert, TAlertException> constructor)
            where TAlertException : AlertException
        {
            throw constructor(alert);
        }

        public static void Throw<TAlertException>(this IAlert alert,
                                                  Func<IAlert, Exception, TAlertException> constructor,
                                                  Exception inner)
            where TAlertException : AlertException
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
                var alertGroup = alertItem as AlertItem;
                if (alertGroup != null)
                    foreach (var item in alertGroup.Values.Where(v => v is IAlertItem))
                        stack.Push(item as IAlertItem);

            }
        }

        public static IEnumerable<object> AllValues(this IAlert alert)
        {
            return from item in alert.AllItems()
                   where item is AlertItem
                   from value in (item as AlertItem).Values
                   where !(value is IAlert)
                   select value;
        }
   
    }
}
