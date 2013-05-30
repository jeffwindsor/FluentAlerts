using System;
using System.Collections.Generic;
using System.Text;

namespace FluentAlerts.Renderers
{
    public interface ITemplateRender
    {
        string GetSerializationHeader();
        string GetSerializationFooter();

        string GetAlertHeader();
        string GetAlertFooter();

        string GetItemHeader(ItemStyle style, int alertWidth);
        string GetItemFooter(ItemStyle style, int alertWidth);

        string GetValueHeader(ItemStyle style, int index, int groupLength, int alertWidth);
        string GetValueFooter(ItemStyle style, int index, int groupLength, int alertWidth);
    }

    public class TemplateRenderer : ITemplateRender
    {
        private readonly Template _template;
        public TemplateRenderer(Template template)
        {
            _template = template;
        }

        private string GetValue(TemplateItem item)
        {
            return _template[item.ToString()];
        }

        private string GetValue(TemplateItem item, IEnumerable<Substitution> substitutions)
        {
            //Replace all parameters given with values
            var sb = new StringBuilder(GetValue(item));
            foreach (var s in substitutions)
            {
                sb.Replace(s.Parameter, s.Value);
            }
            return sb.ToString();
        }

        public string GetSerializationHeader()
        {
            return GetValue(TemplateItem.SerializationHeader);
        }

        public string GetSerializationFooter()
        {
            return GetValue(TemplateItem.SerializationFooter);
        }

        public string GetAlertHeader()
        {
            return GetValue(TemplateItem.AlertHeader);
        }

        public string GetAlertFooter()
        {
            return GetValue(TemplateItem.AlertFooter);
        }
     
        public string GetItemHeader(ItemStyle style, int alertWidth)
        {
            var subs = new[] {new Substitution(TemplateValueParameter.Colspan, alertWidth.ToString())};
            return GetValue(TemplateItem.GroupHeader, subs);
        }

        public string GetItemFooter(ItemStyle style, int alertWidth)
        {
            var subs = new[] {new Substitution(TemplateValueParameter.Colspan, alertWidth.ToString())};
            return GetValue(TemplateItem.GroupFooter, subs);
        }

        //i begins at 1 and goes to length
        public string GetValueHeader(ItemStyle style, int index, int groupLength, int alertWidth)
        {
            var spanning = index == groupLength && groupLength < alertWidth;
            var item = spanning
                           ? TemplateItem.ValueSpanningHeader
                           : TemplateItem.ValueNormalHeader;

            switch (style)
            {
                case ItemStyle.NormalText:
                    item = TemplateItem.TextNormalHeader;
                    break;
                case ItemStyle.EmphasizedText:
                    item = TemplateItem.TextEmphasizedHeader;
                    break;
                case ItemStyle.HeaderOneText:
                    item = TemplateItem.TextTitleHeader;
                    break;
                case ItemStyle.EmphasizedRow:
                    item = spanning
                               ? TemplateItem.ValueEmphasizedSpanningHeader
                               : TemplateItem.ValueEmphasizedHeader;
                    break;
                    
                case ItemStyle.Seperator:
                    item = TemplateItem.SeperatorHeader;
                    break;

                case ItemStyle.Url:
                    item = TemplateItem.UrlHeader;
                    break;

            }
            //Make the last value a spanning region if group width < alert width
            var subs = new[] { new Substitution(TemplateValueParameter.Colspan, (alertWidth - groupLength).ToString()) };
            return GetValue(item, subs);
        }

        public string GetValueFooter(ItemStyle style, int index, int groupLength, int alertWidth)
        {
            var spanning = index == groupLength && groupLength < alertWidth;
            var item = spanning
                           ? TemplateItem.ValueSpanningFooter
                           : TemplateItem.ValueNormalFooter;

            switch (style)
            {
                case ItemStyle.NormalText:
                    item = TemplateItem.TextNormalFooter;
                    break;
                case ItemStyle.EmphasizedText:
                    item = TemplateItem.TextEmphasizedFooter;
                    break;
                case ItemStyle.HeaderOneText:
                    item = TemplateItem.TextTitleFooter;
                    break;

                case ItemStyle.EmphasizedRow:
                    item = spanning
                               ? TemplateItem.ValueEmphasizedSpanningFooter
                               : TemplateItem.ValueEmphasizedFooter;
                    break;

                case ItemStyle.Seperator:
                    item = TemplateItem.SeperatorFooter;
                    break;

                case ItemStyle.Url:
                    item = TemplateItem.UrlFooter;
                    break;

            }
            //Make the last value a spanning region if group width < alert width
            var subs = new[] { new Substitution(TemplateValueParameter.Colspan, (alertWidth - groupLength).ToString()) };
            return GetValue(item, subs);
        }
        
        private class Substitution
        {
            public Substitution(TemplateValueParameter parameter, string value)
            {
                Parameter = "{" + parameter + "}";
                Value = value;
            }
            public string Parameter { get; private set; }
            public string Value { get; private set; }
        }
    }
}
