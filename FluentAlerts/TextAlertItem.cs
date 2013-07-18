namespace FluentAlerts
{
    public class TextAlertItem : AlertItem
    {
        public TextAlertItem(TextStyle style, string text)
        {
            Style = style;
            Values = new object[] {text};
        } 

        public string Text {
            get { return (string) Values[0]; }
            set { Values[0] = value; }
        }

        public TextStyle Style
        {
            get { return ItemStyle.ToTextStyle(); }
            set { ItemStyle = value.ToItemStyle(); }
        }

    }
}