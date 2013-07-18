namespace FluentAlerts
{
    public class ValueAlertItem : AlertItem
    {
        public ValueAlertItem(object value)
        {
            ItemStyle = ItemStyle.Value;
            Values = new [] {value};
        }
    }
}