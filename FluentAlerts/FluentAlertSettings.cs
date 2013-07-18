using System.Collections.Generic;
using System.Configuration;

namespace FluentAlerts
{
    public class FluentAlertSettings : IFluentAlertSettings
    {
        private const string DefaultTemplateNameValue = "HtmlWithEmbeddedCssTableTemplate";
        private const string TemplateFileNameValue = "DefaultTemplates.json";
        private const string MemberPathSeperatorValue = ".";
        private const string DefaultTemplateDictionaryValue =
            @"{
                  ""HtmlWithEmbeddedCssTableTemplate"": {
                    ""SerializationHeader"": ""<html><body>"",
                    ""SerializationFooter"": ""</html></body>"",
                    ""AlertHeader"": ""<table cellspacing='1' cellpadding='2' style='font-family:Arial,Sans-Serif;font-size:8pt;' width='100%'>"",
                    ""AlertFooter"": ""</table>"",
                    ""TextNormalHeader"": ""<TR><TD colspan='{Colspan}' style='bgcolor=gainsboro;'>"",
                    ""TextNormalFooter"": ""</TD></TR>"",
                    ""TextTitleHeader"": ""<TR><TD colspan='{Colspan}' style='bgcolor=silver;font-size:10pt;font-weight:bold;'>"",
                    ""TextTitleFooter"": ""</TD></TR>"",
                    ""TextEmphasizedHeader"": ""<TR><TD colspan='{Colspan}' style='bgcolor=silver;font-weight:bold;'>"",
                    ""TextEmphasizedFooter"": ""</TD></TR>"",
                    ""GroupHeader"": ""<TR>"",
                    ""GroupFooter"": ""</TR>"",
                    ""ValueNormalHeader"": ""<TD style='bgcolor=whitesmoke;'>"",
                    ""ValueNormalFooter"": ""</TD>"",
                    ""ValueEmphasizedHeader"": ""<TD style='bgcolor=silver;font-weight:bold;'>"",
                    ""ValueEmphasizedFooter"": ""</TD>"",
	                ""ValueEmphasizedSpanningHeader"": ""<TD colspan='{Colspan}' style='bgcolor=silver;font-weight:bold;'>"",
                    ""ValueEmphasizedSpanningFooter"": ""</TD>"",
                    ""ValueSpanningHeader"": ""<TD colspan='{Colspan}' style='bgcolor=gainsboro;font-weight:bold;'>"",
                    ""ValueSpanningFooter"": ""</TD>"",
                    ""UrlHeader"": ""<TD style='font-weight:bold;'>"",
                    ""UrlValue"": ""<a href='{Url}'>{UrlTitle}</a>"",
                    ""UrlFooter"": ""</TD>"",
                    ""SeperatorHeader"": ""<TD colspan='{Colspan}' style='bgcolor=gainsboro;font-weight:bold;'>"",
                    ""SeperatorFooter"": ""</TD>""
                  }
                }";

        private enum ConfigKey
        {
            DefaultTemplateName,
            DefaultTemplateDictionary,
            TemplateFileName,
            MemberPathSeperator
        }

        public string DefaultTemplateDictionary()
        {
            return TryGetConfigurationValue(ConfigKey.DefaultTemplateDictionary, DefaultTemplateDictionaryValue);
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
