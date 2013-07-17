using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace FluentAlerts.Renderers
{
    public class Template : Dictionary<string, string>{}
    internal class TemplateDictionary
    {
        private readonly Dictionary<string, Template> _inner;
        private readonly IEnumerable<string> _keys;
        private readonly ITemplateIssueHandler _issueHandler;
        
        public TemplateDictionary(ITemplateIssueHandler issueHandler, string fileName = "")
        {
            _issueHandler = issueHandler;

            //Load or Default
            if (!string.IsNullOrWhiteSpace(fileName) && File.Exists(fileName))
            {
                _inner = Deserialize(File.ReadAllText(fileName));
            }
            else
            {
                Trace.TraceInformation("Templates file {0} was not found, default templates loaded.", fileName);
                _inner = Deserialize(DefaultTemplates); 
            }

            //Sorted Keys
            _keys = from key in _inner.Keys 
                    orderby key
                    select key;
        }

        public Template GetTemplate(string templateName)
        {
             return _inner.ContainsKey(templateName)
                       ? _inner[templateName]
                       : _issueHandler.TemplateNotFound(templateName);
        }

        public string Export(string fileName)
        {
            return FileExporter.Export(fileName, Serialize(_inner));
        }

        public IEnumerable<string> Keys 
        {
            get
            {
                return _keys;
            }
        }

        private static string Serialize(Dictionary<string, Template> o)
        {
            return JsonConvert.SerializeObject(o, Formatting.Indented, new KeyValuePairConverter());
        }

        private static Dictionary<string, Template> Deserialize(string json)
        {
            return JsonConvert.DeserializeObject<Dictionary<string, Template>>(json);
        }

        #region Default Templates
        private const string DefaultTemplates =
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
        #endregion
    }
}
