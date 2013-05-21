using System.Collections.Generic;

namespace FluentAlerts.Transformers
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
