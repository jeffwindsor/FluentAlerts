using FluentAlerts.Transformers;
 using FluentAlerts.Transformers.Strategies;
 using FluentAlerts.Transformers.TypeFormatters;

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
        private readonly ITypeFormatter<TResult> _typeFormatter;

        protected AlertSerializer(ITransformer transformer,
            ITransformStrategy transformStrategy,
            ITypeFormatter<TResult> typeFormatter)
        {
            _transformer = transformer;
            _transformStrategy = transformStrategy;
            _typeFormatter = typeFormatter;
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

            var alert = item as IAlert;
            if (alert != null)
            {
                Add(alert);
            }
            else
            {
                var @group = item as AlertGroup;
                if (@group != null)
                {
                    Add(@group);
                }
                else
                {
                    var block = item as AlertTextBlock;
                    if (block != null)
                    {
                        Add(block);
                    }
                }
            }
        }

        protected virtual void AddValue(object value)
        {
            //Route value by type
            var item = value as IAlert;
            if (item != null)
            { 
                //Embedded Alert in Group, send to base for routing
                Add(item);
            }
            else
            {
                var alertItem = value as IAlertItem;
                if (alertItem != null)
                {
                    //Embedded Alert Item in Group, send to base for routing
                    Add(alertItem);
                }
                else
                {
                    if (_transformStrategy.IsTransformRequired(value,0))
                    {
                        //If object qualifies, transform object into an alert, and add 
                        Add(_transformer.Transform(value, _transformStrategy, 0));
                    }
                    else 
                    {
                        //otherwise add formatted value
                        Add(_typeFormatter.Format(value));
                    }
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
