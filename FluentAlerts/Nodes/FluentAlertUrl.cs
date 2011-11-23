namespace FluentAlerts.Nodes
{
    internal class FluentAlertUrl : IFluentAlert
    {
        public FluentAlertUrl(string text, string url)
        {
            Text = text;
            Url = url;
        }

        public string Text { get; set; }
        public string Url { get; set; }
    }
}