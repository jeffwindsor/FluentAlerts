 using FluentAlerts.Formatters;
 using FluentAlerts.Transformers;

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
        //Allows for the transformation of objects to an alert
        private readonly ITransformer _transformer;
        //Determines if a value is transformed or formated
        private readonly ITransformStrategy _transformStrategy;
        //Allows for the formatting of objects to strings
        private readonly IFormatter<TResult> _formatter;

        protected AlertSerializer(ITransformer transformer,
            ITransformStrategy transformStrategy,
            IFormatter<TResult> formatter)
        {
            _transformer = transformer;
            _transformStrategy = transformStrategy;
            _formatter = formatter;
        }

        public TResult Serialize(IAlert alert)
        {
            BeginSerialization();
            Add(alert);
            EndSerialization();
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
                Add(item);
            }
        }

        /// <summary>
        /// Routes the item to its correct type.
        /// MultiMethods Alternative
        /// </summary>
        protected virtual void Add(IAlertItem item)
        {
            if (item == null) return;

            if (item is IAlert)              { Add((IAlert)item);}
            else if (item is AlertGroup)     { Add((AlertGroup)item);}
            else if (item is AlertTextBlock) { Add((AlertTextBlock)item);}
        }

        protected virtual void AddValue(object value)
        {
            //Route value by type
            if (value is IAlert)
            {
                //Embedded Alert in Group, send to base for routing
                Add((IAlert)value);
            }
            else if (value is IAlertItem)
            {
                //Embedded Alert Item in Group, send to base for routing
                Add((IAlertItem)value);
            }
            else
            {
                if (_transformStrategy.IsTransformRequired(value))
                {
                    //If object qualifies, transform object into an alert, and add 
                    Add(_transformer.Transform(value,_transformStrategy));
                }
                else
                {
                    //otherwise add formatted value
                    Add(_formatter.Format(value));
                }
            }
        }
        protected abstract void Add(TResult value);
        protected abstract void BeginSerialization();
        protected abstract void BeginAlert();
        protected abstract void Add(AlertGroup g);
        protected abstract void Add(AlertTextBlock textBlock);
        protected abstract void EndAlert();
        protected abstract void EndSerialization();
        protected abstract TResult GetResult();
    }
}
