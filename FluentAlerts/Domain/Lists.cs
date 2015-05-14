using System.Collections.Generic;

namespace FluentAlerts.Domain
{
    public class OrderedList : List<OrderedListItem>
    {
        public OrderedList(params OrderedListItem[] items) : base(items) { }
        public OrderedList(IEnumerable<OrderedListItem> items) : base(items) { }
    }
    public class UnOrderedList : List<UnOrderedListItem>
    {
        public UnOrderedList(params UnOrderedListItem[] items) : base(items) { }
        public UnOrderedList(IEnumerable<UnOrderedListItem> items) : base(items) { }
    }

    public class OrderedListItem { public object Content { get; set; } }
    public class UnOrderedListItem { public object Content { get; set; } }
}