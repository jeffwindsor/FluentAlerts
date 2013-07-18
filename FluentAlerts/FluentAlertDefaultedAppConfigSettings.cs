using System.Collections.Generic;
using System.Configuration;

namespace FluentAlerts
{
    public class FluentAlertDefaultedAppConfigSettings : IFluentAlertSettings
    {
        private const string DefaultTemplateNameValue = "HtmlWithEmbeddedCssTableTemplate";
        private const string TemplateFileNameValue = "DefaultTemplates.json";
        private const string MemberPathSeperatorValue = ".";

        private enum ConfigKey
        {
            DefaultTemplateName,
            TemplateFileName,
            MemberPathSeperator
        }

        public string DefaultTemplateName()
        {
            return TryGetConfigurationValue(ConfigKey.DefaultTemplateName, DefaultTemplateNameValue);
        }

        public string TemplateFileName()
        {
            return TryGetConfigurationValue(ConfigKey.TemplateFileName, TemplateFileNameValue);
        }

        public char MemberPathSeperator()
        {
            //return first character or .
            var chars = TryGetConfigurationValue(ConfigKey.MemberPathSeperator, MemberPathSeperatorValue).ToCharArray();
            return chars[0];
        }

        private static string TryGetConfigurationValue(ConfigKey key, string defaultValue)
        {
            var value = ConfigurationManager.AppSettings[key.ToString()];
            return string.IsNullOrWhiteSpace(value) ? defaultValue : value;
        }
    }
}
