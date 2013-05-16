namespace FluentAlerts.Serializers
{
    public interface IAlertSerializer<out TResult>
    {
        /// <summary>
        /// Serializes the Alert into the TResult type, using derived class methodology
        /// </summary>
        TResult Serialize(IAlert alert);
    }

    public abstract class AlertSerializer<TResult> : IAlertSerializer<TResult>
    {
        public TResult Serialize(IAlert alert)
        {
            BeginSerialization(alert.Style);
            Add(alert);
            EndSerialization(alert.Style);
            return GetResult();
        }

        /// <summary>
        /// Append all the items in the note using the notes style
        /// </summary>
        protected virtual void Add(IAlert alert)
        {
            if (alert == null) return;
            foreach (var item in alert)
            {
                Add(item, alert.Style);
            }
        }

        /// <summary>
        /// Routes the item to its correct type.
        /// MultiMethods Alternative
        /// </summary>
        protected virtual void Add(IAlertItem item, AlertStyle style)
        {
            if (item == null) return;

            if (item is IAlert)              { Add((IAlert)item);}
            else if (item is AlertGroup)     { Add((AlertGroup)item, style);}
            else if (item is AlertTextBlock) { Add((AlertTextBlock)item, style);}
        }

        protected abstract void BeginSerialization(AlertStyle style);
        protected abstract void BeginAlert(AlertStyle style);
        protected abstract void Add(AlertGroup g, AlertStyle style);
        protected abstract void Add(AlertTextBlock textBlock, AlertStyle style);
        protected abstract void EndAlert(AlertStyle style);
        protected abstract void EndSerialization(AlertStyle style);
        protected abstract TResult GetResult();
    }
}
