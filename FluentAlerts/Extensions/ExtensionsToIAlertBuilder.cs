using System;
using System.Collections.Generic;
using System.Linq;
using FluentAlerts.Transformers;

namespace FluentAlerts
{
    public static class ExtensionsToIAlertBuilder
    {
        public static IAlert Transform<TResult>(this IAlertBuilder alertBuilder, ITransformer<TResult> transformer)
        {
            return alertBuilder.ToAlert().Transform(transformer);
        }

        public static void Throw<TAlertException>(this IAlertBuilder alertBuilder,
                                                  Func<IAlert, TAlertException> constructor)
            where TAlertException : AlertException
        {
            alertBuilder.ToAlert().Throw(constructor);
        }

        public static void Throw<TAlertException>(this IAlertBuilder alertBuilder,
                                                  Func<IAlert, Exception, TAlertException> constructor,
                                                  Exception inner)
            where TAlertException : AlertException
        {
            alertBuilder.ToAlert().Throw(constructor, inner);
        }

        public static IEnumerable<IAlertItem> AllItems(this IAlertBuilder alertBuilder)
        {
            return alertBuilder.ToAlert().AllItems();
        }

        public static IEnumerable<object> AllValues(this IAlertBuilder alertBuilder)
        {
            return alertBuilder.ToAlert().AllValues();
        }
   
    }
}
