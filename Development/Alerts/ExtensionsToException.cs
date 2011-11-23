using System;
using System.Collections.Generic;

namespace Alerts
{
    public static partial class ExtensionsToException
    {
        public static IEnumerable<Exception> ToList(this Exception ex)
        {
            while (ex != null)
            {
                yield return ex;
                ex = ex.InnerException;
            }
            yield break;
        }

    }

}
