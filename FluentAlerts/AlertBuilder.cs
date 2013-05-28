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
        void AppendTitleWith(string text);
        IAlertBuilder WithTitleOf(string text);
        IAlertBuilder WithTitleOf(string format, params object[] args);
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
        private readonly AlertItemCollection _items;

        public AlertBuilder(IAlertFactory iaf, AlertItemCollection items)
        {
            _alertFactory = iaf;
            _items = items;
        }

        /// <summary>
        /// Appends text to first item if it is a text block, otherwise it inserts a text block at
        /// the first position
        /// </summary>
        public void AppendTitleWith(string text)
        {
            if (!string.IsNullOrWhiteSpace(text))
            {
                _items.GetCreateTitle().Text.Append(text);
            }
        }

        public IAlertBuilder WithTitleOf(string text)
        {
            if (!string.IsNullOrWhiteSpace(text))
            {
                var title = _items.GetCreateTitle().Text;
                //Clear and Append Text
                title.Length = 0;
                title.Append(text);
            }
            return this;
        }

        public IAlertBuilder WithTitleOf(string format, params object[] args)
        {
            return WithTitleOf(string.Format(format, args));
        }

        public IAlertBuilder WithSeperator()
        {
            _items.AddGroup(GroupStyle.Seperator, string.Empty);
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
            return With(TextStyle.Header_One, text);
        }

        public IAlertBuilder WithHeaderOne(string format, params object[] args)
        {
            return With(TextStyle.Header_One, format, args);
        }

        public IAlertBuilder WithUrl(string text, string url)
        {
            _items.AddGroup(GroupStyle.Url, text, url);
            return this;
        }

        public IAlertBuilder WithValue(object value)
        {
            _items.AddGroup(GroupStyle.Value, value);
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
            _items.AddGroup(GroupStyle.Row, cells);
            return this;
        }

        public IAlertBuilder WithEmphasizedRow(params object[] cells)
        {
            _items.AddGroup(GroupStyle.EmphasizedRow, cells);
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
            _items.AddAlertItems(n);
            return this;
        }
        
        public IAlert ToAlert()
        {
            return _alertFactory.Create(_items);
        }
       
  
        private IAlertBuilder With(TextStyle style, string format, params object[] args)
        {
            return With(style, string.Format(format, args));
        }

        private IAlertBuilder With(TextStyle style, string text)
        {
            _items.AddText(text, style);
            return this;
        }
    }
}
