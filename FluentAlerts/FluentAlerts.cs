using System;

namespace FluentAlerts
{
    public class FluentAlerts: IFluentAlerts
    {
        public IFluentAlertBuilder Create()
        {
            return CreateAlertBuilder();
        }

        public IFluentAlertBuilder Create(string title)
        {
            return Create().WithTitle(title);
        }

        public IFluentAlertBuilder Create(object obj)
        {
            return CreateAlertBuilder().With(obj);
        }

        private static FluentAlertBuilder CreateAlertBuilder()
        {
            return new FluentAlertBuilder();
        }
    }
}
