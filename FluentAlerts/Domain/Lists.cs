using System.Collections.Generic;

namespace FluentAlerts.Domain
{
    public class OrderedList : List<object> {
        public OrderedList(params object[] items) : base(items) { }
        public OrderedList(IEnumerable<object> items): base(items){}
    }
    public class UnOrderedList : List<object>
    {
        public UnOrderedList(params object[] items) : base(items) { }
        public UnOrderedList(IEnumerable<object> items) : base(items) { }
    }
}