using System.Collections.Generic;

namespace FluentAlerts
{
    public class ArrayAlertItem : AlertItem
    {
        public ArrayAlertItem(ArrayStyle style, IList<object> values)
        {
            Style = style;
            Values = values;
        }

        public ArrayStyle Style {
            get { return ItemStyle.ToArrayStyle(); }
            set { ItemStyle = value.ToItemStyle(); }
        }
    
    }
}