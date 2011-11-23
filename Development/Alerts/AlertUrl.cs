namespace Alerts
{
    internal class AlertURL : IAlert
    {
        public AlertURL(string text, string url)
        {
            Text = text;
            Url = url;
        }

        public string Text { get; set; }
        public string Url { get; set; }
    }
}