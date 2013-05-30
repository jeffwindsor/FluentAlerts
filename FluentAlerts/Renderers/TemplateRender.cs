using System;
using System.Collections.Generic;
using System.Text;

namespace FluentAlerts.Renderers
{
    public interface TemplateRender
    {
        string GetSerializationHeader();
        string GetSerializationFooter();

        string GetAlertHeader();
        string GetAlertFooter();

        string GetTextBlockHeader(TextStyle style, int alertWidth);
        string GetTextBlockFooter(TextStyle style, int alertWidth);

        string GetGroupHeader(GroupStyle style, int alertWidth);
        string GetGroupFooter(GroupStyle style, int alertWidth);

        string GetValueHeader(GroupStyle style, int index, int groupLength, int alertWidth);
        string GetValueFooter(GroupStyle style, int index, int groupLength, int alertWidth);
    }

    public class TemplateRenderer : TemplateRender
    {
        private int _currentAlertWidth;
        private int _currentGroupWidth;

        private Template _template;
        public TemplateRenderer(Template template)
        {
            _template = template;
        }

        private class Substitution
        {
            public Substitution(TemplateValueParameter parameter, string value)
            {
                Parameter = parameter.ToString();
                Value = value;
            }
            public string Parameter { get; private set; }
            public string Value { get; private set; }
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

        public string GetTextBlockHeader(TextStyle style, int alertWidth)
        {
            var subs = new [] {new Substitution(TemplateValueParameter.Colspan, alertWidth.ToString())};
            var item = TemplateItem.TextNormalHeader;
            switch (style)
            {
                case TextStyle.Emphasized:
                    item = TemplateItem.TextEmphasizedHeader;
                    break;
                case TextStyle.Header_One:
                    item = TemplateItem.TextTitleHeader;
                    break;
            }
            return GetValue(item, subs);
        }

        public string GetTextBlockFooter(TextStyle style, int alertWidth)
        {
            var subs = new[] {new Substitution(TemplateValueParameter.Colspan, alertWidth.ToString())};
            var item = TemplateItem.TextNormalFooter;
            switch (style)
            {
                case TextStyle.Emphasized:
                    item = TemplateItem.TextEmphasizedFooter;
                    break;
                case TextStyle.Header_One:
                    item = TemplateItem.TextTitleFooter;
                    break;
            }
            return GetValue(item, subs);
        }

        public string GetGroupHeader(GroupStyle style, int alertWidth)
        {
            var subs = new[] {new Substitution(TemplateValueParameter.Colspan, alertWidth.ToString())};
            return GetValue(TemplateItem.GroupHeader, subs);
        }

        public string GetGroupFooter(GroupStyle style, int alertWidth)
        {
            var subs = new[] {new Substitution(TemplateValueParameter.Colspan, alertWidth.ToString())};
            return GetValue(TemplateItem.GroupFooter, subs);
        }

        //i begins at 1 and goes to length
        public string GetValueHeader(GroupStyle style, int index, int groupLength, int alertWidth)
        {
            var spanning = index == groupLength && groupLength < alertWidth;
            var item = spanning
                           ? TemplateItem.ValueSpanningHeader
                           : TemplateItem.ValueNormalHeader;

            switch (style)
            {
                case GroupStyle.EmphasizedRow:
                    item = spanning
                               ? TemplateItem.ValueEmphasizedSpanningHeader
                               : TemplateItem.ValueEmphasizedHeader;
                    break;
                    
                case GroupStyle.Seperator:
                    item = TemplateItem.SeperatorHeader;
                    break;

                case GroupStyle.Url:
                    item = TemplateItem.UrlHeader;
                    break;

            }
            //Make the last value a spanning region if group width < alert width
            var subs = new[] { new Substitution(TemplateValueParameter.Colspan, (alertWidth - groupLength).ToString()) };
            return GetValue(item, subs);
        }

        public string GetValueFooter(GroupStyle style, int index, int groupLength, int alertWidth)
        {
            var spanning = index == groupLength && groupLength < alertWidth;
            var item = spanning
                           ? TemplateItem.ValueSpanningFooter
                           : TemplateItem.ValueNormalFooter;

            switch (style)
            {
                case GroupStyle.EmphasizedRow:
                    item = spanning
                               ? TemplateItem.ValueEmphasizedSpanningFooter
                               : TemplateItem.ValueEmphasizedFooter;
                    break;

                case GroupStyle.Seperator:
                    item = TemplateItem.SeperatorFooter;
                    break;

                case GroupStyle.Url:
                    item = TemplateItem.UrlFooter;
                    break;

            }
            //Make the last value a spanning region if group width < alert width
            var subs = new[] { new Substitution(TemplateValueParameter.Colspan, (alertWidth - groupLength).ToString()) };
            return GetValue(item, subs);
        }
    }
}
