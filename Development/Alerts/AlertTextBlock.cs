﻿using System.Text;

namespace Alerts
{
    internal class AlertTextBlock : IAlert
    {
        public AlertTextBlock(string text = null, TextStyle style = TextStyle.Normal)
        {
            Style = style;
            Text = new StringBuilder();
            if (!string.IsNullOrEmpty(text)) Text.Append(text);
       }
        public TextStyle Style { get; set; }
        public StringBuilder Text { get; private set; }
        public bool IsEmpty() { return Text.Length == 0;}
    }
}
