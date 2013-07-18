using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace FluentAlerts.Renderers
{
    public class AlertRenderTemplate : Dictionary<string, string>{}
    internal class AlertRenderTemplateDictionary
    {
        private const string TemplateFilePattern = "RenderTemplate.json";
        private readonly IAlertRenderTemplateDictionaryIssueHandler _issueHandler;
        private readonly IDictionary<string, AlertRenderTemplate> _inner = new Dictionary<string, AlertRenderTemplate>();
        
        public AlertRenderTemplateDictionary(IAlertRenderTemplateDictionaryIssueHandler issueHandler)
        {
            _issueHandler = issueHandler;
        }

        public IEnumerable<string> Keys
        {
            get
            {
                return from key in _inner.Keys
                       orderby key
                       select key;
            }
        }
         
        /// <summary>
        /// 
        /// </summary>
        /// <returns>The set of keys from the imported files</returns>
        public IEnumerable<string> Import()
        {
            _inner.Clear();

            //Import all files that match the pattern
            foreach (var filePath in Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory,
                                                        "*" + TemplateFilePattern))
            {
                var key = filePath.Replace(TemplateFilePattern, string.Empty);
                try
                {
                    _inner[key] = Deserialize(File.ReadAllText(filePath));
                }
                catch (Exception ex)
                {
                    ex.ToTrace("Serialization of {0} failed.",key);
                }
            }

             //Default check
            if (!_inner.Any())
                throw new ApplicationException(string.Format("No valid formatter template matching the pattern *{0} was found in the application directory", TemplateFilePattern));

            return Keys;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="overwrite"></param>
        /// <returns>Filename or String Empty if no file was written</returns>
        public string Export(string key, bool overwrite = true)
        {
            if (_inner.ContainsKey(key))
            {
                //Append key to make a file name
                var fileName = string.Format("{0}{1}", key, TemplateFilePattern);       

                //Do not overwrite existing file
                if (overwrite == false && File.Exists(fileName)) return string.Empty;
                
                return Serialize(_inner[key]).ExportToFile(fileName);
            }
            return string.Empty;
        }

        public AlertRenderTemplate GetTemplate(string templateName)
        {
             return _inner.ContainsKey(templateName)
                       ? _inner[templateName]
                       : _issueHandler.TemplateNotFound(templateName);
        }



        private static string Serialize(AlertRenderTemplate o)
        {
            return JsonConvert.SerializeObject(o, Formatting.Indented, new KeyValuePairConverter());
        }

        private static AlertRenderTemplate Deserialize(string json)
        {
            return JsonConvert.DeserializeObject<AlertRenderTemplate>(json);
        }

    }
}
