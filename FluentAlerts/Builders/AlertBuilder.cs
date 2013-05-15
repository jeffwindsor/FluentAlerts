using System.Collections.Generic;

namespace FluentAlerts
{
    public interface IAlertBuilder
    {
        /// <summary>
        /// The build function, produces a notification with the current items
        /// </summary>
        IAlert ToAlert();
    }

    public abstract class BaseAlertBuilder : IAlertBuilder
    {
        private readonly IAlertFactory _notificationFactory;
      
        protected BaseAlertBuilder(IAlertFactory nf)
        {
            _notificationFactory = nf;
            NotificationItems = new List<IAlertItem>();
        }

        protected AlertStyle NotificationStyle { get; private set; }
        protected IList<IAlertItem> NotificationItems { get; private set; }

        //TODO: Is this needed for fluent build, is so change name to WithStyle otherwise remove and use property
        protected void SetStyle(AlertStyle style)
        {
            NotificationStyle = style;
        }

        protected void AppendNotifications(IEnumerable<IAlert> ns)
        {
            if (ns == null) return;
            foreach (var n in ns) {
                AppendNotification(n);
            }
        }
        protected void AppendNotification(IAlert n)
        {
            if (n != null)
            {
                AddNotificationItems(n);
            }
        }

        protected void AddNotificationItems(IEnumerable<IAlertItem> items)
        {
            if (items == null) return;
            foreach (var item in items) {
                AddNotificationItem(item);
            }
        }
        protected void AddNotificationItem(IAlertItem item)
        {
            if (item != null)
            {
                NotificationItems.Add(item);
            }
        }
 
        //TODO : any advatage to making INotification.AddValue generic, maybe for type incerception inserializers?
        protected void AddValue(object item)
        {
            AddGroup(GroupStyle.Value, new object[] { item });
        }

        protected void AddGroup(GroupStyle style, params object[] items)
        {
            AddNotificationItem(new AlertGroup{Style = style,Values = items});
        }

        protected void AddText(string text, TextStyle style = TextStyle.Normal)
        {
            if (!string.IsNullOrEmpty(text))
            {
                AddNotificationItem(new AlertTextBlock(text){Style = TextStyle.Normal});
            }
        }

        public IAlert ToAlert()
        {
            return _notificationFactory.Create(NotificationStyle, NotificationItems);
        }

    }

}
