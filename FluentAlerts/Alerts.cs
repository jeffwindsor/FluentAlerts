namespace FluentAlerts
{
    /// <summary>
    /// Static Factory for non IoC implementations
    /// </summary>
    public class Alerts
    {
        private static readonly IAlertFactory AlertFactory = new GenericNotificationFactory<Alert>();
        public static IAlertBuilder Create()
        {
            return new AlertBuilder(AlertFactory);
        }
        public static IAlertBuilder Create(string title)
        {
           return Create().WithTitleOf(title);
        } 
    }
}
