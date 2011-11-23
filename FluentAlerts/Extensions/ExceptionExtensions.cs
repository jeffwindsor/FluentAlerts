using System;
using System.Collections.Generic;

namespace FluentAlerts.Extensions
{
    public static class ExceptionExtensions
    {
        public static IEnumerable<Exception> ToList(this Exception ex)
        {
            while (ex != null)
            {
                yield return ex;
                ex = ex.InnerException;
            }
        }

    }

}
