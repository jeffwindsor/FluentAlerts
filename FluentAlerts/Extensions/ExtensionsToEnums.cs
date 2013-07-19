using System;
namespace FluentAlerts
{
    public static class ExtensionsToEnums
    {

        public static ItemStyle ToItemStyle(this ValueStyle style)
        {
            switch (style)
            {
                case ValueStyle.Normal:
                    return ItemStyle.Normal;
                case ValueStyle.Emphasized:
                    return ItemStyle.Emphasized;
                case ValueStyle.Title:
                    return ItemStyle.Title;
                default:
                    throw new ArgumentException("Text Style not recognized", "style");
            }
        }
        public static ValueStyle ToValueStyle(this ItemStyle style)
        {
            switch (style)
            {
                case ItemStyle.Normal:
                    return ValueStyle.Normal;
                case ItemStyle.Emphasized:
                    return ValueStyle.Emphasized;
                case ItemStyle.Title:
                    return ValueStyle.Title;
                default:
                    throw new ArgumentException("Item Style not recognized", "style");
            }
        }
    }
}
