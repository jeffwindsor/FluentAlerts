using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentAlerts.Transformers;

namespace FluentAlerts
{
    public class FluentAlertSerializer<TSerializationTemplate>: IFluentAlertTemplateSerializer<TSerializationTemplate> 
        where TSerializationTemplate : ISerializationTemplate, new()
    {
        private const string ValueHeaderDecorationForSpanning_ColumnsToSpan = "{ColumnsToSpan}";
        private readonly StringBuilder _acc = new StringBuilder();
        private readonly ISerializationTemplate _template;
        private readonly ITransformer _transformer;
        public FluentAlertSerializer(ITransformer transformer)
        {
            _template = new TSerializationTemplate();
            _transformer = transformer;
        }

        /// <summary>
        /// Serializes the complete alert tree, any non transformed bojects
        /// will be transformed then formatted for output
        /// </summary>
        public string Serialize(IFluentAlert alert)
        {
            if (alert == null) return string.Empty;

            //Document Header
            Append(_template.SerializationHeader);
            
            //Render Internals
            RenderAlert(alert);

            //Document Footer
            Append(_template.SerializationFooter);

            //Return result
            return _acc.ToString(); 
        }

        private string RenderValueHeader(string style, int index, int maximumItemsValueIndex, int maximumValueIndex)
        {
            var sb = new StringBuilder();
            //Requires Spanning Decoration
            if (maximumItemsValueIndex < maximumValueIndex && index == maximumItemsValueIndex)
            {
                var columnsToSpan = (1 + (maximumValueIndex - index)).ToString();
                sb.AppendFormat(_template.ValueHeaderDecorationForSpanningFormat, " " + columnsToSpan + " ");
            }
            //First and Last Column Decorations
            if (index == 0)
            {
                sb.Append(_template.ValueHeaderDecorationForFirstColumn);
            }
            else if (maximumItemsValueIndex > 0 && index == maximumItemsValueIndex)
            {
                sb.Append(_template.ValueHeaderDecorationForLastColumn);
            }

            //ADD Style DECORATION
            var templateItem = _template.ValueHeader;
            var styleDecoration = GetValueStyleDecoration(style, ref templateItem);
                    private string GetValueStyleDecoration(AlertItemStyle style, ref string templateItem)
        {
            switch (style)
            {
                case AlertItemStyle.Normal:
                    return GetTemplateItemByType(SerializationTemplateItemType.ValueNormalDecoration, " ");
                case AlertItemStyle.Emphasized:
                    return GetTemplateItemByType(SerializationTemplateItemType.ValueEmphasizedDecoration, " ");
                case AlertItemStyle.Title:
                    return GetTemplateItemByType(SerializationTemplateItemType.ValueTitleDecoration, " ");
                case AlertItemStyle.Seperator:
                    // if sperator template exists, replace item template with seperator template.
                    var seperatorItem = GetTemplateItemByType(SerializationTemplateItemType.SeperatorHeader);
                    if (!string.IsNullOrEmpty(seperatorItem)) templateItem = seperatorItem;

                    return GetTemplateItemByType(SerializationTemplateItemType.ValueSeperatorDecoration, " ");

                case AlertItemStyle.Url:
                    return GetTemplateItemByType(SerializationTemplateItemType.ValueUrlDecoration, " ");
            }
            return string.Empty;
        }

            sb.Append(styleDecoration);

            var args = new[]
                {
                    new RenderTemplateArguement(SerializationArguementType.Decorations,
                                                sb.ToString())
                };
            return RenderTemplateItem(templateItem, args);
        }

        private string ApplySubstitutions(string text)
        {
            var sb = new StringBuilder(text);
            foreach (var substitution in _alertRenderTemplate.Substitutions)
            {
                sb.Replace(substitution.Key, substitution.Value);
            }
            return sb.ToString();
        }

        #region Traverse Tree
        /// <summary>
        /// Internal render function for alerts, original and embedded 
        /// alerts will use this function
        /// </summary>
        /// <param name="alert"></param>
        private void RenderAlert(IFluentAlert alert) 
        {
            //Alert Header
            Append(_template.AlertHeader);

            //Route each item in Alert by type
            var maximumValueIndex = GetMaximumValueCount(alert) - 1;
            foreach (var item in alert)
            {
                //Add Alert Item
                RouteAlertItem(item, maximumValueIndex);
            }
            
            //Alert Footer
            Append(_template.AlertFooter);
        }

        private void RouteAlertItem(IFluentAlertItem item, int maximumValueIndex)
        {
            //Route all embeded IAlerts back to the IAlert Render Method
            var alert = item as IFluentAlert;
            if (alert != null)
            {
                RenderAlert(alert);
                return;
            }

            //Route all AlertItems to Render IAlertItem Method
            var alertItem = item as AlertItem;
            if (alertItem != null)
            {
                RenderAlertItem(alertItem, maximumValueIndex);
                return;
            }

            //Unknown type
            throw new ArgumentException(string.Format("Alert item type [{0}] not recognized by serilazer", item.GetType().Name), "item");
        }

        private void RenderAlertItem(AlertItem g, int maximumValueIndex)
        {
            var maximumItemsValueIndex = g.Values.Count - 1;

            //Alert Item Header
            Append(_template.AlertItemHeader);
            for (var index = 0; index <= maximumItemsValueIndex; index++)
            {
                //Value Header
                Append(RenderValueHeader(g.Style, index, maximumItemsValueIndex, maximumValueIndex));

                //Add Value
                RouteValue(g.Values[index]);

                //Value Footer
                Append(_template.ValueFooter);

            }
            //Alert Item Footer
            Append(_template.AlertItemFooter);
        }

        private void RouteValue(object value)
        {
            //Route all embedded IAlertBuilders (Mistake but hard to see in traces, so covering here for free) back to the IAlert Render Method
            var builder = value as IFluentAlertBuilder;
            if (builder != null)
            {
                var alert = builder.ToAlert();
                RenderAlert(alert);
                return;
            }

            //Route all embedded IAlerts back to the IAlert Render Method
            var fluentAlert = value as IFluentAlert;
            if (fluentAlert != null)
            {
                RenderAlert(fluentAlert);
                return;
            }
            
            //Value is of output type, so append it to output stream
            var s = value as string;
            if (s != null)
            {
                Append(ApplySubstitutions(s));
                return;
            }

            //Anything else (non-transformed objects), transform the object and re-route 
            var transformed = _transformer.Transform(value);
            RouteValue(transformed);
        }
 
        private void Append(string text)
        {
            _acc.Append(text);
        }

        private static int GetMaximumValueCount(IEnumerable<IFluentAlertItem> alert)
        {
            // return max number of values
            var widths = (from item in alert
                let alertItem = item as AlertItem
                where alertItem != null
                select alertItem.Values.Count)
                .ToList();

            return widths.Any()
                       ? Math.Max(1, widths.Max())
                       : 1;
        }
        #endregion

        #region Decorations
        
        //private string GetTemplateItemByType(SerializationTemplateItemType type, string prefix = "",
        //                                     string postfix = "")
        //{
        //    //Find key in dictionary, return empty sting if not found
        //    var key = type.ToString();
        //    return _alertRenderTemplate.Templates.ContainsKey(key)
        //               ? string.Format("{0}{1}{2}", prefix, _alertRenderTemplate.Templates[key], postfix)
        //               : string.Empty;
        //}







        private static string RenderTemplateItem(string template, string replaceThis, string withThat)
        {
            return template.Replace("{" + replaceThis + "}", withThat);
        }

        #endregion
    }
}
