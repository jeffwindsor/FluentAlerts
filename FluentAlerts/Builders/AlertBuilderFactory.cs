 using System;

namespace FluentAlerts
{
    public interface IAlertBuilderFactory
    {
        IAlertDocumentBuilder Create(string title = "");
        IAlertTableBuilder CreateTable(string title = "");
        IAlertTableBuilder CreateTable(object o, string title = "");

    }
    //TODO: convert factory apis to Funcs<>??
    public class AlertBuilderFactory: IAlertBuilderFactory
    {
        private readonly IAlertFactory _alertFactory;
        public AlertBuilderFactory(IAlertFactory alertFactory)
        {
            _alertFactory = alertFactory;
        }

        public IAlertDocumentBuilder Create(string title = "")
        {
            return new AlertDocumentBuilder(_alertFactory)
                .WithTitleOf(title);
        }

        public IAlertTableBuilder CreateTable(string title = "")
        {
            var result = new AlertTableBuilder(_alertFactory) as IAlertTableBuilder;
            if (String.IsNullOrWhiteSpace(title) == false)
                result.WithSpanningRow(title);
            return result;
        }

        public IAlertTableBuilder CreateTable(object o, string title = "")
        {
            return CreateTable(title).WithRow(o);
        }
    }
}
