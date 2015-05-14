using System.Collections.Generic;

namespace FluentAlerts.Domain
{
    public class TextBlock : List<object>
    {
        public TextBlock(params object[] items) : base(items) { }
        public TextBlock(IEnumerable<object> items): base(items){}
    }
    public class HeaderTextBlock : TextBlock
    {
        public int Level { get; set; }

        public HeaderTextBlock(int level, params object[] items)
            : base(items)
        {
            Level = level;
        }
        public HeaderTextBlock(IEnumerable<object> items, int level)
            : base(items)
        {
            Level = level;
        }
    }
}