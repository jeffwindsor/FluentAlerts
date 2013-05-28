using System.Collections.Generic;
using FluentAlerts.Transformers;

namespace FluentAlerts
{
    public static class ExtensionsToIEnumerableOfString
    {
        public static MemberPath ToMemberPath(this IEnumerable<string> source)
        {
            return new MemberPath(source);
        }
        public static string ToMemberPathString(this IEnumerable<string> source)
        {
            return new MemberPath(source).ToString();
        }
    }
}
