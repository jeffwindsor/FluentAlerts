using System;
using System.Diagnostics;

namespace FluentAlerts
{
    public static class TraceExtensions
    {
        public static void ToTrace(this Exception source, string format, params object[] args)
        {
            source.ToTrace(string.Format(format,args));
        }

        public static void ToTrace(this Exception source, string message)
        {
            if (!string.IsNullOrWhiteSpace(message))
                Trace.WriteLine(message);

            source.ToTrace();
        }

        public static void ToTrace(this Exception source)
        {
            source.ToTrace(0);
        }

        private static void ToTrace(this Exception source, int level)
        {
            Trace.WriteLine((level == 0) ? "Exception" : string.Format("Inner Exception {0}", level));
            Trace.WriteLine(string.Format(" Message: {0}", source.Message));
            Trace.WriteLine(string.Format(" {0}",source.StackTrace));
        }
    }
}
