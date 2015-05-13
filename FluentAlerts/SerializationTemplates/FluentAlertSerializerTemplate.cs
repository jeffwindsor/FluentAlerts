using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using FluentAlerts.Builders;
using FluentAlerts.Domain;
using FluentAlerts.Extensions;

namespace FluentAlerts
{
    public abstract class FluentAlertSerializerTemplate
    {
        protected delegate bool Predicate(object o);
        protected delegate void SerializerCallBack(object source, StringBuilder result);
        protected delegate void CustomSerializer(object source, StringBuilder result, SerializerCallBack serializerCallback);
        protected delegate void CustomTypeSerializer<in T>(T source, StringBuilder result);
        protected delegate void CustomTypeSerializerWithCallBack<in T>(T source, StringBuilder result, SerializerCallBack serializerCallback);
        protected delegate string TextMap<in T>(T item);
        protected delegate IEnumerable ItemMap<in T>(T item);
        protected delegate IEnumerable EnumerableMap<in T>(T list) where T : IEnumerable;
        
        private readonly Dictionary<Type, CustomSerializer> _customTypeSerializers = new Dictionary<Type, CustomSerializer>();
        private readonly CustomMatchSerializers _customMatchSerializers = new CustomMatchSerializers();

        protected FluentAlertSerializerTemplate()
        {
            //Standard Builder Overrides, makes sure the builders are converted to output objects before serialization, just in case
            SerializeTypeWith<FluentDocumentBuilder>((source, result, callback) => callback(source.ToDocument(), result));
            SerializeTypeWith<FluentTextBlockBuilder>((source, result, callback) => callback(source.ToTextBlock(), result));
            SerializeTypeWith<FluentTableBuilder>((source, result, callback) => callback(source.ToTable(), result));

            //Default Value Type rule
//            SerializeMatchWith(source => true, DefaultSerializers.SerializeToString);   // Default rule, object to Public Property NameValue Table
            SerializeMatchWith(source => true, DefaultSerializers.SerializeToPublicPropertiesPropertyNameValueIntoNestedTables);   // Default rule, object to Public Property NameValue Table
            SerializeMatchWith(source => source.GetType().IsValueType, DefaultSerializers.SerializeToString);  // Value types to string           
        }
        
        public virtual void PreSerializationHook(StringBuilder result) { }
        public virtual void PostSerializationHook(StringBuilder result) { }

        public void Serialize(object source, StringBuilder result)
        {
            var serializer = GetSerializer(source);
            serializer(source, result, Serialize);
        }

        protected CustomSerializer GetSerializer(object source)
        {
            //Catch any null value
            if (source == null)
                return DefaultSerializers.SerializeToNullString;
            
            //Search for type match
            var type = source.GetType();
            if (_customTypeSerializers.ContainsKey(type))
                return _customTypeSerializers[type];
            
            //Search for Predicate rule match, otherwise pretty type name
            var csp = _customMatchSerializers.FindMatch(source);
            return csp ?? DefaultSerializers.SerializeToPrettyTypeName;
        }

        #region SerializeWith
        protected void ClearAllCustomSerializers()
        {
            ClearCustomTypeSerializers();
            ClearCustomMatchSerializers();
        }
        protected void ClearCustomTypeSerializers()
        {
            Debug.Print("ClearCustomTypeSerializers");
            _customTypeSerializers.Clear();
        }
        protected void ClearCustomMatchSerializers()
        {
            Debug.Print("ClearCustomMatchSerializers");
            _customMatchSerializers.Clear();
        }

        /// <summary>
        /// Adds match rule, LIFO checking
        /// </summary>
        /// <param name="match"></param>
        /// <param name="customSerializer"></param>
        /// <returns></returns>
        protected void SerializeMatchWith(Predicate match, CustomSerializer customSerializer)
        {
            if (match != null && customSerializer != null)
                _customMatchSerializers.Insert(0, new CustomMatchSerializer { Predicate = match, CustomSerializer = customSerializer });
        }

        protected void SerializeTypeWith(Type type, CustomSerializer customSerializer)
        {
            if (type != null && customSerializer != null)
                _customTypeSerializers[type] = customSerializer;
        }

        protected void SerializeTypeWith<T>(CustomTypeSerializer<T> customTypeSerializer)
        {
            //Typed Serialization
            SerializeTypeWith(typeof(T),
                (source, result, serializerCallBack) =>
                {
                    Debug.Print("SerializeTypeWith > CustomTypeSerializer<T>");
                    if (customTypeSerializer != null) customTypeSerializer((T)source, result);
                });
        }
        protected void SerializeTypeWith<T>(CustomTypeSerializerWithCallBack<T> customTypeSerializer)
        {
            //Typed Serialization
            SerializeTypeWith(typeof(T),
                (source, result, serializerCallBack) =>
                {
                    Debug.Print("SerializeTypeWith > CustomTypeSerializerWithCallBack<T>");
                    if (customTypeSerializer != null) customTypeSerializer((T)source, result, serializerCallBack);
                });
        }

        protected void SerializeTypeWith<T>(string prefix, string postfix, ItemMap<T> itemMap = null)
        {
            SerializeTypeWith(source => prefix, source => postfix, itemMap);
        }
        protected void SerializeTypeWith<T>(TextMap<T> prefix, TextMap<T> postfix, ItemMap<T> itemMap = null)
        {
            SerializeTypeWith(prefix,postfix, (source, result, serializerCallBack) =>
            {
                if (itemMap == null) return;
                Debug.Print("SerializeTypeWith > ItemMap");
	            var items = itemMap(source).Cast<object>().ToArray();
                foreach (var item in items)
                {
                    Debug.Print("  MapItem: {0}", item);
	                if (item == null) continue;
	                if (item.GetType().IsArray)
	                {
		                var contents = item as object[];
		                if (contents == null) continue;
		                if ((item as object[]).Length == 0)
		                {
			                result.Append("Empty");
							continue;
		                }
	                }
					serializerCallBack(item, result);
                }
            });
        }

        protected void SerializeTypeWith<T>(string prefix, string postfix, CustomTypeSerializer<T> innerCustomTypeSerializer)
        {
            SerializeTypeWith(source => prefix, source => postfix, innerCustomTypeSerializer);
        }
        protected void SerializeTypeWith<T>(TextMap<T> prefix, TextMap<T> postfix, CustomTypeSerializer<T> innerCustomTypeSerializer)
        {
            SerializeTypeWith(prefix,postfix, (source, result, serializerCallBack) =>
            {
                Debug.Print("SerializeTypeWith > CustomTypeSerializer<T> INNER");
                if (innerCustomTypeSerializer != null) innerCustomTypeSerializer(source, result);
            });
        }

        protected void SerializeTypeWith<T>(string prefix, string postfix, CustomTypeSerializerWithCallBack<T> innerCustomTypeSerializer)
        {
            SerializeTypeWith(source => prefix, source => postfix, innerCustomTypeSerializer);
        }
        protected void SerializeTypeWith<T>(TextMap<T> prefix, TextMap<T> postfix, CustomTypeSerializerWithCallBack<T> innerCustomTypeSerializer)
        {
            SerializeTypeWith(typeof(T),
                (source, result, serializerCallBack) =>
                {
                    //Wrap item in the given pre and post fix text, then call give item serializer on T
                    var item = (T)source;
                    Debug.Print("SerializeTypeWith > CustomTypeSerializerWithCallBack<T> INNER for {0}", item);
                    if (prefix != null) result.Append(prefix(item));
                    if (innerCustomTypeSerializer != null) innerCustomTypeSerializer(item, result, serializerCallBack);
                    if (postfix != null) result.Append(postfix(item));
                });
        }

        protected void SerializeTypeAsListWith<T>(string prefix, string postfix,
            EnumerableMap<T> enumerableMap = null, SerializerCallBack serializerCallBackOverride = null)
            where T : IEnumerable
        {
            SerializeTypeAsListWith(source => prefix, source => postfix, enumerableMap, serializerCallBackOverride);
        }

        protected void SerializeTypeAsListWith<T>(TextMap<T> prefix, TextMap<T> postfix,
            EnumerableMap<T> enumerableMap = null, SerializerCallBack serializerCallBackOverride = null)
            where T : IEnumerable
        {
            SerializeTypeWith(typeof (T),
                (source, result, serializerCallBack) =>
                {
                    Debug.Print("SerializeTypeAsListWith");
                    var list = (T) source;
                    var items = (enumerableMap == null) ? list : enumerableMap(list);
                    if (serializerCallBackOverride == null)
                    {
                        Debug.Print("  SerializerCallBackOverride not given");
                        serializerCallBackOverride = serializerCallBack;
                    }
                    //Wrap serialization of list items in the given pre and post fix text, inside wrapper call serialization on all list items
                    if (prefix != null) result.Append(prefix(list));
                    foreach (var item in items)
                    {
                        Debug.Print("  ListItem: {0}", item);
                        serializerCallBackOverride(item, result);
                    }
                    if (postfix != null) result.Append(postfix(list));
                });
        }
        #endregion

        protected static class DefaultSerializers
        {
            public static void SerializeAsListToRows(object source, StringBuilder result,SerializerCallBack serializeCallback)
            {
                Debug.Print("SerializeAsListToRows");
                var table = new Table(from object item in (source as IEnumerable) select new Row(new Cell {Content = item}));
                serializeCallback(table, result);
            }

            public static void SerializeToPublicPropertiesPropertyNameValueIntoNestedTables(object source, StringBuilder result,SerializerCallBack serializeCallback)
            {
                Debug.Print("SerializeToPublicPropertiesPropertyNameValueIntoNestedTables");
                var type = source.GetType();
                var table = new Table(from pi in type.GetProperties()
                    select new Row(new Cell { Content = pi.Name }, new Cell { Content = pi.GetValue(source) }));
                table.Insert(0, new Row(new HeaderCell{Content = type.ToPrettyName()}));
                serializeCallback(table, result);
            }

            public static void SerializeToString(object source, StringBuilder result, SerializerCallBack serializeCallback)
            {
                Debug.Print("SerializeToString");
                result.Append(source);
            }

            public static void SerializeToPrettyTypeName(object source, StringBuilder result, SerializerCallBack serializeCallback)
            {
                Debug.Print("SerializeToPrettyTypeName");
                result.Append(source.GetType().ToPrettyName());
            }

            public static void SerializeToNullString(object source, StringBuilder result, SerializerCallBack serializeCallback)
            {
                Debug.Print("SerializeToNullString");
                result.Append("Null");
            }
        }

        #region Classes
        private class CustomMatchSerializer
        {
            public Predicate Predicate { get; set; }
            public CustomSerializer CustomSerializer { get; set; }

            public bool IsMatch(object o)
            {
                return Predicate(o);
            }
        }

        private class CustomMatchSerializers: List<CustomMatchSerializer>
        {
            public CustomSerializer FindMatch(object o)
            {
                var found = this.FirstOrDefault(p => p.IsMatch(o));
                return (found == null) ? null : found.CustomSerializer ;
            }
        }
        #endregion
    }
}