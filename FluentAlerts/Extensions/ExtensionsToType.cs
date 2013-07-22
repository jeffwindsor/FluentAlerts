using System;

namespace FluentAlerts
{
    internal static class TypeExtensions
    {
        /// <summary>
        /// Return true for string, enum, date time and primitives
        /// </summary>
        public static bool IsFundamental(this Type type)
        {
            return type.IsPrimitive || type == typeof(string) || type == typeof(DateTime) || type.IsEnum;
        }

        public static bool IsFundamental(this object o)
        {
            return o.GetType().IsFundamental();
        }
    }
}
