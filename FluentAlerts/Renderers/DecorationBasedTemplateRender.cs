 using System;
using System.Collections.Generic;
using System.Text;
 

namespace FluentAlerts.Renderers
{
    public class DecorationBasedTemplateRender : ITemplateRender
    {
        private readonly RenderTemplate _alertRenderTemplate;

        public DecorationBasedTemplateRender(RenderTemplate alertRenderTemplate)
        {
            _alertRenderTemplate = alertRenderTemplate;
        }

        public string RenderHeader()
        {
            return GetTemplateItemByType(DecorationBasedRenderTemplateItemType.SerializationHeader);
        }

        public string RenderFooter()
        {
            return GetTemplateItemByType(DecorationBasedRenderTemplateItemType.SerializationFooter);
        }

        public string RenderAlertHeader()
        {
            return GetTemplateItemByType(DecorationBasedRenderTemplateItemType.AlertHeader);
        }

        public string RenderAlertFooter()
        {
            return GetTemplateItemByType(DecorationBasedRenderTemplateItemType.AlertFooter);
        }

        public string RenderAlertItemHeader(AlertItemStyle style)
        {
            return GetTemplateItemByType(DecorationBasedRenderTemplateItemType.AlertItemHeader);
        }

        public string RenderAlertItemFooter(AlertItemStyle style)
        {
            return GetTemplateItemByType(DecorationBasedRenderTemplateItemType.AlertItemFooter);
        }

        public string RenderValueHeader(AlertItemStyle style, int index, int maximumItemsValueIndex, int maximumValueIndex)
        {
            var decorationValue = new StringBuilder();
            var templateItem = GetTemplateItemByType(DecorationBasedRenderTemplateItemType.ValueHeader);
            var requiresSpanning = (maximumItemsValueIndex < maximumValueIndex && index == maximumItemsValueIndex);

            //ADD SPANNING DECORATION: if item's value count is less than alerts && this is the last value in the item
            if (requiresSpanning)
                decorationValue.Append(GetSpanningDecoration(index, maximumValueIndex));

            //ADD ID DECOARTION: FOr First And Last Column (if both use first column id)
            if (index == 0)
                decorationValue.Append(GetTemplateItemByType(DecorationBasedRenderTemplateItemType.ValueFirstColumnId,
                                                             " "));
            else if (maximumItemsValueIndex > 0 && index == maximumItemsValueIndex)
                decorationValue.Append(GetTemplateItemByType(DecorationBasedRenderTemplateItemType.ValueLastColumnId,
                                                             " "));

            //ADD Style DECORATION
            var styleDecoration = GetValueStyleDecoration(style, ref templateItem);

            decorationValue.Append(styleDecoration);

            var args = new[]
                {
                    new RenderTemplateArguement(DecorationBasedRenderTemplateArguementType.Decorations,
                                                decorationValue.ToString())
                };
            return RenderTemplateItem(templateItem, args);
        }

        public string RenderValueFooter(AlertItemStyle style, int index, int maximumItemsValueIndex, int maximumValueIndex)
        {
            return GetTemplateItemByType(DecorationBasedRenderTemplateItemType.ValueFooter);
        }

        public string Scrub(string text)
        {
            var sb = new StringBuilder(text);
            foreach (var substitution in _alertRenderTemplate.Substitutions)
            {
                sb.Replace(substitution.Key, substitution.Value);
            }
            return sb.ToString();
        }

        private string GetTemplateItemByType(DecorationBasedRenderTemplateItemType type, string prefix = "",
                                             string postfix = "")
        {
            //Find key in dictionary, return empty sting if not found
            var key = type.ToString();
            return _alertRenderTemplate.Templates.ContainsKey(key)
                       ? string.Format("{0}{1}{2}", prefix, _alertRenderTemplate.Templates[key], postfix)
                       : string.Empty;
        }

        private string RenderTemplateItem(DecorationBasedRenderTemplateItemType type,
                                          IEnumerable<RenderTemplateArguement> args, string prefix = "",
                                          string postfix = "")
        {
            //Replace all parameters given with values
            var template = GetTemplateItemByType(type, prefix, postfix);
            return RenderTemplateItem(template, args);
        }

        private string GetSpanningDecoration(int index, int maximumValueIndex)
        {
            //span all remaining columns
            var span = 1 + (maximumValueIndex - index);
            var spanArgs = new[]
                {
                    new RenderTemplateArguement(DecorationBasedRenderTemplateArguementType.SpanColumns,
                                                span.ToString())
                };
            return RenderTemplateItem(DecorationBasedRenderTemplateItemType.ValueSpanningDecoration, spanArgs, " ");
        }

        private string GetValueStyleDecoration(AlertItemStyle style, ref string templateItem)
        {
            switch (style)
            {
                case AlertItemStyle.Normal:
                    return GetTemplateItemByType(DecorationBasedRenderTemplateItemType.ValueNormalDecoration, " ");
                case AlertItemStyle.Emphasized:
                    return GetTemplateItemByType(DecorationBasedRenderTemplateItemType.ValueEmphasizedDecoration, " ");
                case AlertItemStyle.Title:
                    return GetTemplateItemByType(DecorationBasedRenderTemplateItemType.ValueTitleDecoration, " ");
                case AlertItemStyle.Seperator:
                    // if sperator template exists, replace item template with seperator template.
                    var seperatorItem = GetTemplateItemByType(DecorationBasedRenderTemplateItemType.SeperatorHeader);
                    if (!string.IsNullOrEmpty(seperatorItem)) templateItem = seperatorItem;

                    return GetTemplateItemByType(DecorationBasedRenderTemplateItemType.ValueSeperatorDecoration, " ");

                case AlertItemStyle.Url:
                    return GetTemplateItemByType(DecorationBasedRenderTemplateItemType.ValueUrlDecoration, " ");
            }
            return string.Empty;
        }

        private static string RenderTemplateItem(string template, IEnumerable<RenderTemplateArguement> args)
        {
            //Replace all parameters given with values
            var sb = new StringBuilder(template);
            foreach (var s in args)
            {
                sb.Replace(s.Name, s.Value);
            }
            return sb.ToString();
        }

        private class RenderTemplateArguement
        {
            public RenderTemplateArguement(DecorationBasedRenderTemplateArguementType name, string value)
            {
                Name = "{" + name + "}";
                Value = value;
            }

            public string Name { get; private set; }
            public string Value { get; private set; }
        }
    }
}
