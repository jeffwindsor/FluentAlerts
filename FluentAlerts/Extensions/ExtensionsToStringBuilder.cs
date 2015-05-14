using System.Text;

namespace FluentAlerts.Extensions
{
    internal static class ExtensionsToStringBuilder
    {
        public static void AppendIfNotNull(this StringBuilder source, string text)
        {
            if (text != null) source.Append(text);
        }
        public static void AppendIfNotNull<T>(this StringBuilder source, TextMap<T> map, T item)
        {
            if (map != null) source.Append(map(item));
        }
    }
}
