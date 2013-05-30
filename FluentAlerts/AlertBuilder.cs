using System.Collections.Generic;

namespace FluentAlerts
{
    public interface IAlertBuilder
    {
        //Fluent Methods

        /// <summary>
        /// Appends text to first item if it is a text block, otherwise it inserts a text block at
        /// the first position
        /// </summary>
        IAlertBuilder WithSeperator();
        IAlertBuilder With(string text);
        IAlertBuilder With(string format, params object[] args);
        IAlertBuilder WithEmphasized(string text);
        IAlertBuilder WithEmphasized(string format, params object[] args);
        IAlertBuilder WithHeaderOne(string text);
        IAlertBuilder WithHeaderOne(string format, params object[] args);
        IAlertBuilder WithUrl(string text, string url);
        IAlertBuilder WithValue(object value);
        IAlertBuilder WithValues(IEnumerable<object> values);
        IAlertBuilder WithRow(params object[] values);
        IAlertBuilder WithEmphasizedRow(params object[] values);
        IAlertBuilder WithRows(IEnumerable <object[]> listOfValues);
        IAlertBuilder WithAlert(IAlert n);
        
        //Terminal Methods
         
        /// <summary>
        /// The build function, produces a alert with the current items
        /// </summary>
        IAlert ToAlert();
    }

    public class AlertBuilder : IAlertBuilder
    {
        private readonly IAlertFactory _alertFactory;
        private readonly AlertList _items = new AlertList();

        public AlertBuilder(IAlertFactory iaf)
        {
            _alertFactory = iaf;
        }
        public IAlertBuilder WithSeperator()
        {
            _items.Add(new SeperatorItem());
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
            _items.Add(new TextItem(style, text));
            return this;
        }

        public IAlertBuilder WithUrl(string text, string url)
        {
            _items.Add(new UrlItem(text, url));
            return this;
        }

        public IAlertBuilder WithValue(object value)
        {
            _items.Add(new ValueItem(value));
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
            _items.Add(new ArrayItem(ArrayStyle.Normal, cells));
            return this;
        }

        public IAlertBuilder WithEmphasizedRow(params object[] cells)
        {
            _items.Add(new ArrayItem(ArrayStyle.Emphasized, cells));
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
