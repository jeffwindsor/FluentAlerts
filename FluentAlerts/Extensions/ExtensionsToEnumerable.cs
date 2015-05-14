using System.Collections.Generic;
using System.Linq;
using FluentAlerts.Domain;

namespace FluentAlerts.Extensions
{
    internal static class ExtensionsToEnumerable
    {
        public static IEnumerable<OrderedListItem> ToOrderedListItems(this IEnumerable<object> items)
        {
            return items.Select(i => new OrderedListItem { Content = i });
        }
        public static IEnumerable<UnOrderedListItem> ToUnOrderedListItems(this IEnumerable<object> items)
        {
            return items.Select(i => new UnOrderedListItem { Content = i });
        }


        public static IEnumerable<Cell> ToCellItems(this IEnumerable<object> items)
        {
            return items.Select(i => new Cell { Content = i });
        }

        public static IEnumerable<EmphasizedCell> ToEmphasizedCellItems(this IEnumerable<object> items)
        {
            return items.Select(i => new EmphasizedCell { Content = i });
        }

        public static IEnumerable<HeaderCell> ToHeaderCellItems(this IEnumerable<object> items)
        {
            return items.Select(i => new HeaderCell { Content = i });
        }
    }
}
