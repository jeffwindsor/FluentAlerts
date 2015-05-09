using System.Collections.Generic;
using System.Configuration;

namespace FluentAlerts
{
    public class FluentAlertDefaultedAppConfigSettings : IFluentAlertsSettings
    {
        private enum ConfigKey
        {
            DefaultTemplateName
        }

        public string DefaultTemplateName()
        {
            return TryGetConfigurationValue(ConfigKey.DefaultTemplateName, "HtmlWithEmbeddedCssTableTemplate");
        }

        private static string TryGetConfigurationValue(ConfigKey key, string defaultValue)
        {
            var value = ConfigurationManager.AppSettings[key.ToString()];
            return string.IsNullOrWhiteSpace(value) ? defaultValue : value;
        }
    }
}
