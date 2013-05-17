namespace FluentAlerts
{
    public class AlertGroup: IAlertItem 
    {
        public GroupStyle Style { get; set; }
        public object[] Values { get; set;  }
    }
}
