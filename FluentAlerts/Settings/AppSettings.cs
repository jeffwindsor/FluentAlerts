using System.Configuration;

namespace FluentAlerts.Settings
{
    public interface IAppSettings
    {
        string DefaultTemplateName();
        string TemplateFileName();
    }
    public class AppSettings : IAppSettings
    {
        private enum ConfigKey
        {
            DefaultTemplateName,
            TemplateFileName
        }

        public string DefaultTemplateName()
        {
            return TryGetConfigurationValue(ConfigKey.DefaultTemplateName, "HtmlWithEmbeddedCssTableTemplate");
        }

        public string TemplateFileName()
        {
            return TryGetConfigurationValue(ConfigKey.TemplateFileName, "DefaultTemplates.json");
        }

        private static string TryGetConfigurationValue(ConfigKey key, string defaultValue)
        {
            return ConfigurationManager.AppSettings[key.ToString()] ?? defaultValue;
        }

        

    }
}
