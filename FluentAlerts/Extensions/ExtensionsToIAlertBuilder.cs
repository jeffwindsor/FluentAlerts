using System;
using System.Collections.Generic;
using System.Linq;

using FluentAlerts.Transformers;

namespace FluentAlerts
{
    public static class ExtensionsToIAlertBuilder
    {
        public static IFluentAlert Transform(this IFluentAlertBuilder fluentAlertsBuilder, ITransformer transformer)
        {
            return fluentAlertsBuilder.ToAlert().Transform(transformer);
        }

        public static void Throw<TAlertException>(this IFluentAlertBuilder fluentAlertsBuilder,
                                                  Func<IFluentAlert, TAlertException> constructor)
            where TAlertException : FluentAlertException
        {
            fluentAlertsBuilder.ToAlert().Throw(constructor);
        }

        public static void Throw<TAlertException>(this IFluentAlertBuilder fluentAlertsBuilder,
                                                  Func<IFluentAlert, Exception, TAlertException> constructor,
                                                  Exception inner)
            where TAlertException : FluentAlertException
        {
            fluentAlertsBuilder.ToAlert().Throw(constructor, inner);
        }

        public static IEnumerable<IFluentAlertItem> AllItems(this IFluentAlertBuilder fluentAlertsBuilder)
        {
            return fluentAlertsBuilder.ToAlert().AllItems();
        }

        public static IEnumerable<object> AllValues(this IFluentAlertBuilder fluentAlertsBuilder)
        {
            return fluentAlertsBuilder.ToAlert().AllValues();
        }
   
    }
}
