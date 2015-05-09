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
        public Alert(){}

        public Alert(List<IAlertItem> items)
        {
            if (items == null) return;
            AddRange(items);
        }

        public IEnumerable<T> GetAlertItems<T>() where T : AlertItem
        {
            return this.Where(i => i is T).Cast<T>();
        }

        public string Title
        {
            get
            {
                var item = GetAlertItems<AlertItemValueList>().FirstOrDefault(i => i.Style == AlertItemValueStyle.Title);
                return (item != null && item.Values[0] != null) 
                    ? Convert.ToString(item.Values[0])
                    : string.Empty;
            }
        }
    }
}
