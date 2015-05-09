using System;


namespace FluentAlerts
{
    public static class ExtensionsToEnums
    {

        public static AlertItemStyle ToItemStyle(this AlertItemValueStyle style)
        {
            switch (style)
            {
                case AlertItemValueStyle.Normal:
                    return AlertItemStyle.Normal;
                case AlertItemValueStyle.Emphasized:
                    return AlertItemStyle.Emphasized;
                case AlertItemValueStyle.Title:
                    return AlertItemStyle.Title;
                default:
                    throw new ArgumentException("Text Style not recognized", "style");
            }
        }
        public static AlertItemValueStyle ToValueStyle(this AlertItemStyle style)
        {
            switch (style)
            {
                case AlertItemStyle.Normal:
                    return AlertItemValueStyle.Normal;
                case AlertItemStyle.Emphasized:
                    return AlertItemValueStyle.Emphasized;
                case AlertItemStyle.Title:
                    return AlertItemValueStyle.Title;
                default:
                    throw new ArgumentException("Item Style not recognized", "style");
            }
        }
    }
}
