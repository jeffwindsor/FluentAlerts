using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FluentAlerts.Serializers
{
    //TODO: Implement an ordered list of is subclass or inherits types, for general serialization overrides
    public class FluentAlertSerializer : IFluentAlertSerializer
    {
        public delegate void Serializer(object source, StringBuilder result, Action<object, StringBuilder> serializeCallback);
        public delegate bool SerializationRule(object o, MemberPath path);

        //private readonly StringBuilder _results = new StringBuilder();
        private readonly Dictionary<Type, Serializer> _typeSerializerBindings = new Dictionary<Type, Serializer>();
        private Serializer _defaultSerializer = InnerDefaultSerializer;
        //private readonly List<SerializationRule> _stopSerializationRules = new List<SerializationRule>();

        public string Serialize(object source)
        {
            var result = new StringBuilder();
            PreSerializationHook(source, result);
            InnerSerialize(source, result);
            PostSerializationHook(source, result);
            return result.ToString();
        }

        protected virtual void PreSerializationHook(object source, StringBuilder results) { }
        protected virtual void PostSerializationHook(object source, StringBuilder results) { }

        #region Bindings
        protected FluentAlertSerializer BindSerializationOf<T>(Serializer serializer)
        {
            return BindSerializationOf(typeof(T), serializer);
        }

        protected FluentAlertSerializer BindSerializationOf(Type type, Serializer serializer)
        {
            if (type != null && serializer != null)
                _typeSerializerBindings[type] = serializer;

            return this;
        }

        //protected FluentAlertSerializer BindDefaultSerializationTo(FluentAlertSerializeDelegate serializer)
        //{
        //    if (serializer != null)
        //        _defaultSerializer = serializer;

        //    return this;
        //}
        #endregion


        private void InnerSerialize(object source, StringBuilder result)
        {
            var serialize = GetSerializerFor(source);
            serialize(source, result, InnerSerialize);
        }

        //private bool IsSerializationStopped(object source)
        //{
        //    return _stopSerializationRules.Any(rule => rule(source, path));
        //}
        private Serializer GetSerializerFor(object source)
        {
            var type = source.GetType();
            return (_typeSerializerBindings.ContainsKey(type))
                ? _typeSerializerBindings[type]
                : _defaultSerializer;
        }

        private static void InnerDefaultSerializer(object source, StringBuilder result, Action<object, StringBuilder> serializeCallback)
        {
            //TODO: use transformers, this is just a place holder
            result.Append(source);
        }
    }
}
