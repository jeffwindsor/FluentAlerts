﻿using System.Configuration;

namespace FluentAlerts
{
    public class FluentAlertSettings : IFluentAlertSettings
    {
        private enum ConfigKey
        {
            DefaultTemplateName,
            TemplateFileName,
            MemberPathSeperator
        }

        public string DefaultTemplateName()
        {
            return TryGetConfigurationValue(ConfigKey.DefaultTemplateName, "HtmlWithEmbeddedCssTableTemplate");
        }

        public string TemplateFileName()
        {
            return TryGetConfigurationValue(ConfigKey.TemplateFileName, "DefaultTemplates.json");
        }

        public char MemberPathSeperator()
        {
            //return first character or .
            var chars = TryGetConfigurationValue(ConfigKey.MemberPathSeperator, ".").ToCharArray();
            return chars[0];
        }

        private static string TryGetConfigurationValue(ConfigKey key, string defaultValue)
        {
            var value = ConfigurationManager.AppSettings[key.ToString()];
            return string.IsNullOrWhiteSpace(value) ? defaultValue : value;
        }
    }
}
