using System;
using System.Linq;
using System.Text;
using FluentAlerts.Transformers;

namespace FluentAlerts.Renderers
{
    public class AlertRenderer : IAlertRenderer
    {
        private readonly StringBuilder _acc = new StringBuilder();
        private readonly IAlertTemplateRender _alertTemplate;
        private readonly ITransformer<string> _transformer;
        public AlertRenderer(ITransformer<string> transformer, IAlertTemplateRender alertTemplate)
        {
            _transformer = transformer;
            _alertTemplate = alertTemplate;
        }

        public string RenderAlert(IAlert alert)
        {
            if (alert == null) return string.Empty;

            //Begin Serialization
            Append(_alertTemplate.GetSerializationHeader());
            
            //Render Internals
            Render(alert);

            //End Serialization
            Append(_alertTemplate.GetSerializationFooter());

            //Return result
            return _acc.ToString(); 
        }

        private void Render(IAlert alert) 
        {
            //Alert Begin
            Append(_alertTemplate.GetAlertHeader());

            //Route each item in Alert by type
            var alertWidth = GetWidth(alert);
            foreach (var item in alert)
            {
                Route(item, alertWidth);
            }
            
            //Alert End
            Append(_alertTemplate.GetAlertFooter());
        }

        private void Render(AlertItem g,int alertWidth)
        {
            var groupStyle = g.ItemStyle;
            var groupLength = g.Values.Count;

            //Group Begin
            Append(_alertTemplate.GetItemHeader(groupStyle, alertWidth));
            for (var index = 0; index < groupLength; index++)
            {
                //Value Begin
                Append(_alertTemplate.GetValueHeader(groupStyle, index, groupLength, alertWidth));

                //Add Value
                var value = g.Values[index];
                Route(value);

                //Value End
                Append(_alertTemplate.GetValueFooter(groupStyle, index, groupLength, alertWidth));

            }
            //Group End
            Append(_alertTemplate.GetItemFooter(groupStyle, alertWidth));
        }

        private void Route(IAlertItem item, int alertWidth)
        {
            //Embedded Alerts
            var itemAsAlert = item as IAlert;
            if (itemAsAlert != null)
            {
                Render(itemAsAlert);
                return;
            }

            //Group Items
            var itemAsGroup = item as AlertItem;
            if (itemAsGroup != null)
            {
                Render(itemAsGroup, alertWidth);
                return;
            }

            throw new ArgumentException("Alert item type not recognized by serilazer", "item");
        }

        private void Route(object value)
        {
            //No multimethods, so route by type
            var alert = value as IAlert;
            if (alert != null)
            {
                Render(alert);
                return;
            }

            var formatted = value as string;
            if (formatted != null)
            {
                Append((string)value);
                return;
            }

            //Transform the object and re-route 
            Route(_transformer.Transform(value));
        }
 
        private void Append(string text)
        {
            _acc.Append(text);
        }

        private static int GetWidth(IAlert alert)
        {
            // return max length of group values
            var widths = from item in alert
                         where item is AlertItem
                         select (item as AlertItem).Values.Count;

            return Math.Max(1, widths.Max());
        }

    }
}
