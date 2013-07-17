using System;

namespace FluentAlerts
{
    public interface IAlertBuilderFactory
    {
        IAlertBuilder Create();
        IAlertBuilder Create(string title);
        IAlertBuilder Create(Exception ex);
    }

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
            return Create().WithHeaderOne(title);
        }

        public IAlertBuilder Create(Exception ex)
        {
            return CreateAlertBuilder().WithValue(ex);
        }

        private  AlertBuilder CreateAlertBuilder()
        {
            return new AlertBuilder(_alertFactory);
        }
    }
}
