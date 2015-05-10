using System.Collections.Generic;


namespace FluentAlerts
{
    public class FluentAlertBuilder : IFluentAlertBuilder
    {
        private readonly List<IFluentAlertItem> _items = new List<IFluentAlertItem>();

        private const string Normal = "normal";


        public IFluentAlertBuilder With(params object[] values)
        {
            //TODO: nulls
            return WithValueList(AlertItemValueStyle.Normal, values);
        }
        
        public IFluentAlertBuilder With(IEnumerable<object[]> listOfValues)
        {
            //TODO: nulls
            foreach (var values in listOfValues)
            {
                With(values);
            }
            return this;
        }

        public IFluentAlertBuilder WithEmphasized(params object[] values)
        {
            //TODO: nulls
            return WithValueList(AlertItemValueStyle.Emphasized, values);
        }

        public IFluentAlertBuilder WithSeperator()
        {
            _items.Add(new AlertItem
            {
                ItemStyle = AlertItemStyle.Seperator,
                Values = new object[] {""}
            });
            return this;
        }

        public IFluentAlertBuilder WithUrl(string text, string url)
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

        public IFluentAlertBuilder WithAlert(IFluentAlert n)
        {
            //TODO: nulls
            _items.Add(n);
            return this;
        }

        public IFluentAlertBuilder WithAlert(IFluentAlertBuilder n)
        {
            //TODO: nulls
            return WithAlert(n.ToAlert());
        }


        //todo: merge or append
        public IFluentAlertBuilder Merge(IFluentAlert n)
        {
            //TODO: nulls
            _items.AddRange(n);
            return this;
        }

        public IFluentAlertBuilder Merge(IFluentAlertBuilder n)
        {
            //TODO: nulls
            return Merge(n.ToAlert());
        }

        public IFluentAlertBuilder WithTitle(string format, params object[] args)
        {
            //TODO: nulls
            var text = string.Format(format, args);
            return WithValueList(AlertItemValueStyle.Title, text);
        }

        public IFluentAlert ToAlert()
        {
            return new Alert(_items);
        }
        
        private IFluentAlertBuilder WithValueList(AlertItemValueStyle style, params object[] values)
        {
            if(values != null)
                _items.Add(new AlertItemValueList(style, values));

            return this;
        }
        
       
    }
}
