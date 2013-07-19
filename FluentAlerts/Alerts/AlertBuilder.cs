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
        
        public IAlertBuilder With(params object[] values)
        {
            return WithValueList(ValueStyle.Normal, values);
        }
        
        public IAlertBuilder With(IEnumerable<object[]> listOfValues)
        {
            foreach (var values in listOfValues)
            {
                With(values);
            }
            return this;
        }

        public IAlertBuilder WithEmphasized(params object[] values)
        {
            return WithValueList(ValueStyle.Emphasized, values);
        }

        public IAlertBuilder WithSeperator()
        {
            _items.Add(new SeperatorAlertItem());
            return this;
        }

        public IAlertBuilder WithUrl(string text, string url)
        {
            _items.Add(new UrIAlertItem(text, url));
            return this;
        }

        public IAlertBuilder WithAlert(IAlert n)
        {
            _items.Add(n);
            return this;
        }

        public IAlertBuilder WithAlert(IAlertBuilder n)
        {
            return WithAlert(n.ToAlert());
        }

        public IAlertBuilder WithText(string format, params object[] args)
        {
            return WithValue(ValueStyle.Normal, format, args);
        }

        public IAlertBuilder WithEmphasizedText(string format, params object[] args)
        {
            return WithValue(ValueStyle.Emphasized, format, args);
        }

        public IAlertBuilder WithTitle(string format, params object[] args)
        {
            return WithValue(ValueStyle.Title, format, args);
        }

        public IAlert ToAlert()
        {
            return _alertFactory.Create(_items);
        }



        private IAlertBuilder WithValueList(ValueStyle style, params object[] values)
        {
            _items.Add(new ValueListAlertItem(style, values));
            return this;
        }

        private IAlertBuilder WithValue(ValueStyle style, string format, params object[] args)
        {
            var text = string.Format(format, args);
            _items.Add(new ValueAlertItem(style, text));
            return this;
        }

    }
}
