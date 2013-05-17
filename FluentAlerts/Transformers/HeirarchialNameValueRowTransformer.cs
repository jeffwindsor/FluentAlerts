using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FluentAlerts.TypeInfoSelectors;

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


//'    Public Shared Function CreateForObjectByReflection(Of T)(ByVal source As T, Optional ByVal title As String = Nothing) As NotificationTable
//'        'Use object Name if title not given
//'        If String.IsNullOrEmpty(title) Then title = source.GetType.Name

//'        'Create a Table
//'        Dim result = NotificationTable.Create(title)
//'        result.AddRowsForObjectByReflection(source)
//'        Return result
//'    End Function
//'    Public Sub AddRowsForObjectByReflection(Of T)(ByVal source As T)
//'        'Get Object Information
//'        Dim sourceType = source.GetType()
//'        Dim pis As New List(Of System.Reflection.PropertyInfo)(sourceType.GetProperties())
//'        pis.Sort(Function(x, y) x.Name.CompareTo(y.Name))

//'        'Build Messag Builder for Object
//'        For Each pi In pis

//'            If pi.PropertyType.IsValueType _
//'            OrElse pi.PropertyType Is GetType(System.String) Then
//'                'Properties
//'                Dim value As Object = Nothing
//'                Try
//'                    value = pi.GetValue(source, Nothing)
//'                Catch ex As Exception
//'                    value = "Reflection Failed to Obtain Value"
//'                End Try
//'                Me.AddRow(pi.Name, value)
//'            End If
//'        Next
//'    End Sub