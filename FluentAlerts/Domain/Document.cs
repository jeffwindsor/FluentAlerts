using System.Collections.Generic;

namespace FluentAlerts.Domain
{
    public class Document : List<object>
    {
        public Document(params object[] items) : base(items) { }
        public Document(IEnumerable<object> items): base(items){}
    }
}