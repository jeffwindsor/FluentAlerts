using System.Collections.Generic;

namespace FluentAlerts
{
    public class AlertBuilder : IAlertBuilder
    {
        private readonly IAlertFactory _alertFactory;
        private readonly List<IAlertItem> _items = new List<IAlertItem>();

        public AlertBuilder(IAlertFactory iaf)
        {
            _alertFactory = iaf;
        }
        
        public IAlertBuilder With(params object[] values)
        {
            //TODO: nulls
            return WithValueList(ValueStyle.Normal, values);
        }
        
        public IAlertBuilder With(IEnumerable<object[]> listOfValues)
        {
            //TODO: nulls
            foreach (var values in listOfValues)
            {
                With(values);
            }
            return this;
        }

        public IAlertBuilder WithEmphasized(params object[] values)
        {
            //TODO: nulls
            return WithValueList(ValueStyle.Emphasized, values);
        }

        public IAlertBuilder WithSeperator()
        {
            _items.Add(new SeperatorAlertItem());
            return this;
        }

        public IAlertBuilder WithUrl(string text, string url)
        {
            //TODO: nulls
            _items.Add(new UrIAlertItem(text, url));
            return this;
        }

        public IAlertBuilder WithAlert(IAlert n)
        {
            //TODO: nulls
            _items.Add(n);
            return this;
        }

        public IAlertBuilder WithAlert(IAlertBuilder n)
        {
            //TODO: nulls
            return WithAlert(n.ToAlert());
        }

        public IAlertBuilder Merge(IAlert n)
        {
            //TODO: nulls
            _items.AddRange(n);
            return this;
        }

        public IAlertBuilder Merge(IAlertBuilder n)
        {
            //TODO: nulls
            return Merge(n.ToAlert());
        }

        public IAlertBuilder WithTitle(string format, params object[] args)
        {
            //TODO: nulls
            var text = string.Format(format, args);
            return WithValueList(ValueStyle.Title, text);
        }

        public IAlert ToAlert()
        {
            return _alertFactory.Create(_items);
        }
        
        private IAlertBuilder WithValueList(ValueStyle style, params object[] values)
        {
            //TODO: nulls????
            _items.Add(new ValueListAlertItem(style, values));
            return this;
        }
        
       
    }
}
