using System.Collections.Generic;

namespace FluentAlerts
{
    public class AlertBuilder : IAlertBuilder
    {
        private readonly IAlertFactory _alertFactory;
        private readonly Alert _items = new Alert();

        public AlertBuilder(IAlertFactory iaf)
        {
            _alertFactory = iaf;
        }
        public IAlertBuilder WithSeperator()
        {
            _items.Add(new SeperatorAlertItem());
            return this;
        }
        
        public IAlertBuilder With(string text)
        {
            return With(TextStyle.Normal, text);
        }

        public IAlertBuilder With(string format, params object[] args)
        {
            return With(TextStyle.Normal, format, args);
        }

        public IAlertBuilder WithEmphasized(string text)
        {
            return With(TextStyle.Emphasized, text);
        }

        public IAlertBuilder WithEmphasized(string format, params object[] args)
        {
            return With(TextStyle.Emphasized, format, args);
        }

        public IAlertBuilder WithHeaderOne(string text)
        {
            return With(TextStyle.HeaderOne, text);
        }

        public IAlertBuilder WithHeaderOne(string format, params object[] args)
        {
            return With(TextStyle.HeaderOne, format, args);
        }

        private IAlertBuilder With(TextStyle style, string format, params object[] args)
        {
            return With(style, string.Format(format, args));
        }

        private IAlertBuilder With(TextStyle style, string text) 
        {
            _items.Add(new TextAlertItem(style, text));
            return this;
        }

        public IAlertBuilder WithUrl(string text, string url)
        {
            _items.Add(new UrIAlertItem(text, url));
            return this;
        }

        public IAlertBuilder WithValue(object value)
        {
            _items.Add(new ValueAlertItem(value));
            return this;
        }
        
        public IAlertBuilder WithValues(IEnumerable<object> values)
        {
            foreach (var value in values)
                WithValue(value);
            return this;
        }

        public IAlertBuilder WithRow(params object[] cells)
        {
            _items.Add(new ArrayAlertItem(ArrayStyle.Normal, cells));
            return this;
        }

        public IAlertBuilder WithEmphasizedRow(params object[] cells)
        {
            _items.Add(new ArrayAlertItem(ArrayStyle.Emphasized, cells));
            return this;
        }

        public IAlertBuilder WithRows(IEnumerable<object[]> rows)
        {
            foreach (var row in rows)
                WithRow(row);
            return this;
        }

        public IAlertBuilder WithAlert(IAlert n)
        {
            _items.Add(n);
            return this;
        }
        
        public IAlert ToAlert()
        {
            return _alertFactory.Create(_items);
        }
        
    }
}
