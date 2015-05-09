using System;
using System.Collections.Generic;
using System.Linq;

using FluentAlerts.Transformers;

namespace FluentAlerts
{
    public static class ExtensionsToIAlertBuilder
    {
        public static IAlert Transform(this IFluentAlertsBuilder fluentAlertsBuilder, ITransformer transformer)
        {
            return fluentAlertsBuilder.ToAlert().Transform(transformer);
        }

        public static void Throw<TAlertException>(this IFluentAlertsBuilder fluentAlertsBuilder,
                                                  Func<IAlert, TAlertException> constructor)
            where TAlertException : FluentAlertException
        {
            fluentAlertsBuilder.ToAlert().Throw(constructor);
        }

        public static void Throw<TAlertException>(this IFluentAlertsBuilder fluentAlertsBuilder,
                                                  Func<IAlert, Exception, TAlertException> constructor,
                                                  Exception inner)
            where TAlertException : FluentAlertException
        {
            fluentAlertsBuilder.ToAlert().Throw(constructor, inner);
        }

        public static IEnumerable<IAlertItem> AllItems(this IFluentAlertsBuilder fluentAlertsBuilder)
        {
            return fluentAlertsBuilder.ToAlert().AllItems();
        }

        public static IEnumerable<object> AllValues(this IFluentAlertsBuilder fluentAlertsBuilder)
        {
            return fluentAlertsBuilder.ToAlert().AllValues();
        }
   
    }
}
