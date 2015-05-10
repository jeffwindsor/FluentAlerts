using System.Collections.Generic;

namespace FluentAlerts.Templates
{
    public class SerializationTemplate
    {
        public string SerializationHeader { get; set; }
        public string SerializationFooter { get; set; }
        public string AlertHeader { get; set; }
        public string AlertFooter { get; set; }
        public string AlertItemHeader { get; set; }
        public string AlertItemFooter { get; set; }
        public string ValueHeader { get; set; }
        public string ValueFooter { get; set; }
        public string ValueSpanningDecoration { get; set; }
        public string ValueNormalDecoration { get; set; }
        public string ValueEmphasizedDecoration { get; set; }
        public string ValueTitleDecoration { get; set; }
        public string ValueUrlDecoration { get; set; }
        public string ValueSeperatorDecoration { get; set; }
        public string ValueFirstColumnId { get; set; }
        public string ValueLastColumnId { get; set; }
        public string UrlValue { get; set; }
        public string SeperatorHeader { get; set; }
        
        public Dictionary<string, string> Substitutions;
    }
}