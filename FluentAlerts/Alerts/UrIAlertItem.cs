namespace FluentAlerts
{
    public class UrIAlertItem : AlertItem
    {
        public UrIAlertItem(string url, string text)
        {
            ItemStyle = ItemStyle.Url;
            Values = new object[] {url, text};
        }

        public string Uri 
        {
            get { return (string)Values[0]; }
            set { Values[0] = value; }
        }

        public string Description
        {
            get { return (string) Values[1]; }
            set { Values[1] = value; }
        }
    }
}