using System.Collections.Generic;
using System.Linq;

namespace FluentAlerts
{
    public interface IAlert: IList<IAlertItem>, IAlertItem
    {
        string Title { get; }
    }

    internal class Alert : List<IAlertItem>, IAlert 
    {
        public string Title
        {
            get
            {
                var title = (from item in this
                             where item is AlertTextBlock
                             select item as AlertTextBlock).FirstOrDefault();
                return title == null ? string.Empty : title.ToString();
            }
        }

    }
}
