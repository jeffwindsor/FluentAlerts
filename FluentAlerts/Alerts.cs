using System;

namespace FluentAlerts
{
    /// <summary>
    /// Static Factory for non IoC implementations
    /// </summary>
    public class Alerts
    {
        public static IAlertBuilder Create()
        {
            return new AlertBuilder(new AlertFactory<Alert>());
        }
        public static IAlertBuilder Create(string title)
        {
           return Create().WithTitleOf(title);
        }
         
        internal static IAlertBuilder Create(Exception inner)
        {
            var builder = new AlertBuilder(new AlertFactory<Alert>());
            builder.WithInnerException(inner);
            return builder;
        }
    }
}
