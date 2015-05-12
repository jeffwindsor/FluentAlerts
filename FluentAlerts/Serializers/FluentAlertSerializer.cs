using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using FluentAlerts.Builders;

namespace FluentAlerts.Serializers
{
    //TODO: Implement an ordered list of is subclass or inherits types, for general serialization overrides
    public abstract class FluentAlertSerializer : IFluentAlertSerializer
    {
        public delegate void SerializerCallBack(object source, StringBuilder result);
        public delegate void CustomSerializer<in T>(T source, StringBuilder result);
        public delegate void CustomSerializerWithCallBack<in T>(T source, StringBuilder result, SerializerCallBack serializerCallback);
        public delegate void CustomSerializer(object source, StringBuilder result, SerializerCallBack serializerCallback);
        public delegate IEnumerable EnumerableMap<in T>(T list) where T : IEnumerable;
        public delegate IEnumerable ItemMap<in T>(T item);
        public delegate string TextMap<in T>(T item);
        public delegate bool SerializationRule(object o, MemberPath path);

        //private readonly StringBuilder _results = new StringBuilder();
        private readonly Dictionary<Type, CustomSerializer> _customSerializers = new Dictionary<Type, CustomSerializer>();
        //private readonly List<SerializationRule> _stopSerializationRules = new List<SerializationRule>();

        protected FluentAlertSerializer()
        {
            //Standard Builder Overrides, makes sure they are converted before serialization
            SerializeWith<FluentDocumentBuilder>((source, result, callback) => callback(source.ToDocument(), result));
            SerializeWith<FluentTextBlockBuilder>((source, result, callback) => callback(source.ToTextBlock(), result));
            SerializeWith<FluentTableBuilder>((source, result, callback) => callback(source.ToTable(), result));
        }

        public string Serialize(object source)
        {
            var result = new StringBuilder();
            PreSerializationHook(result);
            InnerSerialize(source, result);
            PostSerializationHook(result);
            return result.ToString();
        }
        private void InnerSerialize(object source, StringBuilder result)
        {
            var serializer = GetSerializerFor(source);
            serializer(source, result, InnerSerialize);
        }

        protected virtual void PreSerializationHook(StringBuilder results) { }
        protected virtual void PostSerializationHook(StringBuilder results) { }
        protected virtual void DefaultSerializer(object source, StringBuilder result, SerializerCallBack serializeCallback)
        {
            //TODO: use transformers, this is just a place holder
            result.Append(source);
        }

        #region SerializeWith
        protected FluentAlertSerializer SerializeWith(Type type, CustomSerializer customSerializer)
        {
            if (type != null && customSerializer != null)
                _customSerializers[type] = customSerializer;

            return this;
        }

        protected FluentAlertSerializer SerializeWith<T>(CustomSerializer<T> customSerializer)
        {
            //Typed Serialization
            return SerializeWith(typeof(T),
                (source, result, serializerCallBack) =>
                {
                    if (customSerializer != null) customSerializer((T)source, result);
                });
        }
        protected FluentAlertSerializer SerializeWith<T>(CustomSerializerWithCallBack<T> customSerializer)
        {
            //Typed Serialization
            return SerializeWith(typeof(T),
                (source, result, serializerCallBack) =>
                {
                    if (customSerializer != null) customSerializer((T)source, result, serializerCallBack);
                });
        }

        protected FluentAlertSerializer SerializeWith<T>(string prefix, string postfix, ItemMap<T> itemMap = null)
        {
            return SerializeWith(source => prefix, source => postfix, itemMap);
        }
        protected FluentAlertSerializer SerializeWith<T>(TextMap<T> prefix, TextMap<T> postfix, ItemMap<T> itemMap = null)
        {
            return SerializeWith(prefix,postfix, (source, result, serializerCallBack) =>
            {
                if (itemMap == null) return;
                foreach (var item in itemMap(source))
                {
                    serializerCallBack(item, result);
                }
            });
        }

        protected FluentAlertSerializer SerializeWith<T>(string prefix, string postfix, CustomSerializer<T> innerCustomSerializer)
        {
            return SerializeWith(source => prefix, source => postfix, innerCustomSerializer);
        }
        protected FluentAlertSerializer SerializeWith<T>(TextMap<T> prefix, TextMap<T> postfix, CustomSerializer<T> innerCustomSerializer)
        {
            return SerializeWith(prefix,postfix, (source, result, serializerCallBack) =>
                {
                    if (innerCustomSerializer != null) innerCustomSerializer(source, result);
                });
        }
        
        protected FluentAlertSerializer SerializeWith<T>(string prefix, string postfix, CustomSerializerWithCallBack<T> innerCustomSerializer)
        {
            return SerializeWith(source => prefix, source => postfix, innerCustomSerializer);
        }
        protected FluentAlertSerializer SerializeWith<T>(TextMap<T> prefix, TextMap<T> postfix, CustomSerializerWithCallBack<T> innerCustomSerializer)
        {
            return SerializeWith(typeof(T),
                (source, result, serializerCallBack) =>
                {
                    //Wrap item in the given pre and post fix text, then call give item serializer on T
                    var item = (T)source;
                    if (prefix != null) result.Append(prefix(item));
                    if (innerCustomSerializer != null) innerCustomSerializer(item, result, serializerCallBack);
                    if (postfix != null) result.Append(postfix(item));
                });
        }

        protected FluentAlertSerializer SerializeAsListWith<T>(string prefix, string postfix,
            EnumerableMap<T> enumerableMap = null, SerializerCallBack serializerCallBackOverride = null)
            where T : IEnumerable
        {
            return SerializeAsListWith(source => prefix, source => postfix, enumerableMap, serializerCallBackOverride);
        }

        protected FluentAlertSerializer SerializeAsListWith<T>(TextMap<T> prefix, TextMap<T> postfix,
            EnumerableMap<T> enumerableMap = null, SerializerCallBack serializerCallBackOverride = null)
            where T : IEnumerable
        {
            return SerializeWith(typeof (T),
                (source, result, serializerCallBack) =>
                {
                    var list = (T) source;
                    var items = (enumerableMap == null) ? list : enumerableMap(list);
                    if (serializerCallBackOverride == null) serializerCallBackOverride = serializerCallBack;
                    //Wrap serialization of list items in the given pre and post fix text, inside wrapper call serialization on all list items
                    if (prefix != null) result.Append(prefix(list));
                    foreach (var item in items)
                    {
                        serializerCallBackOverride(item, result);
                    }
                    if (postfix != null) result.Append(postfix(list));
                });
        }

        //protected FluentAlertSerializer BindDefaultSerializationTo(FluentAlertSerializeDelegate serializer)
        //{
        //    if (serializer != null)
        //        _defaultSerializer = serializer;

        //    return this;
        //}
        #endregion

        //private bool IsSerializationStopped(object source)
        //{
        //    return _stopSerializationRules.Any(rule => rule(source, path));
        //}
        private CustomSerializer GetSerializerFor(object source)
        {
            var type = source.GetType();
            return (_customSerializers.ContainsKey(type))
                ? _customSerializers[type]
                : DefaultSerializer;
        }
    }
}
