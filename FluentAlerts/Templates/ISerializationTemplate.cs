using System.Collections.Generic;

namespace FluentAlerts
{
    public interface ISerializationTemplate
    {
        string SerializationHeader { get; }
        string SerializationFooter { get; }
        string AlertHeader { get; }
        string AlertFooter { get; }
        string AlertItemHeader { get; }
        string AlertItemFooter { get; }
        string ValueHeader { get; }
        string ValueFooter { get; }

        string ValueHeaderDecorationForSpanningFormat { get; }
        string ValueHeaderDecorationForSpanningNumberOfColumns { get; }

        string ValueNormalDecoration { get; }
        string ValueEmphasizedDecoration { get; }
        string ValueTitleDecoration { get; }
        string ValueUrlDecoration { get; }
        string ValueSeperatorDecoration { get; }
        string ValueHeaderDecorationForFirstColumn { get; }
        string ValueHeaderDecorationForLastColumn { get; }
        string UrlValue { get; }
        string SeperatorHeader { get; }
        
        IDictionary<string, string> Substitutions { get; }
    }
}