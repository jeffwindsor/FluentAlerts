//using System.Collections.Generic;
//using System.Text;
//using FluentAlerts.Templates;
//using FluentAlerts.Transformers;

//namespace FluentAlerts
//{
//    public class FluentAlertTemplateBasedTransformableSerializer : FluentAlertSerializer
//    {
//        private readonly SerializationTemplate _alertRenderTemplate;
//        private readonly ITransformer _transformer;
//        public FluentAlertTemplateBasedTransformableSerializer(SerializationTemplate alertRenderTemplate, ITransformer transformer)
//        {
//            _alertRenderTemplate = alertRenderTemplate;
//            _transformer = transformer;
//        }


//        protected override string RenderAlertItemHeader(AlertItemStyle style)
//        {
//            return GetTemplateItemByType(SerializationTemplateItemType.AlertItemHeader);
//        }

//        protected override string RenderAlertItemFooter(AlertItemStyle style)
//        {
//            return GetTemplateItemByType(SerializationTemplateItemType.AlertItemFooter);
//        }

//        protected override string RenderValueHeader(AlertItemStyle style, int index, int maximumItemsValueIndex, int maximumValueIndex)
//        {
//            var decorationValue = new StringBuilder();
//            var templateItem = GetTemplateItemByType(SerializationTemplateItemType.ValueHeader);
//            var requiresSpanning = (maximumItemsValueIndex < maximumValueIndex && index == maximumItemsValueIndex);

//            //ADD SPANNING DECORATION: if item's value count is less than alerts && this is the last value in the item
//            if (requiresSpanning)
//                decorationValue.Append(GetSpanningDecoration(index, maximumValueIndex));

//            //ADD ID DECOARTION: FOr First And Last Column (if both use first column id)
//            if (index == 0)
//                decorationValue.Append(GetTemplateItemByType(SerializationTemplateItemType.ValueFirstColumnId,
//                                                             " "));
//            else if (maximumItemsValueIndex > 0 && index == maximumItemsValueIndex)
//                decorationValue.Append(GetTemplateItemByType(SerializationTemplateItemType.ValueLastColumnId,
//                                                             " "));

//            //ADD Style DECORATION
//            var styleDecoration = GetValueStyleDecoration(style, ref templateItem);

//            decorationValue.Append(styleDecoration);

//            var args = new[]
//                {
//                    new RenderTemplateArguement(SerializationArguementType.Decorations,
//                                                decorationValue.ToString())
//                };
//            return RenderTemplateItem(templateItem, args);
//        }

//        protected override string RenderValueFooter(AlertItemStyle style, int index, int maximumItemsValueIndex, int maximumValueIndex)
//        {
//            return GetTemplateItemByType(SerializationTemplateItemType.ValueFooter);
//        }

//        protected override string ApplySubstitutions(string text)
//        {
//            var sb = new StringBuilder(text);
//            foreach (var substitution in _alertRenderTemplate.Substitutions)
//            {
//                sb.Replace(substitution.Key, substitution.Value);
//            }
//            return sb.ToString();
//        }

//        protected override object Transform(object o)
//        {
//            return _transformer.Transform(o);
//        }

//        #region Templates
//        private string GetTemplateItemByType(SerializationTemplateItemType type, string prefix = "",
//                                             string postfix = "")
//        {
//            //Find key in dictionary, return empty sting if not found
//            var key = type.ToString();
//            return _alertRenderTemplate.Templates.ContainsKey(key)
//                       ? string.Format("{0}{1}{2}", prefix, _alertRenderTemplate.Templates[key], postfix)
//                       : string.Empty;
//        }

//        private string RenderTemplateItem(SerializationTemplateItemType type,
//                                          IEnumerable<RenderTemplateArguement> args, string prefix = "",
//                                          string postfix = "")
//        {
//            //Replace all parameters given with values
//            var template = GetTemplateItemByType(type, prefix, postfix);
//            return RenderTemplateItem(template, args);
//        }

//        private string GetSpanningDecoration(int index, int maximumValueIndex)
//        {
//            //span all remaining columns
//            var span = 1 + (maximumValueIndex - index);
//            var spanArgs = new[]
//                {
//                    new RenderTemplateArguement(SerializationArguementType.SpanColumns,
//                                                span.ToString())
//                };
//            return RenderTemplateItem(SerializationTemplateItemType.ValueSpanningDecoration, spanArgs, " ");
//        }

//        private string GetValueStyleDecoration(AlertItemStyle style, ref string templateItem)
//        {
//            switch (style)
//            {
//                case AlertItemStyle.Normal:
//                    return GetTemplateItemByType(SerializationTemplateItemType.ValueNormalDecoration, " ");
//                case AlertItemStyle.Emphasized:
//                    return GetTemplateItemByType(SerializationTemplateItemType.ValueEmphasizedDecoration, " ");
//                case AlertItemStyle.Title:
//                    return GetTemplateItemByType(SerializationTemplateItemType.ValueTitleDecoration, " ");
//                case AlertItemStyle.Seperator:
//                    // if sperator template exists, replace item template with seperator template.
//                    var seperatorItem = GetTemplateItemByType(SerializationTemplateItemType.SeperatorHeader);
//                    if (!string.IsNullOrEmpty(seperatorItem)) templateItem = seperatorItem;

//                    return GetTemplateItemByType(SerializationTemplateItemType.ValueSeperatorDecoration, " ");

//                case AlertItemStyle.Url:
//                    return GetTemplateItemByType(SerializationTemplateItemType.ValueUrlDecoration, " ");
//            }
//            return string.Empty;
//        }

//        private static string RenderTemplateItem(string template, IEnumerable<RenderTemplateArguement> args)
//        {
//            //Replace all parameters given with values
//            var sb = new StringBuilder(template);
//            foreach (var s in args)
//            {
//                sb.Replace(s.Name, s.Value);
//            }
//            return sb.ToString();
//        }

//        private class RenderTemplateArguement
//        {
//            public RenderTemplateArguement(SerializationArguementType name, string value)
//            {
//                Name = "{" + name + "}";
//                Value = value;
//            }

//            public string Name { get; private set; }
//            public string Value { get; private set; }
//        }
//        #endregion
//    }
//}
