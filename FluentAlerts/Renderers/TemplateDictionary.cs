using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using FluentAlerts.Settings;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace FluentAlerts.Renderers
{
    public class Template : Dictionary<string, string>
    {
        //public string Export(string fileName)
        //{
        //    return FileExporter.Export(fileName, Serialize(this));
        //}

        //private static string Serialize(Template o)
        //{
        //    return JsonConvert.SerializeObject(o, new KeyValuePairConverter());
        //}

        //private static Template Deserialize(string json)
        //{
        //    return JsonConvert.DeserializeObject<Template>(json);
        //}
    }
    internal class TemplateDictionary
    {
        private const string DefaultTemplates =
              "{ "
            + "\"HtmlWithEmbeddedCssTableTemplate\"" + ":" + "{\"SerializationHeader\":\"<html><body>\",\"SerializationFooter\":\"</html></body>\",\"AlertHeader\":\"<table cellspacing='1' cellpadding='2' style='font-family:Arial,Sans-Serif;font-size:8pt;' width='100%'>\",\"AlertFooter\":\"</table>\",\"TextNormalHeader\":\"<TR><TD colspan=' {Colspan}' style='bgcolor=gainsboro;'>\",\"TextNormalFooter\":\"</TD></TR>\",\"TextTitleHeader\":\"<TR><TD colspan=' {Colspan}' style='bgcolor=silver;font-size:10pt;font-weight:bold;'>\",\"TextTitleFooter\":\"</TD></TR>\",\"TextEmphasizedHeader\":\"<TR><TD colspan=' {Colspan}' style='bgcolor=silver;font-weight:bold;'>\",\"TextEmphasizedFooter\":\"</TD></TR>\",\"GroupHeader\":\"<TR>\",\"GroupFooter\":\"</TR>\",\"ValueNormalHeader\":\"<TD style='bgcolor=whitesmoke;'>\",\"ValueNormalValue\":\"{Value}\",\"ValueNormalFooter\":\"</TD>\",\"ValueEmphasizedHeader\":\"<TD colspan=' {Colspan}' style='bgcolor=silver;font-weight:bold;'>\",\"ValueEmphasizedValue\":\"{Value}\",\"ValueEmphasizedFooter\":\"</TD>\",\"ValueSpanningHeader\":\"<TD colspan=' {Colspan}' style='bgcolor=gainsboro;font-weight:bold;'>\",\"ValueSpanningValue\":\"{Value}\",\"ValueSpanningFooter\":\"</TD>\",\"UrlHeader\":\"<TD style='font-weight:bold;'>\",\"UrlValue\":\"<a href='{Url}'>{UrlTitle}</a>\",\"UrlFooter\":\"</TD>\",\"SeperatorHeader\":\"<TD colspan=' {Colspan}' style='bgcolor=gainsboro;font-weight:bold;'>\",\"SeperatorValue\":\"{Value}\",\"SeperatorFooter\":\"</TD>\"}"
            + "}";

        private readonly Dictionary<string, Template> _inner;
        private readonly IEnumerable<string> _keys; 
        public TemplateDictionary(string fileName = "")
        {
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
                       : Factory.Issues.HandleRenderTemplateNotFound(templateName);
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
        
    }
}

//private static IDictionary<string, string> GetHtmlWithEmbeddedCssTableTemplate()
//{
//    var result = new Dictionary<string, string>();

//    //BODY
//    result[TemplateKeys.SerializationHeader.ToString()] = "<html><body>";
//    result[TemplateKeys.SerializationFooter.ToString()] = "</html></body>";

//    //Sections 
//    result[TemplateKeys.AlertHeader.ToString()] = "<table cellspacing='1' cellpadding='2' style='font-family:Arial,Sans-Serif;font-size:8pt;' width='100%'>";
//    result[TemplateKeys.AlertFooter.ToString()] = "</table>";

//    //TEXT - NORMAL => ROW + SPANNING VALUE
//    result[TemplateKeys.TextNormalHeader.ToString()] = "<TR><TD colspan=' {Colspan}' style='bgcolor=gainsboro;'>";
//    result[TemplateKeys.TextNormalFooter.ToString()] = "</TD></TR>";

//    //TEXT - TITLE
//    result[TemplateKeys.TextTitleHeader.ToString()] = "<TR><TD colspan=' {Colspan}' style='bgcolor=silver;font-size:10pt;font-weight:bold;'>";
//    result[TemplateKeys.TextTitleFooter.ToString()] = "</TD></TR>";

//    //TEXT - EMPAHSIZED => ROW + EMPHASIZED VALUE
//    result[TemplateKeys.TextEmphasizedHeader.ToString()] = "<TR><TD colspan=' {Colspan}' style='bgcolor=silver;font-weight:bold;'>";
//    result[TemplateKeys.TextEmphasizedFooter.ToString()] = "</TD></TR>";

//    //ROW 
//    result[TemplateKeys.GroupHeader.ToString()] = "<TR>";
//    result[TemplateKeys.GroupFooter.ToString()] = "</TR>";

//    //VALUE NORMAL
//    result[TemplateKeys.ValueNormalHeader.ToString()] = "<TD style='bgcolor=whitesmoke;'>";
//    result[TemplateKeys.ValueNormalValue.ToString()] = "{Value}";
//    result[TemplateKeys.ValueNormalFooter.ToString()] = "</TD>";

//    //EMPHASIZED VALUE
//    result[TemplateKeys.ValueEmphasizedHeader.ToString()] = "<TD colspan=' {Colspan}' style='bgcolor=silver;font-weight:bold;'>";
//    result[TemplateKeys.ValueEmphasizedValue.ToString()] = "{Value}";
//    result[TemplateKeys.ValueEmphasizedFooter.ToString()] = "</TD>";

//    //SPANNING VALUE
//    result[TemplateKeys.ValueSpanningHeader.ToString()] = "<TD colspan=' {Colspan}' style='bgcolor=gainsboro;font-weight:bold;'>";
//    result[TemplateKeys.ValueSpanningValue.ToString()] = "{Value}";
//    result[TemplateKeys.ValueSpanningFooter.ToString()] = "</TD>";

//    //URL 
//    result[TemplateKeys.UrlHeader.ToString()] = "<TD style='font-weight:bold;'>";
//    result[TemplateKeys.UrlValue.ToString()] = "<a href='{Url}'>{UrlTitle}</a>";
//    result[TemplateKeys.UrlFooter.ToString()] = "</TD>";

//    //SEPERATOR
//    result[TemplateKeys.SeperatorHeader.ToString()] = "<TD colspan=' {Colspan}' style='bgcolor=gainsboro;font-weight:bold;'>";
//    result[TemplateKeys.SeperatorValue.ToString()] = "{Value}";
//    result[TemplateKeys.SeperatorFooter.ToString()] = "</TD>";

//    return result;
//}

//public static IDictionary<string, string> GetHtmlWithEmbeddedCssDocumentTemplate()
//{
//    var result = new Dictionary<string, string>();
//    //PLAIN HTML TEMPLATE
//    result["SerializationHeader = "<html><body>";
//    result["SerializationFooter = "</html></body>";
//    result["AlertHeader = "<span style='font-family:Arial,Sans-Serif;font-size:9pt;'>";
//    result["AlertFooter = "</span><BR>";

//    //TEXT - NORMAL
//    result["TextNormalHeader = "<span style='font-family:Arial,Sans-Serif;font-size:9pt;'>";
//    result["TextNormalFooter = "</span><BR>";
//    //TEXT - TITLE
//    result["TextTitleHeader = "<span style='font-family:Arial,Sans-Serif;font-size:11pt;font-weight:bold;'>";
//    result["TextTitleFooter = "</span><BR>";
//    //TEXT - EMPAHSIZED
//    result["TextEmphasizedHeader = "<span style='font-family:Arial,Sans-Serif;font-size:9pt;font-weight:bold;'>";
//    result["TextEmphasizedFooter = "</span><BR>";

//    //URL 
//    result["UrlHeader = "<span style='font-family:Arial,Sans-Serif;font-size:11pt;font-weight:bold;'>";
//    result["UrlValue = "<a href='{Url}'>{UrlTitle}</a>";
//    result["UrlFooter = "</span><BR>";

//    //SEPERATOR
//    result["SeperatorHeader = "";
//    result["SeperatorFormat = "";
//    result["SeperatorFooter = "<HR>";

//    //VALUE
//    //ROW
//    //EMPHASIZED_ROW
//    //SPANNING VALUE

//    return result;
//}