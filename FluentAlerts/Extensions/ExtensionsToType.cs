﻿using System;

namespace FluentAlerts
{
    internal static class TypeExtensions
    {
        /// <summary>
        /// Return true for string, datetime and primitives
        /// </summary>
        public static bool IsFundamental(this Type type)
        {
            return type.IsPrimitive || type == typeof(string) || type == typeof(DateTime);
        }

        public static bool IsClassOrUserDefinedStruct(this Type type)
        {
            return !type.IsFundamental() && !type.IsEnum;
        }
    }
}