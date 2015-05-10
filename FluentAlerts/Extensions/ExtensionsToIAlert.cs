using System;
using System.Collections.Generic;
using System.Linq;

using FluentAlerts.Transformers;

namespace FluentAlerts
{
    public static class ExtensionsToIAlert
    {
        public static string Render(this IFluentAlert alert, IFluentAlertSerializer renderer)
        {
            return renderer.Serialize(alert);
        } 

        public static IFluentAlert Transform(this IFluentAlert alert, ITransformer transformer)
        {
            return (IFluentAlert)transformer.Transform(alert);
        }
        
        public static void Throw<TAlertException>(this IFluentAlert alert,
                                                  Func<IFluentAlert, TAlertException> constructor)
            where TAlertException : FluentAlertException
        {
            throw constructor(alert);
        }

        public static void Throw<TAlertException>(this IFluentAlert alert,
                                                  Func<IFluentAlert, Exception, TAlertException> constructor,
                                                  Exception inner)
            where TAlertException : FluentAlertException
        {
            throw constructor(alert, inner);
        }

        public static IEnumerable<IFluentAlertItem> AllItems(this IFluentAlert alert)
        {
            //HACK: may not be the most efficent
            var stack = new Stack<IFluentAlertItem>(alert);
            while (stack.Any())
            {
                var alertItem = stack.Pop();
                yield return alertItem;

                //Add children to stack
                var stackAlert = alertItem as IFluentAlert;
                if (stackAlert != null)
                    foreach (var item in stackAlert)
                        stack.Push(item);

                //Add group values
                var alertGroup = alertItem as AlertItem;
                if (alertGroup != null)
                    foreach (var item in alertGroup.Values.Where(v => v is IFluentAlertItem))
                        stack.Push(item as IFluentAlertItem);

            }
        }

        public static IEnumerable<object> AllValues(this IFluentAlert alert)
        {
            return from item in alert.AllItems()
                   where item is AlertItem
                   from value in (item as AlertItem).Values
                   where !(value is IFluentAlert)
                   select value;
        }
   
    }
}
