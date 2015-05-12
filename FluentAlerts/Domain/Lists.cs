using System.Collections.Generic;

namespace FluentAlerts.Domain
{
    public class OrderedList : List<ListItem>
    {
        public OrderedList(params ListItem[] items) : base(items) { }
        public OrderedList(IEnumerable<ListItem> items) : base(items) { }
    }
    public class UnOrderedList : List<ListItem>
    {
        public UnOrderedList(params ListItem[] items) : base(items) { }
        public UnOrderedList(IEnumerable<ListItem> items) : base(items) { }
    }

    public class ListItem
    {
        public object Content { get; set; }
    }
}