using System;
using System.Collections.Generic;
using System.Text;

namespace FluentAlerts.Renderers
{
    public class TemplateRenderer : ITemplateRender
    {
        private readonly RenderTemplate _alertRenderTemplate;
        public TemplateRenderer(RenderTemplate alertRenderTemplate)
        {
            _alertRenderTemplate = alertRenderTemplate;
        }

        public string RenderHeader()
        {
            return RenderTemplateItem(RenderTemplateItemType.SerializationHeader);
        }
        public string RenderFooter()
        {
            return RenderTemplateItem(RenderTemplateItemType.SerializationFooter);
        }

        public string RenderAlertHeader()
        {
            return RenderTemplateItem(RenderTemplateItemType.AlertHeader);
        }
        public string RenderAlertFooter()
        {
            return RenderTemplateItem(RenderTemplateItemType.AlertFooter);
        }
     
        public string RenderAlertItemHeader(ItemStyle style)
        {
            return RenderTemplateItem(RenderTemplateItemType.AlertItemHeader);
        }
        public string RenderAlertItemFooter(ItemStyle style)
        {
            return RenderTemplateItem(RenderTemplateItemType.AlertItemFooter);
        }

        public string RenderValueHeader(ItemStyle style, int index, int maximumItemsValueIndex, int maximumValueIndex)
        {
            var decorationValue = new StringBuilder();

            //ADD SPANNING DECORATION: if item's value count is less than alerts && this is the last value in the item
            if  (maximumItemsValueIndex < maximumValueIndex && index == maximumItemsValueIndex);
                decorationValue.Append(" ").Append(RenderTemplateItem(RenderTemplateItemType.ValueSpanningDecoration));

            //ADD STYLE DECORATION
            switch (style)
            {
                case ItemStyle.Normal:
                    decorationValue.Append(" ").Append(RenderTemplateItem(RenderTemplateItemType.ValueNormalDecoration));
                    break;
                case ItemStyle.Emphasized
                    decorationValue.Append(" ").Append(RenderTemplateItem(RenderTemplateItemType.));
                    break;
                case ItemStyle.Title:
                    decorationValue.Append(" ").Append(RenderTemplateItem(RenderTemplateItemType.));
                    break;
                case ItemStyle.Seperator:  WHAT TO DO HERE?
                    decorationValue.Append(" ").Append(RenderTemplateItem(RenderTemplateItemType.));
                    break;
                case ItemStyle.Url:
                    decorationValue.Append(" ").Append(RenderTemplateItem(RenderTemplateItemType.));
                    break;
            }
            
            var args = new[]
                {
                    new RenderTemplateArguement(RenderTemplateArguementType.Decorations, decorationValue.ToString())
                };
            return RenderTemplateItem(RenderTemplateItemType.ValueHeader, args);
        }

        public string RenderValueFooter(ItemStyle style, int index, int maximumItemsValueIndex, int maximumValueIndex)
        {
            var spanning = index == maximumItemsValueIndex && maximumItemsValueIndex < maximumValueIndex;
            var item = spanning
                           ? RenderTemplateItemType.ValueSpanningFooter
                           : RenderTemplateItemType.ValueNormalFooter;

            switch (style)
            {
                case ItemStyle.NormalText:
                    item = RenderTemplateItemType.TextNormalFooter;
                    break;
                case ItemStyle.EmphasizedText:
                    item = RenderTemplateItemType.TextEmphasizedFooter;
                    break;
                case ItemStyle.HeaderOneText:
                    item = RenderTemplateItemType.TextTitleFooter;
                    break;

                case ItemStyle.EmphasizedRow:
                    item = spanning
                               ? RenderTemplateItemType.ValueEmphasizedSpanningFooter
                               : RenderTemplateItemType.ValueEmphasizedFooter;
                    break;

                case ItemStyle.Seperator:
                    item = RenderTemplateItemType.SeperatorFooter;
                    break;

                case ItemStyle.Url:
                    item = RenderTemplateItemType.UrlFooter;
                    break;

            }
            //Make the last value a spanning region if group width < alert width
            var subs = new[] { new RenderTemplateArguement(RenderTemplateArguementType.Colspan, (maximumValueIndex - maximumItemsValueIndex).ToString()) };
            return RenderTemplateItem(item, subs);
        }
        


        private string RenderTemplateItem(RenderTemplateItemType item)
        {
            return _alertRenderTemplate[item.ToString()];
        }
        private string RenderTemplateItem(RenderTemplateItemType item, IEnumerable<RenderTemplateArguement> args)
        {
            //Replace all parameters given with values
            var sb = new StringBuilder(RenderTemplateItem(item));
            foreach (var s in args)
            {
                sb.Replace(s.Name, s.Value);
            }
            return sb.ToString();
        }
        
        private class RenderTemplateArguement
        {
            public RenderTemplateArguement(RenderTemplateArguementType name, string value)
            {
                Name = "{" + name + "}";
                Value = value;
            }
            public string Name { get; private set; }
            public string Value { get; private set; }
        }

    }
}
