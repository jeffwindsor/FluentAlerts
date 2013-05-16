using System.Text;

namespace FluentAlerts
{
    public class AlertTextBlock : IAlertItem
    {
        private readonly StringBuilder _text = new StringBuilder();
        public AlertTextBlock(string text = "")
        {
            if (!string.IsNullOrWhiteSpace(text))
            {
                Text.Append(text);
            }
        }

        public TextStyle Style { get; set; }
        public StringBuilder Text
        {
            get { return _text; }
        }

        public bool IsEmpty
        {
            get { return _text.Length == 0; }
        }

        public override string ToString()
        {
            return _text.ToString();
        }
    }
}
