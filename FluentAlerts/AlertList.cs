using System.Collections.Generic;
using System.Linq;

namespace FluentAlerts
{
    public interface IAlert: IList<IAlertItem>, IAlertItem
    {
        string Title { get; }
    }

    internal class AlertList : List<IAlertItem>, IAlert 
    {
        public IEnumerable<T> GetAlertItems<T>() where T: AlertItem
        {
            return this.Where(i => i is T).Cast<T>();
        }

        public string Title
        {
            get
            {
                var item = GetAlertItems<TextItem>().FirstOrDefault(i => i.Style==TextStyle.HeaderOne);
                return (item == null) ? string.Empty : item.Text; 
            }
        }

    }
}
