﻿using System;
using System.Linq;

namespace FluentAlerts.Extensions
{
    internal static class ExtensionsToType
    {
        public static string ToPrettyName(this object source)
        {
            return ToPrettyName(source.GetType());
        }

        public static string ToPrettyName(this Type type)
        {
            var typeName = type.Name;
            var genericArguments = type.GetGenericArguments();
            if (!type.GetGenericArguments().Any()) return typeName;

            var baseName = typeName.Substring(0, typeName.IndexOf("`", StringComparison.Ordinal));
            var genericNames = genericArguments.Select(ToPrettyName);
            var genericsNames = String.Join(",", genericNames);
            return string.Format("{0}<{1}>", baseName, genericsNames);
        }
    }
}
