using System;

namespace FluentAlerts
{
    internal static class ExceptionExtensions
    {
        public static IAlertBuilder WrapInAlert<TException>(this TException ex) where TException : Exception
        {
            //HACK: IOC and extendability issue
            return Alerts.Create(ex);
        }
    }
}
