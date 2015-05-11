using System;
using System.Collections.Generic;
using System.Text;

namespace FluentAlerts
{
    public delegate void FluentAlertSerializeDelegate(object source,StringBuilder results, Action<object> serializeObject);
    public class FluentAlertSerializer : IFluentAlertSerializer
    {
        //TODO: Implement an ordered list of is subclass or inherits types, for general serialization overrides
        private readonly StringBuilder _results = new StringBuilder();
        private FluentAlertSerializeDelegate _defaultSerializer = InnerDefaultSerializer;
        private readonly Dictionary<Type, FluentAlertSerializeDelegate> _typeSerializerBindings = new Dictionary<Type, FluentAlertSerializeDelegate>();

        public string Serialize(object source)
        {
            PreSerializationHook(source, _results);
            InnerSerialize(source);
            PostSerializationHook(source, _results);
            return _results.ToString();
        }

        /// <summary>
        /// Load all Serialization Bindings Here
        /// </summary>
        protected virtual void Load(){}
        protected virtual void PreSerializationHook(object source, StringBuilder results) { }
        protected virtual void PostSerializationHook(object source, StringBuilder results) { }

        #region Binders
        protected FluentAlertSerializer BindSerializationOf<T>(FluentAlertSerializeDelegate serializer)
        {
            return BindSerializationOf(typeof(T), serializer);
        }

        protected FluentAlertSerializer BindSerializationOf(Type type, FluentAlertSerializeDelegate serializer)
        {
            if (type != null && serializer != null)
                _typeSerializerBindings[type] = serializer;

            return this;
        }

        protected FluentAlertSerializer BindDefaultSerializationTo(FluentAlertSerializeDelegate serializer)
        {
            if (serializer != null)
                _defaultSerializer = serializer;

            return this;
        }
        #endregion

        private void InnerSerialize(object o)
        {
            GetSerializerFor(o)(o, _results, InnerSerialize);
        }

        private FluentAlertSerializeDelegate GetSerializerFor(object o)
        {
            var type = o.GetType();
            return (_typeSerializerBindings.ContainsKey(type))
                ? _typeSerializerBindings[type]
                : _defaultSerializer;
        }

        private static void InnerDefaultSerializer(object source, StringBuilder results, Action<object> serializeObject)
        {
            //TODO: use transformers, this is just a place holder
            results.Append(source);
        }
    }
}
