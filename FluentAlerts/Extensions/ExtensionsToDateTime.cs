using System;
namespace FluentAlerts
{
    public static class ExtensionsToDateTime
    {
        public static string ToIsoFormat(this DateTime source)
        {
            return source.ToString("yyyy_MM_dd_HH_mm_ss");
        }
    }
}
