using System;
using System.Collections.Generic;
using System.Linq;

namespace FluentAlerts
{
    //TODO : With footer of ??
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
        IAlertBuilder WithTextBlock(string text);
        IAlertBuilder WithTextBlock(TextStyle style, string text);
        IAlertBuilder WithTextBlock(string format, params object[] args);
        IAlertBuilder WithTextBlock(TextStyle style, string format, params object[] args);
        IAlertBuilder WithUrl(string text, string url);
        IAlertBuilder WithValue(object value);
        IAlertBuilder WithValues(IEnumerable<object> values);
        IAlertBuilder WithRow(params object[] items);
        IAlertBuilder WithRow(RowStyle style, params object[] items);
        IAlertBuilder WithAlert(IAlert n);
        
        //Terminal Methods

        /// <summary>
        /// The build function, produces a notification with the current items
        /// </summary>
        IAlert ToAlert();
        void Throw();
        void ThrowAs<TException>(Func<IAlert, Exception, TException> constructor) where TException : AlertException;
    }

    public class AlertBuilder : IAlertBuilder
    {
        private readonly IAlertFactory _notificationFactory;
        private AlertStyle _style;
        private readonly IList<IAlertItem> _items;
        private Exception _inner = null;

        public AlertBuilder(IAlertFactory iaf)
        {
            _notificationFactory = iaf;
            _items = new List<IAlertItem>();
        }

        //NOTE: special case still debating form
        internal IAlertBuilder WithInnerException(Exception ex)
        {
            _inner = ex;
            return this;
        }

        /// <summary>
        /// Appends text to first item if it is a text block, otherwise it inserts a text block at
        /// the first position
        /// </summary>
        public void AppendTitleWith(string text)
        {
            if (!string.IsNullOrWhiteSpace(text))
            {
                GetCreateTitle().Text.Append(text);
            }
        }

        public IAlertBuilder WithTitleOf(string text)
        {
            if (!string.IsNullOrWhiteSpace(text))
            {
                var title = GetCreateTitle().Text;
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
            AddGroup(GroupStyle.Seperator, string.Empty);
            return this;
        }

        public IAlertBuilder WithUrl(string text, string url)
        {
            AddGroup(GroupStyle.Url, text, url);
            return this;
        }

        public IAlertBuilder WithValue(object value)
        {
            AddGroup(GroupStyle.Value, value);
            return this;
        }

        public IAlertBuilder WithValues(IEnumerable<object> values)
        {
            if (values != null)
            {
                foreach (var v in values)
                {
                    WithValue(v);
                }
            }
            return this;
        }

        public IAlertBuilder WithAlert(IAlert n)
        {
            Add_items(n);
            return this;
        }

        public IAlertBuilder WithTextBlock(TextStyle style, string text)
        {
            AddText(text, style);
            return this;
        }

        public IAlertBuilder WithTextBlock(TextStyle style, string format, params object[] args)
        {
            return WithTextBlock(style, string.Format(format, args));
        }

        public IAlertBuilder WithTextBlock(string text)
        {
            return WithTextBlock(TextStyle.Normal, text);
        }

        public IAlertBuilder WithTextBlock(string format, params object[] args)
        {
            return WithTextBlock(TextStyle.Normal, format, args);
        }

        public IAlertBuilder WithRow(params object[] items)
        {
            return WithRow(RowStyle.Normal, items);
        }

        public IAlertBuilder WithRow(RowStyle style, params object[] items)
        {
            AddGroup(style.ToGroupStyle(), items);
            return this;
        }
        public IAlertBuilder WithSpanningRow(object item)
        {
            return WithSpanningRow(RowStyle.Highlight, item);
        }

        public IAlertBuilder WithSpanningRow(RowStyle style, object item)
        {
            AddGroup(style.ToGroupStyle(), new[] { item });
            return this;
        }
        
        public IAlert ToAlert()
        {
            return _notificationFactory.Create(_style, _items);
        }

        public void Throw()
        {
            throw new AlertException(ToAlert(), _inner);
        }

        public void ThrowAs<TException>(Func<IAlert, Exception, TException> constructor) where TException : AlertException
        {
            throw constructor(ToAlert(), _inner);
        }

        //TODO: Is this needed for fluent build, is so change name to WithStyle otherwise remove and use property
        private void SetStyle(AlertStyle style)
        {
            _style = style;
        }

        private void AppendNotifications(IEnumerable<IAlert> ns)
        {
            if (ns == null) return;
            foreach (var n in ns) {
                AppendNotification(n);
            }
        }
        private void AppendNotification(IAlert n)
        {
            if (n != null)
            {
                Add_items(n);
            }
        }

        private void Add_items(IEnumerable<IAlertItem> items)
        {
            if (items == null) return;
            foreach (var item in items) {
                AddNotificationItem(item);
            }
        }
        private void AddNotificationItem(IAlertItem item)
        {
            if (item != null)
            {
                _items.Add(item);
            }
        }
 
        //TODO : any advatage to making INotification.AddValue generic, maybe for type incerception inserializers?
        private void AddValue(object item)
        {
            AddGroup(GroupStyle.Value, new object[] { item });
        }

        private void AddGroup(GroupStyle style, params object[] items)
        {
            AddNotificationItem(new AlertGroup{Style = style,Values = items});
        }

        private void AddText(string text, TextStyle style = TextStyle.Normal)
        {
            if (!string.IsNullOrEmpty(text))
            {
                AddNotificationItem(new AlertTextBlock(text){Style = TextStyle.Normal});
            }
        }


        /// <summary>
        /// Title is defined in a document as a text block of style title in the first item position
        /// </summary>
        /// <returns>Current or new inserted Title</returns>
        private AlertTextBlock GetCreateTitle()
        {
            if (HasTitle())
            {
                return _items[0] as AlertTextBlock;
            }
            return CreateTitle();
        }

        private bool HasTitle()
        {
            return (_items.Any() && IsTitle(_items[0]));
        }

        private AlertTextBlock CreateTitle()
        {
            //Othewise insert
            var result = new AlertTextBlock { Style = TextStyle.Header };
            _items.Insert(0, result);
            return result;
        }

        /// <summary>
        /// Return true if object is a text block with a style of Title
        /// </summary>
        private static bool IsTitle(object o)
        {
            var block = o as AlertTextBlock;
            if (block != null)
            {
                return block.Style == TextStyle.Header;
            }
            return false;
        }

    }

}
