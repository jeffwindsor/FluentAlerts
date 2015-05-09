using System;

namespace FluentAlerts
{
    public class FluentAlerts: IFluentAlerts
    {
        public IFluentAlertsBuilder Create()
        {
            return CreateAlertBuilder();
        }

        public IFluentAlertsBuilder Create(string title)
        {
            return Create().WithTitle(title);
        }

        public IFluentAlertsBuilder Create(object obj)
        {
            return CreateAlertBuilder().With(obj);
        }

        private static FluentAlertsBuilder CreateAlertBuilder()
        {
            return new FluentAlertsBuilder();
        }
    }
}
