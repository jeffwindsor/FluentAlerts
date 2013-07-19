using System;

namespace FluentAlerts
{
    public class AlertBuilderFactory: IAlertBuilderFactory
    {
        private readonly IAlertFactory _alertFactory;
        public AlertBuilderFactory(IAlertFactory alertFactory)
        {
            _alertFactory = alertFactory;
        }

        public IAlertBuilder Create()
        {
            return CreateAlertBuilder();
        }

        public IAlertBuilder Create(string title)
        {
            return Create().WithTitle(title);
        }

        public IAlertBuilder Create(object obj)
        {
            return CreateAlertBuilder().With(obj);
        }

        private  AlertBuilder CreateAlertBuilder()
        {
            return new AlertBuilder(_alertFactory);
        }
    }
}
