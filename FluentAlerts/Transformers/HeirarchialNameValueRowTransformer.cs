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

        public IAlert Transform(object o, ITransformStrategy transformStrategy, int objectsDepth)
        {
            //Establish object's type
            var type = o.GetType();

            //Obtain the members to serialize for this type
            var typeInfo = _typeInfos.Find(type);

            //Create Alert with a property and fields section containing name-values pairs  
            return Alerts.Create(type.Name)
                .WithHeader("Properties")
                .WithRows(GetPropertyNameValuePairs(o,typeInfo,transformStrategy, objectsDepth))
                .WithHeader("Fields")
                .WithRows(GetFieldNameValuePairs(o, typeInfo, transformStrategy, objectsDepth))
                .ToAlert(); 
        }

        protected virtual IEnumerable<object[]> GetPropertyNameValuePairs(object o, TypeInfo typeInfo, ITransformStrategy transformStrategy, int objectsDepth)
        {
            return from info in typeInfo.PropertyInfos
                   orderby info.Name
                   select new[] { info.Name, GetPropertyValue(o, info, transformStrategy, objectsDepth) };
        }
        private object GetPropertyValue(object o, PropertyInfo pi, ITransformStrategy transformStrategy, int objectsDepth)
        {
            try
            {
                //Obtain Value
                var value = pi.GetValue(o, null);
                var valueDepth = objectsDepth + 1;
                //Check Tranform strategy and transform if required
                return transformStrategy.IsTransformRequired(value, valueDepth)
                    ? Transform(value, transformStrategy, valueDepth) 
                    : value;
            }
            catch (Exception)
            {
                return "Failed to Obtain Value";
            }
        }

        protected virtual IEnumerable<object[]> GetFieldNameValuePairs(object o, TypeInfo typeInfo, ITransformStrategy transformStrategy, int objectsDepth)
        {
            return from info in typeInfo.FieldInfos
                   orderby info.Name
                   select new[] { info.Name, GetFieldValue(o, info, transformStrategy, objectsDepth) };
        }
        private object GetFieldValue(object o, FieldInfo fi, ITransformStrategy transformStrategy, int valueDepth)
        {
            try
            { 
                //Obtain Value
                var value = fi.GetValue(o);
                //Check Tranform strategy and transform if required
                return transformStrategy.IsTransformRequired(value, valueDepth)
                    ? Transform(value, transformStrategy, valueDepth) 
                    : value;
            }
            catch (Exception)
            {
                return "Failed to Obtain Value";
            }
        }
    }
}
