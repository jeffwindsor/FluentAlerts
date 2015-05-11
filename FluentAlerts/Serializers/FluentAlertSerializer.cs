using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FluentAlerts.Serializers
{
    //TODO: Implement an ordered list of is subclass or inherits types, for general serialization overrides
    public abstract class FluentAlertSerializer : IFluentAlertSerializer
    {
        public delegate void CustomSerializer<in T>(T source, StringBuilder result, Action<object, StringBuilder> serialize);
        public delegate void CustomSerializer(object source, StringBuilder result, Action<object, StringBuilder> serialize);
        public delegate IEnumerable CustomListTransformer<in T>(T list) where T : IEnumerable;
        public delegate IEnumerable CustomItemTransformer<in T>(T item);
        public delegate string CustomItemToTextTransformer<in T>(T item);
        public delegate bool SerializationRule(object o, MemberPath path);

        //private readonly StringBuilder _results = new StringBuilder();
        private readonly Dictionary<Type, CustomSerializer> _customSerializers = new Dictionary<Type, CustomSerializer>();
        //private readonly List<SerializationRule> _stopSerializationRules = new List<SerializationRule>();

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
            var serialize = GetSerializerFor(source);
            serialize(source, result, InnerSerialize);
        }

        protected virtual void PreSerializationHook(StringBuilder results) { }
        protected virtual void PostSerializationHook(StringBuilder results) { }
        protected virtual void DefaultSerializer(object source, StringBuilder result, Action<object, StringBuilder> serializeCallback)
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

        protected FluentAlertSerializer SerializeWith<T>(CustomSerializer<T> itemCustomSerializer)
        {
            //Typed Serialization
            return SerializeWith(typeof(T),
                (source, result, serialize) =>
                {
                    if (itemCustomSerializer != null) itemCustomSerializer((T)source, result, serialize);
                });
        }

        protected FluentAlertSerializer SerializeWith<T>(string prefix, string postfix, CustomItemTransformer<T> itemTransformer = null)
        {
            return SerializeWith(typeof (T),
                (source, result, serialize) =>
                {
                    //Wrap item in the given pre and post fix text, then call give item serializer on items given by transformation of source
                    result.Append(prefix);
                    if (itemTransformer != null)
                    {
                        foreach (var item in itemTransformer((T) source))
                        {
                            serialize(item, result);
                        }
                    }
                    result.Append(postfix);
                });
        }
        protected FluentAlertSerializer SerializeWith<T>(CustomItemToTextTransformer<T> prefix, CustomItemToTextTransformer<T> postfix, CustomItemTransformer<T> itemTransformer = null)
        {
            return SerializeWith(typeof(T),
                (source, result, serialize) =>
                {
                    var typedSource = (T)source;
                    //Wrap item in the given pre and post fix text, then call give item serializer on items given by transformation of source
                    if (prefix != null) result.Append(prefix(typedSource));
                    if (itemTransformer != null)
                    {
                        foreach (var item in itemTransformer(typedSource))
                        {
                            serialize(item, result);
                        }
                    }
                    if (postfix != null) result.Append(postfix(typedSource));
                });
        }

        protected FluentAlertSerializer SerializeWith<T>(string prefix, string postfix, CustomSerializer<T> itemCustomSerializer)
        {
            return SerializeWith(typeof(T),
                (source, result, serialize) =>
                {
                    //Wrap item in the given pre and post fix text, then call give item serializer on T
                    var item = (T)source;
                    result.Append(prefix);
                    if (itemCustomSerializer != null) itemCustomSerializer(item, result, serialize);
                    result.Append(postfix);
                });
        }
        protected FluentAlertSerializer SerializeWith<T>(CustomItemToTextTransformer<T> prefix, CustomItemToTextTransformer<T> postfix, CustomSerializer<T> itemCustomSerializer)
        {
            return SerializeWith(typeof(T),
                (source, result, serialize) =>
                {
                    //Wrap item in the given pre and post fix text, then call give item serializer on T
                    var item = (T)source;
                    if (prefix != null) result.Append(prefix(item));
                    if (itemCustomSerializer != null) itemCustomSerializer(item, result, serialize);
                    if (postfix != null) result.Append(postfix(item));
                });
        }

        protected FluentAlertSerializer SerializeAsListWith<T>(string prefix, string postfix, CustomListTransformer<T> transformer = null) where T : IEnumerable
        {
            return SerializeWith(typeof(T),
                (source, result, serialize) =>
                {
                    var list = (T) source;
                    var items = (transformer == null) ? list : transformer(list);
                    //Wrap serialization of list items in the given pre and post fix text, inside wrapper call serialization on all list items
                    result.Append(prefix);
                    foreach (var item in items)
                    {
                        serialize(item, result);
                    }
                    result.Append(postfix);
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
