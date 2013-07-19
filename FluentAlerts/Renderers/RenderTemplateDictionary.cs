 using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace FluentAlerts.Renderers
{
    public class RenderTemplateDictionary
    {
        private const string TemplateFileExtension = "json";
        private const string TemplateFilePattern = "RenderTemplate*." + TemplateFileExtension;
        private readonly IRenderTemplateDictionaryIssueHandler _issueHandler;
        private readonly IDictionary<string, RenderTemplate> _inner = new Dictionary<string, RenderTemplate>();
        
        public RenderTemplateDictionary(IRenderTemplateDictionaryIssueHandler issueHandler)
        {
            _issueHandler = issueHandler;
            //Import all files
            Import();
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
                                                        TemplateFilePattern))
            {
                var key = Path.GetFileNameWithoutExtension(filePath);
                try 
                {
                    _inner[key] = Deserialize(File.ReadAllText(filePath));
                }
                catch (Exception ex)
                {
                    ex.ToTrace("Serialization of {0} failed.",key);
                }
            }

            return Keys;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns>Filenames</returns>
        public IEnumerable<string> Export()
        {
            return (from key in Keys 
                    let fileName = string.Format("{0}{1}", key, TemplateFilePattern) 
                    select Serialize(_inner[key]).ExportToFile(fileName)
                    ).ToList();
        }

        public RenderTemplate GetTemplate(string templateName)
        {
             return _inner.ContainsKey(templateName)
                       ? _inner[templateName]
                       : _issueHandler.TemplateNotFound(templateName);
        }
        
        private static string Serialize(RenderTemplate o)
        {
            return JsonConvert.SerializeObject(o, Formatting.Indented, new KeyValuePairConverter());
        }

        private static RenderTemplate Deserialize(string json)
        {
            return JsonConvert.DeserializeObject<RenderTemplate>(json);
        }

    }
}
