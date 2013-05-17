using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FluentAlerts.TypeInformers;

namespace FluentAlerts.Transformers
{
    public class HeirarchialNameValueRowTransformer: ITransformer 
    {
        private readonly ITypeInfoRepository _typeInfos;
        
        public HeirarchialNameValueRowTransformer(ITypeInfoRepository typeInfos)
        {
            _typeInfos = typeInfos;
        }

        public IAlert Transform(object o, ITransformStrategy transformStrategy)
        {
            var type = o.GetType();
            //Obtain the members to serialize for this type
            var typeInfo = _typeInfos.Find(type);

            //Create Alert with a property and fields section containing name-values pairs  
            return Alerts.Create(type.Name)
                .WithHeader("Properties")
                .WithRows(GetPropertyNameValuePairs(o,typeInfo,transformStrategy))
                .WithHeader("Fields")
                .WithRows(GetFieldNameValuePairs(o, typeInfo, transformStrategy))
                .ToAlert(); 
        }
        
        protected virtual IEnumerable<object[]> GetPropertyNameValuePairs(object o, TypeInfo typeInfo, ITransformStrategy transformStrategy)
        {
            return from info in typeInfo.PropertyInfos
                   orderby info.Name
                   select new[] {info.Name, GetPropertyValue(o, info, transformStrategy)};
        }
        private object GetPropertyValue(object o, PropertyInfo pi, ITransformStrategy transformStrategy)
        {
            try
            {
                //Obtain Value
                var value = pi.GetValue(o, null);
                //Check Tranform strategy and transform if required
                return transformStrategy.IsTransformRequired(value) ? Transform(value, transformStrategy) : value;
            }
            catch (Exception)
            {
                return "Failed to Obtain Value";
            }
        }

        protected virtual IEnumerable<object[]> GetFieldNameValuePairs(object o, TypeInfo typeInfo, ITransformStrategy transformStrategy)
        {
            return from info in typeInfo.FieldInfos
                   orderby info.Name
                   select new[] {info.Name, GetFieldValue(o, info, transformStrategy)};
        }
        private object GetFieldValue(object o, FieldInfo fi, ITransformStrategy transformStrategy)
        {
            try
            { 
                //Obtain Value
                var value = fi.GetValue(o);
                //Check Tranform strategy and transform if required
                return transformStrategy.IsTransformRequired(value) ? Transform(value, transformStrategy) : value;
            }
            catch (Exception)
            {
                return "Failed to Obtain Value";
            }
        }
        
        protected class NameValue
        {
            public string Name { get; set; }
            public object Value { get; set; }
        }
    }
}
