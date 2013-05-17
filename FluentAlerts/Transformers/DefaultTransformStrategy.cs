using System;
using System.Collections.Generic;

namespace FluentAlerts.Transformers
{
    /// <summary>
    /// Will not transform primitives, strings, enums or datetimes.
    /// </summary>
    public class DefaultTransformStrategy : TransformStrategy 
    {
        public DefaultTransformStrategy()
        {
            //Transform UserDefinedStructs or Classes at depth zero only
            Rules.Add((type, depth) => type.IsClassOrUserDefinedStruct() && depth == 0);
            //Transform any exception at any depth
            Rules.Add((type, depth) => type.IsAssignableFrom(typeof(Exception)));
        }
    }
}
