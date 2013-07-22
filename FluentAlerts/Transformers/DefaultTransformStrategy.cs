using System;
using System.Linq;

namespace FluentAlerts.Transformers
{
    /// <summary>
    /// A list of transform rules.  The render is looking for any one of these rules to pass
    /// as an indication the object should be transformed into an alert instead of formatted
    /// to a string.
    /// </summary>
    public class DefaultTransformStrategy : TransformStrategy 
    {
        public DefaultTransformStrategy()
        {
            //Transform any exception at any depth
            TransformationRequiredRules.Add((o, objectMemberPath) => o.GetType().IsSubclassOf(typeof (Exception)));

            //Transform UserDefinedStructs or Classes do not re-curse properties and fields
            TransformationRequiredRules.Add((o, objectMemberPath) => !o.IsFundamental() && (objectMemberPath.Length < 2));
        }
    }
}
