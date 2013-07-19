namespace FluentAlerts
{
    public class ValueAlertItem : AlertItem
    {
        public ValueAlertItem(ValueStyle style, object value)
        {
            Style = style;
            Values = new object[] { value };
        } 

        public object Value {
            get { return Values[0]; }
            set { Values[0] = value; }
        }

        public ValueStyle Style
        {
            get { return ItemStyle.ToValueStyle(); }
            set { ItemStyle = value.ToItemStyle(); }
        }

    }
}