namespace FluentAlerts
{
    /// <summary>
    /// Static Factory for non IoC implementations
    /// </summary>
    public class Alerts
    {
        private static readonly IAlertBuilderFactory BuilderFactory = 
            new AlertBuilderFactory(new GenericNotificationFactory<Alert>());

        public static IAlertDocumentBuilder Create(string title = "")
        {
            return BuilderFactory.Create(title);
        }

        public static IAlertTableBuilder CreateTable(object o, string title = "")
        {
            return BuilderFactory.CreateTable(o, title);
        }

        public static IAlertTableBuilder CreateTable(string title = "")
        {
            return BuilderFactory.CreateTable(title);
        }

    }
}
