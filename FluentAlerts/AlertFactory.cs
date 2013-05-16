using System.Collections.Generic;

namespace FluentAlerts
{
    public interface IAlertFactory
    {
        IAlert Create(AlertStyle style, IList<IAlertItem> items);
    }
    
    public class GenericNotificationFactory<T> : IAlertFactory where T : IAlert, new()
    {
        public IAlert Create(AlertStyle style, IList<IAlertItem> items)
	    {
		    var result = new T { Style = style };
		    if (items != null) 
            {
			    foreach (var item in items) 
                {
				    result.Add(item);
			    }
		    }
		    return result;
	    }
    }

}
