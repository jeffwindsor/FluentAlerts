using FluentAlerts.Transformers;
using FluentAlerts.Transformers.Formatters;
using FluentAlerts.Transformers.Strategies;

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
        private readonly ITransformer<TResult> _transformer;
        protected AlertSerializer(ITransformer<TResult> transformer)
        {
            _transformer = transformer;
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
        private void Add(IAlert alert)
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
        private void Add(IAlertItem item)
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

        protected void RouteGroupValue(object value)
        {
            //No multimethods, so route by type
            var alert = value as IAlert;
            if (alert != null)
            {
                Add(alert);
                return;
            }

            if (IsResultType(value))
            {
                Add((TResult) value);
                return;
            }

            //Transform the object and re-route 
            RouteGroupValue(_transformer.Transform(value));
        }

        protected abstract bool IsResultType(object value);
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
