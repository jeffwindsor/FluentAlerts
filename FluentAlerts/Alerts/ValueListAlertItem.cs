using System.Collections.Generic;

namespace FluentAlerts
{
    public class ValueListAlertItem : AlertItem
    {
        public ValueListAlertItem(ValueStyle style, IList<object> values)
        {
            Style = style;
            Values = values;
        }

        public ValueStyle Style
        {
            get { return ItemStyle.ToValueStyle(); }
            set { ItemStyle = value.ToItemStyle(); }
        }
    
    }
}