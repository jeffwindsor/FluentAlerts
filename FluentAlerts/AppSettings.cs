using System;
using System.Configuration;
using FluentAlerts.Renderers;

namespace FluentAlerts
{
    internal static class AppSettings
    {
        public static string DefaultTemplateName()
        {
            return TryGetConfigurationValue(ConfigKey.DefaultTemplateName, "HtmlWithEmbeddedCssTableTemplate");
        }

        public static string TemplateFileName()
        {
            return TryGetConfigurationValue(ConfigKey.TemplateFileName, "FluentAlertRenderTemplates.json");
        }

        private static string TryGetConfigurationValue(ConfigKey key, string defaultValue)
        {
            return ConfigurationManager.AppSettings[key.ToString()] ?? defaultValue;
        }

        private enum ConfigKey
        {
            DefaultTemplateName,
            TemplateFileName
        }

    }
}
