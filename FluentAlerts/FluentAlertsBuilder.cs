using System.Collections.Generic;


namespace FluentAlerts
{
    public class FluentAlertsBuilder : IFluentAlertsBuilder
    {
        private readonly List<IAlertItem> _items = new List<IAlertItem>();
   
        public IFluentAlertsBuilder With(params object[] values)
        {
            //TODO: nulls
            return WithValueList(AlertItemValueStyle.Normal, values);
        }
        
        public IFluentAlertsBuilder With(IEnumerable<object[]> listOfValues)
        {
            //TODO: nulls
            foreach (var values in listOfValues)
            {
                With(values);
            }
            return this;
        }

        public IFluentAlertsBuilder WithEmphasized(params object[] values)
        {
            //TODO: nulls
            return WithValueList(AlertItemValueStyle.Emphasized, values);
        }

        public IFluentAlertsBuilder WithSeperator()
        {
            _items.Add(new AlertItem
            {
                ItemStyle = AlertItemStyle.Seperator,
                Values = new object[] {""}
            });
            return this;
        }

        public IFluentAlertsBuilder WithUrl(string text, string url)
        {
            //TODO, should we put in property name-value pairs for this sort of thing?
            //TODO: nulls
            _items.Add(new AlertItem
            {
                ItemStyle = AlertItemStyle.Url,
                Values = new object[] {url, text}
            });
            return this;
        }

        public IFluentAlertsBuilder WithAlert(IAlert n)
        {
            //TODO: nulls
            _items.Add(n);
            return this;
        }

        public IFluentAlertsBuilder WithAlert(IFluentAlertsBuilder n)
        {
            //TODO: nulls
            return WithAlert(n.ToAlert());
        }


        //todo: merge or append
        public IFluentAlertsBuilder Merge(IAlert n)
        {
            //TODO: nulls
            _items.AddRange(n);
            return this;
        }

        public IFluentAlertsBuilder Merge(IFluentAlertsBuilder n)
        {
            //TODO: nulls
            return Merge(n.ToAlert());
        }

        public IFluentAlertsBuilder WithTitle(string format, params object[] args)
        {
            //TODO: nulls
            var text = string.Format(format, args);
            return WithValueList(AlertItemValueStyle.Title, text);
        }

        public IAlert ToAlert()
        {
            return new Alert(_items);
        }
        
        private IFluentAlertsBuilder WithValueList(AlertItemValueStyle style, params object[] values)
        {
            //TODO: nulls????
            _items.Add(new AlertItemValueList(style, values));
            return this;
        }
        
       
    }
}
