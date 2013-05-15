namespace FluentAlerts.Extensions
{
    internal static class EnumerationExtensions
    {
        public static GroupStyle ToGroupStyle(this TableRowStyle style)
        {
            switch (style)
            {
                case TableRowStyle.Highlight:
                    return GroupStyle.HighlightedText;
                default:
                    return GroupStyle.NormalText;
            }
        }

        public static GroupStyle ToGroupStyle(this TableSpanningRowStyle style)
        {
            switch (style)
            {
                case TableSpanningRowStyle.Footer:
                    return GroupStyle.Footer;
                default:
                    return GroupStyle.Header;
            }
        }
    }
}
