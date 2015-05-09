using System.Collections.Generic;

namespace FluentAlerts
{
    public class AlertItemValueList : AlertItem
    {
        public AlertItemValueList(AlertItemValueStyle style, IList<object> values)
        {
            Style = style;
            Values = values;
        }

        public AlertItemValueStyle Style
        {
            get { return ItemStyle.ToValueStyle(); }
            set { ItemStyle = value.ToItemStyle(); }
        }
    
    }
}