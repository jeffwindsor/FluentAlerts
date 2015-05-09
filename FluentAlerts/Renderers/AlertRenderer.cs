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
        private readonly ITemplateRender _templateRenderer;
        private readonly ITransformer _transformer;
        public AlertRenderer(ITransformer transformer, ITemplateRender templateRenderer)
        {
            _transformer = transformer;
            _templateRenderer = templateRenderer;
        }

        /// <summary>
        /// Serializes the complete alert tree, any non transformed bojects
        /// will be transformed then formatted for output
        /// </summary>
        public string Render(IAlert alert)
        {
            if (alert == null) return string.Empty;

            //Document Header
            Append(_templateRenderer.RenderHeader());
            
            //Render Internals
            RenderAlert(alert);

            //Document Footer
            Append(_templateRenderer.RenderFooter());

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
            Append(_templateRenderer.RenderAlertHeader());

            //Route each item in Alert by type
            var maximumValueIndex = GetMaximumValueCount(alert) - 1;
            foreach (var item in alert)
            {
                //Add Alert Item
                RouteAlertItem(item, maximumValueIndex);
            }
            
            //Alert Footer
            Append(_templateRenderer.RenderAlertFooter());
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
            Append(_templateRenderer.RenderAlertItemHeader(itemStyle));
            for (var index = 0; index <= maximumItemsValueIndex; index++)
            {
                //Value Header
                Append(_templateRenderer.RenderValueHeader(itemStyle, index, maximumItemsValueIndex, maximumValueIndex));

                //Add Value
                RouteValue(g.Values[index]);

                //Value Footer
                Append(_templateRenderer.RenderValueFooter(itemStyle, index, maximumItemsValueIndex, maximumValueIndex));

            }
            //Alert Item Footer
            Append(_templateRenderer.RenderAlertItemFooter(itemStyle));
        }

        private void RouteValue(object value)
        {
            //Route all embedded IAlertBuilders (Mistake but hard to see in traces, so covering here for free) back to the IAlert Render Method
            if (value is IFluentAlertsBuilder)
            {
                var alert = (value as IFluentAlertsBuilder).ToAlert();
                RenderAlert(alert);
                return;
            }

            //Route all embedded IAlerts back to the IAlert Render Method
            if (value is IAlert)
            {
                RenderAlert(value as IAlert);
                return;
            }
            
            //Value is of output type, so append it to output stream
            if (value is string)
            {
                Append(_templateRenderer.Scrub(value as string));
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

        private static int GetMaximumValueCount(IEnumerable<IAlertItem> alert)
        {
            // return max number of values
            var widths = (from item in alert
                         where item is AlertItem
                         select (item as AlertItem).Values.Count).ToList();

            return widths.Any()
                       ? Math.Max(1, widths.Max())
                       : 1;
        }

    }
}
