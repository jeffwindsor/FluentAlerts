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
        private readonly IFluentAlertSettings _settings;

        public TemplateDictionary(ITemplateIssueHandler issueHandler, IFluentAlertSettings settings, string fileName = "")
        {
            _issueHandler = issueHandler;
            _settings = settings;

            //Load or Default
            if (!string.IsNullOrWhiteSpace(fileName) && File.Exists(fileName))
            {
                _inner = Deserialize(File.ReadAllText(fileName));
            }
            else
            {
                Trace.TraceInformation("Templates file {0} was not found, default templates loaded.", fileName);
                _inner = Deserialize(settings.DefaultTemplateDictionary()); 
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
            return Serialize(_inner).ExportToFile(fileName);
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

    }
}
