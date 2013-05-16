namespace FluentAlerts
{
    internal static class EnumerationExtensions
    {
        public static GroupStyle ToGroupStyle(this RowStyle style)
        {
            switch (style)
            {
                case RowStyle.Highlight:
                    return GroupStyle.HighlightedRow;
                default:
                    return GroupStyle.NormalRow;
            }
        }
    }
}
