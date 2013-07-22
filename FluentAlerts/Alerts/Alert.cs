 using System;
 using System.Collections.Generic;
using System.Linq;

namespace FluentAlerts
{
    /// <summary>
    /// Alert as simple list of items 
    /// </summary>
    public class Alert : List<IAlertItem>, IAlert
    {
        public IEnumerable<T> GetAlertItems<T>() where T : AlertItem
        {
            return this.Where(i => i is T).Cast<T>();
        }

        public string Title
        {
            get
            {
                var item = GetAlertItems<ValueListAlertItem>().FirstOrDefault(i => i.Style == ValueStyle.Title);
                return (item != null && item.Values[0] != null) 
                    ? Convert.ToString(item.Values[0])
                    : string.Empty;
            }
        }
    }
}
