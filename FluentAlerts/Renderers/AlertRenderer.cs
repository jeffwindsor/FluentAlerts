using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentAlerts.Transformers;

namespace FluentAlerts.Renderers
{
    public class AlertRenderer : IAlertRenderer
    {
        private readonly StringBuilder _acc = new StringBuilder();
        private readonly ITemplateRender _alertTemplate;
        private readonly ITransformer<string> _transformer;
        public AlertRenderer(ITransformer<string> transformer, ITemplateRender alertTemplate)
        {
            _transformer = transformer;
            _alertTemplate = alertTemplate;
        }

        /// <summary>
        /// Serializes the complete alert tree, any non transformed bojects
        /// will be transformed then formatted for output
        /// </summary>
        public string Render(IAlert alert)
        {
            if (alert == null) return string.Empty;

            //Document Header
            Append(_alertTemplate.RenderHeader());
            
            //Render Internals
            RenderAlert(alert);

            //Document Footer
            Append(_alertTemplate.RenderFooter());

            //Return result
            return _acc.ToString(); 
        }

        /// <summary>
        /// Internal render function for alerts, original and embedded 
        /// alerts will use this function
        /// </summary>
        /// <param name="alert"></param>
        private void RenderAlert(IAlert alert) 
        {
            //Alert Header
            Append(_alertTemplate.RenderAlertHeader());

            //Route each item in Alert by type
            var maximumValueIndex = GetMaximumValueCount(alert) - 1;
            foreach (var item in alert)
            {
                //Add Alert Item
                RouteAlertItem(item, maximumValueIndex);
            }
            
            //Alert Footer
            Append(_alertTemplate.RenderAlertFooter());
        }

        private void RouteAlertItem(IAlertItem item, int maximumValueIndex)
        {
            //Route all embeded IAlerts back to the IAlert Render Method
            if (item is IAlert)
            {
                RenderAlert(item as IAlert);
                return;
            }

            //Route all AlertItems to Render IAlertItem Method
            if (item is AlertItem)
            {
                RenderAlertItem(item as AlertItem, maximumValueIndex);
                return;
            }

            //Unknown type
            throw new ArgumentException(string.Format("Alert item type [{0}] not recognized by serilazer", item.GetType().Name), "item");
        }

        private void RenderAlertItem(AlertItem g, int maximumValueIndex)
        {
            var itemStyle = g.ItemStyle;
            var maximumItemsValueIndex = g.Values.Count - 1;

            //Alert Item Header
            Append(_alertTemplate.RenderAlertItemHeader(itemStyle));
            for (var index = 0; index <= maximumItemsValueIndex; index++)
            {
                //Value Header
                Append(_alertTemplate.RenderValueHeader(itemStyle, index, maximumItemsValueIndex, maximumValueIndex));

                //Add Value
                RouteValue(g.Values[index]);

                //Value Footer
                Append(_alertTemplate.RenderValueFooter(itemStyle, index, maximumItemsValueIndex, maximumValueIndex));

            }
            //Alert Item Footer
            Append(_alertTemplate.RenderAlertItemFooter(itemStyle));
        }

        private void RouteValue(object value)
        {
            //Route all embeded IAlerts back to the IAlert Render Method
            if (value is IAlert)
            {
                RenderAlert(value as IAlert);
                return;
            }
            
            //Value is of output type, so append it to ouptut stream
            if (value is string)
            {
                Append(value as string);
                return;
            }

            //Any thing else (non-transformed objects), transform the object and re-route 
            RouteValue(_transformer.Transform(value));
        }
 
        private void Append(string text)
        {
            _acc.Append(text);
        }

        private static int GetMaximumValueCount(IEnumerable<IAlertItem> alert)
        {
            // return max number of values
            var widths = from item in alert
                         where item is AlertItem
                         select (item as AlertItem).Values.Count;

            return Math.Max(1, widths.Max());
        }

    }
}
