using System;
using System.Linq;

namespace FluentAlerts.Transformers.Strategies
{
    /// <summary>
    /// Will not transform primitives, strings, enums or datetimes.
    /// </summary>
    public class DefaultTransformStrategy : TransformStrategy 
    {
        public DefaultTransformStrategy()
        {
            //Transform UserDefinedStructs or Classes do not recurse preoprties andd fields
            TransformationRequiredRules.Add((type, objectMemberPath) => type.IsClassOrUserDefinedStruct() && objectMemberPath.Count() == 1);
            //Transform any exception at any depth
            TransformationRequiredRules.Add((type, objectMemberPath) => type.IsAssignableFrom(typeof(Exception)));
        }
    }
}
