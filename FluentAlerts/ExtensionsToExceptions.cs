using System;

namespace FluentAlerts
{
    internal static class ExtensionsToExceptions
    {
        public static IAlertBuilder ToAlert<TException>(this TException ex) where TException : Exception
        {
            return Alerts.Create(ex);
        }
    }
}
