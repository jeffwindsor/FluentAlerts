using System.Collections.Generic;
using FluentAlerts.Transformers;

namespace FluentAlerts
{
    public static class ExtensionsToIEnumerableOfString
    {
        public static MemberPath ToMemberPath(this IEnumerable<string> source, IFluentAlertSettings settings)
        {
            return new MemberPath(source,settings);
        }
        public static string ToMemberPathString(this IEnumerable<string> source, IFluentAlertSettings settings)
        {
            return new MemberPath(source,settings).ToString();
        }
    }
}
