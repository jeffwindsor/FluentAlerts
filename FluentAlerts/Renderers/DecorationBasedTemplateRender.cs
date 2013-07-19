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
     
        public string RenderAlertItemHeader(ItemStyle style)
        {
            return GetTemplateItemByType(DecorationBasedRenderTemplateItemType.AlertItemHeader);
        }
        public string RenderAlertItemFooter(ItemStyle style)
        {
            return GetTemplateItemByType(DecorationBasedRenderTemplateItemType.AlertItemFooter);
        }

        public string RenderValueHeader(ItemStyle style, int index, int maximumItemsValueIndex, int maximumValueIndex)
        {
            var decorationValue = new StringBuilder();
            var templateItem = GetTemplateItemByType(DecorationBasedRenderTemplateItemType.ValueHeader);
            var requiresSpanning = (maximumItemsValueIndex < maximumValueIndex && index == maximumItemsValueIndex);

            //ADD SPANNING DECORATION: if item's value count is less than alerts && this is the last value in the item
            if (requiresSpanning)
            {
                //span all remaining columns
                var span = 1 + (maximumValueIndex - index);
                var spanArgs = new[]
                    {
                        new RenderTemplateArguement(DecorationBasedRenderTemplateArguementType.SpanColumns,
                                                    span.ToString())
                    }; 
                decorationValue.Append(
                    RenderTemplateItem(DecorationBasedRenderTemplateItemType.ValueSpanningDecoration, spanArgs, " "));
            }

            //First Column by Id (may be bad css style)
            if (!requiresSpanning && index == 0)
                decorationValue.Append(GetTemplateItemByType(DecorationBasedRenderTemplateItemType.ValueFirstColumnId, " "));
            
            //Last Column by Id (may be bad css style)
            if (!requiresSpanning && index == maximumItemsValueIndex)
                decorationValue.Append(GetTemplateItemByType(DecorationBasedRenderTemplateItemType.ValueLastColumnId, " "));

            //ADD Other DECORATION
            switch (style)
            {
                case ItemStyle.Normal:
                    decorationValue.Append(GetTemplateItemByType(DecorationBasedRenderTemplateItemType.ValueNormalDecoration, " "));
                    break;
                case ItemStyle.Emphasized:
                    decorationValue.Append(GetTemplateItemByType(DecorationBasedRenderTemplateItemType.ValueEmphasizedDecoration, " " ));
                    break;
                case ItemStyle.Title:
                    decorationValue.Append(GetTemplateItemByType(DecorationBasedRenderTemplateItemType.ValueTitleDecoration, " "));
                    break;
                case ItemStyle.Seperator:
                    //sub in seperator template if it exists
                    var seperatorItem = GetTemplateItemByType(DecorationBasedRenderTemplateItemType.SeperatorHeader);
                    if (!string.IsNullOrEmpty(seperatorItem))
                        templateItem = seperatorItem;
                    decorationValue.Append(GetTemplateItemByType(DecorationBasedRenderTemplateItemType.ValueSeperatorDecoration, " "));
                    break;
                case ItemStyle.Url:
                    decorationValue.Append(GetTemplateItemByType(DecorationBasedRenderTemplateItemType.ValueUrlDecoration, " "));
                    break;
            }
            
            var args = new[]
                {
                    new RenderTemplateArguement(DecorationBasedRenderTemplateArguementType.Decorations, decorationValue.ToString())
                };
            return RenderTemplateItem(templateItem, args);
        }

        public string RenderValueFooter(ItemStyle style, int index, int maximumItemsValueIndex, int maximumValueIndex)
        {
            return GetTemplateItemByType(DecorationBasedRenderTemplateItemType.ValueFooter);
        }
        
        private string GetTemplateItemByType(DecorationBasedRenderTemplateItemType type, string prefix = "", string postfix = "")
        {
            //Find key in dictionary, return empty sting if not found
            var key = type.ToString();
            return _alertRenderTemplate.ContainsKey(key)
                       ? string.Format("{0}{1}{2}", prefix, _alertRenderTemplate[key], postfix)
                       : string.Empty;
        }

        private string RenderTemplateItem(DecorationBasedRenderTemplateItemType type, IEnumerable<RenderTemplateArguement> args, string prefix  = "", string postfix = "")
        {
            //Replace all parameters given with values
            var template = GetTemplateItemByType(type, prefix, postfix);
            return RenderTemplateItem(template, args);
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
