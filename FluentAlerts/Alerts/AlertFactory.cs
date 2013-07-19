using System.Collections.Generic;

namespace FluentAlerts
{
    /// <summary>
    /// Generic based alert factory for any IAlert implmenting concrete class
    /// that has a parameterless constructor
    /// </summary>
    /// <typeparam name="T">Concrete Alert Implementation</typeparam>
    public class AlertFactory<T> : IAlertFactory where T : IAlert, new()
    {
        public IAlert Create(IList<IAlertItem> items)
	    {
		    var result = new T();
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
