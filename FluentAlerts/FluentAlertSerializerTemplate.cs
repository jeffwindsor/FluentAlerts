using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentAlerts.Builders;
using FluentAlerts.Domain;
using FluentAlerts.Extensions;

namespace FluentAlerts
{
    public delegate string TextMap<in T>(T item);
    public abstract class FluentAlertSerializerTemplate
    {
        protected delegate bool Predicate(object o);
        //protected delegate void SerializerCallBack(object source, StringBuilder result);
        protected delegate void CustomSerializer(object source, StringBuilder result); //, SerializerCallBack serializerCallback);
        protected delegate void CustomTypeSerializer<in T>(T source, StringBuilder result);
        //protected delegate void CustomTypeSerializerWithCallBack<in T>(T source, StringBuilder result); //, SerializerCallBack serializerCallback);
        
        protected delegate IEnumerable ItemMap<in T>(T item);
        protected delegate IEnumerable EnumerableMap<in T>(T list) where T : IEnumerable;

        private readonly Dictionary<Type, CustomSerializer> _customTypeSerializers = new Dictionary<Type, CustomSerializer>();
        private readonly CustomMatchSerializers _customMatchSerializers = new CustomMatchSerializers();

        protected FluentAlertSerializerTemplate()
        {
            //Standard Builder Overrides, makes sure the builders are converted to output objects before serialization, just in case
            SerializeTypeWith<FluentDocumentBuilder>((source, result) => Serialize(source.ToDocument(), result));
            SerializeTypeWith<FluentTextBlockBuilder>((source, result) => Serialize(source.ToTextBlock(), result));
            SerializeTypeWith<FluentTableBuilder>((source, result) => Serialize(source.ToTable(), result));

            //Default Value Type rule
            SerializeTypeWith<Guid>(SerializeAsToString);
            SerializeTypeWith<DateTime>(SerializeAsToString);
            SerializeTypeWith<string>((source,result) => result.Append(source));

            //Matches
            SerializeMatchWith(Match.Anything, SerializeAsPropertyNameValueTable);   // Default rule, object to Public Property NameValue Table         
            SerializeMatchWith(Match.EnumsAndPrimitives, SerializeAsToString);  // Primitive and Enums
            SerializeMatchWith(Match.Exceptions, (source,result) =>  SerializeException(source, result, "Message", "StackTrace", "InnerException"));
        }
        
        public virtual void PreSerializationHook(StringBuilder result) { }
        public virtual void PostSerializationHook(StringBuilder result) { }

        public void Serialize(object source, StringBuilder result)
        {
            var serializer = GetSerializer(source);
            serializer(source, result);
        }

        protected CustomSerializer GetSerializer(object source)
        {
            //Catch any null value
            if (source == null)
                return SerializeAsNull;

            //Search for type match
            var type = source.GetType();
            if (_customTypeSerializers.ContainsKey(type))
                return _customTypeSerializers[type];
            
            //Search for Predicate rule match, otherwise pretty type name
            var csp = _customMatchSerializers.FindMatch(source);
            return csp ?? SerializeAsTypeName;
        }

        #region SerializeWith
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

        protected void SerializeIgnoreType<T>()
        {
            SerializeTypeWith(typeof (T), (source, result) => { });
        }
        protected void SerializeTypeWith<T>(CustomTypeSerializer<T> customTypeSerializer)
        {
            //Typed Serialization
            if (customTypeSerializer != null)
                SerializeTypeWith(typeof (T), (source, result) => customTypeSerializer((T) source, result));
        }

        protected void SerializeTypeWith<T>(string prefix, string postfix = "")
        {
            SerializeTypeWith<T>(source => prefix, source => postfix);
        }
        protected void SerializeTypeWith<T>(TextMap<T> prefix, TextMap<T> postfix = null)
        {
            SerializeTypeWith(typeof(T),
                (source, result) =>
                {
                    var item = (T)source;
                    result.AppendIfNotNull(prefix,item);
                    result.AppendIfNotNull(postfix, item);
                });
        }

        protected void SerializeTypeWith<T>(string prefix, string postfix, ItemMap<T> map)
        {
            SerializeTypeWith(source => prefix, source => postfix, map);
        }

        protected void SerializeTypeWith<T>(TextMap<T> prefix, TextMap<T> postfix, ItemMap<T> map)
        {
            SerializeTypeWith(prefix, postfix, (source, result) =>
            {
                foreach (var item in map(source))
                {
                    Serialize(item, result);
                }
            });
        }

        protected void SerializeTypeWith<T>(string prefix, string postfix, CustomTypeSerializer<T> serialize)
        {
            SerializeTypeWith(source => prefix, source => postfix, serialize);
        }
        protected void SerializeTypeWith<T>(TextMap<T> prefix, TextMap<T> postfix, CustomTypeSerializer<T> serialize)
        {
            SerializeTypeWith(typeof (T),
                (source, result) =>
                {
                    var item = (T) source;
                    result.AppendIfNotNull(prefix, item);
                    serialize(item, result);
                    result.AppendIfNotNull(postfix, item);
                });
        }

        protected void SerializeTypeAsEnumerable<T>(string prefix, string postfix = "", EnumerableMap<T> enumerableMap = null)
            where T : IEnumerable
        {
            SerializeTypeAsEnumerable(source => prefix, source => postfix, enumerableMap);
        }

        protected void SerializeTypeAsEnumerable<T>(TextMap<T> prefix, TextMap<T> postfix = null, EnumerableMap<T> enumerableMap = null)
            where T : IEnumerable
        {
            SerializeTypeWith(typeof(T),
                (source, result) =>
                {
                    //Debug.Print("SerializeTypeAsListWith");
                    var list = (T)source;
                    var items = (enumerableMap == null) ? list : enumerableMap(list);
                    //Wrap serialization of list items in the given pre and post fix text, inside wrapper call serialization on all list items
                    if (prefix != null) result.Append(prefix(list));
                    foreach (var item in items)
                    {
                        //Debug.Print("  ListItem: {0}", item);
                        Serialize(item, result);
                    }
                    if (postfix != null) result.Append(postfix(list));
                });
        }
        #endregion

        #region Serialize As
        protected void SerializeAsPropertyNameValueTable(object source, StringBuilder result)
        {
            if (source == null)
            {
                SerializeAsNull(result);
                return;
            }

            var enumerable = (source as IEnumerable);
            if (enumerable == null)
            {
                // Object => Table of Properties
                SerializeAsTableOfProperties(source, result);
                return;
            }

            // Enumerable => Table of Rows
            var array = enumerable.Cast<object>().ToArray();
            if (array.Any())
            {
                SerializeEnumerableAsTable(array, result);
                return;
            }

            // Empty
            result.AppendFormat("Empty {0}", source.GetType().ToPrettyName());
        }

        private void SerializeEnumerableAsTable(object[] array, StringBuilder result)
        {
            var table = new Table(array.Select((item, index) => new Row(new Cell {Content = index}, new Cell {Content = item})));
            table.Insert(0, new Row(new HeaderCell { Content = array.GetType().ToPrettyName() }));
            Serialize(table, result);
        }

        private void SerializeAsTableOfProperties(object source, StringBuilder result, params string[] propertyNames)
        {
            var rows = (propertyNames.Any())
                ? from pi in source.GetType().GetProperties()
                    where propertyNames.Contains(pi.Name)
                    select new Row(new Cell {Content = pi.Name}, new Cell {Content = pi.GetValue(source)})
                : from pi in source.GetType().GetProperties()
                    select new Row(new Cell {Content = pi.Name}, new Cell {Content = pi.GetValue(source)});

            var table = new Table(rows);
            table.Insert(0, new Row(new HeaderCell {Content = source.GetType().ToPrettyName()}));
            Serialize(table, result);
        }

        protected void SerializeException(object source, StringBuilder result, params string[] propertyNames)
        {
            var ex = source as Exception;
            if (ex != null)
                SerializeAsTableOfProperties(source, result, propertyNames);
        }


        protected void SerializeAsToString<T>(T source, StringBuilder result)
        {
            if (source == null)
            {
                SerializeAsNull(result);
                return;
            }
                result.Append(source);
        }

        protected void SerializeAsToString(object source, StringBuilder result)
        {
            if (source == null)
            {
                SerializeAsNull(result);
                return;
            }
                result.Append(source);
        }


        protected void SerializeAsTypeName(object source, StringBuilder result)
        {
            if (source == null)
            {
                SerializeAsNull(result);
                return;
            }
                result.Append(source.GetType().ToPrettyName());
        }

        protected void SerializeAsNull(object source, StringBuilder result)
        {
            SerializeAsNull(result);
        }
        protected void SerializeAsNull(StringBuilder result)
        {
            result.Append("Null");
        }
        #endregion

        #region Predicate Matches
        protected static class Match 
        {
            public static bool Anything(object o)
            {
                return true;
            }
            public static bool EnumsAndPrimitives(object o)
            {
                var type = o.GetType();
                return type.IsPrimitive || type.IsEnum;
            }
            public static bool Exceptions(object o)
            {
                var type = o.GetType();
                return type.IsSubclassOf(typeof(Exception));
            }
        }
        #endregion

        #region Clear
        protected void ClearAllCustomSerializers()
        {
            ClearCustomTypeSerializers();
            ClearCustomMatchSerializers();
        }
        protected void ClearCustomTypeSerializers()
        {
            //Debug.Print("ClearCustomTypeSerializers");
            _customTypeSerializers.Clear();
        }
        protected void ClearCustomMatchSerializers()
        {
            //Debug.Print("ClearCustomMatchSerializers");
            _customMatchSerializers.Clear();
        }
        #endregion

        #region Classes
        private class CustomMatchSerializer
        {
            public Predicate Predicate { private get; set; }
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