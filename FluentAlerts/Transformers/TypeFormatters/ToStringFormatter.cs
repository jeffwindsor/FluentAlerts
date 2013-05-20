using System.Collections.Generic;

namespace FluentAlerts.Transformers.TypeFormatters
{
    public class ToStringTypeFormatter:ITypeFormatter<string>
    {
        public string Format(object o, IEnumerable<string> objectMemberPath)
        {
            return o.ToString();
        }
    }
}
