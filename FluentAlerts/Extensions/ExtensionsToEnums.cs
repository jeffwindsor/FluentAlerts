using System;
namespace FluentAlerts
{
    public static class ExtensionsToEnums
    {

        public static ItemStyle ToItemStyle(this TextStyle style)
        {
            switch (style)
            {
                case TextStyle.Emphasized:
                    return ItemStyle.EmphasizedText;
                case TextStyle.HeaderOne:
                    return ItemStyle.HeaderOneText;
                case TextStyle.Normal:
                    return ItemStyle.NormalText;
                default:
                    throw new ArgumentException("Text Style not recognized", "style");
            }
        }
        public static TextStyle ToTextStyle(this ItemStyle style)
        {
            switch (style)
            {
                case ItemStyle.EmphasizedText:
                    return TextStyle.Emphasized;
                case ItemStyle.HeaderOneText:
                    return TextStyle.HeaderOne;
                case ItemStyle.NormalText:
                    return TextStyle.Normal;
                default:
                    throw new ArgumentException("Item Style not recognized", "style");
            }
        }

        public static ItemStyle ToItemStyle(this ArrayStyle style)
        {
            switch (style)
            {
                case ArrayStyle.Emphasized:
                    return ItemStyle.EmphasizedRow;
                case ArrayStyle.Normal:
                    return ItemStyle.NormalRow;
                default:
                    throw new ArgumentException("Text Style not recognized", "style");
            }
        }
        public static ArrayStyle ToArrayStyle(this ItemStyle style)
        {
            switch (style)
            {
                case ItemStyle.EmphasizedRow:
                    return ArrayStyle.Emphasized;
                case ItemStyle.NormalRow:
                    return ArrayStyle.Normal;
                default:
                    throw new ArgumentException("Item Style not recognized", "style");
            }
        }
    }
}
