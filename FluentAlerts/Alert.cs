 using System.Collections.Generic;
using System.Linq;

namespace FluentAlerts
{
    /// <summary>
    /// Alert as simple list of items 
    /// </summary>
    internal class Alert : List<IAlertItem>, IAlert
    {
        public IEnumerable<T> GetAlertItems<T>() where T : AlertItem
        {
            return this.Where(i => i is T).Cast<T>();
        }

        public string Title
        {
            get
            {
                var item = GetAlertItems<TextAlertItem>().FirstOrDefault(i => i.Style == TextStyle.HeaderOne);
                return (item == null) ? string.Empty : item.Text;
            }
        }
    }
}
