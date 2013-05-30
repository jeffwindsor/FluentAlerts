using System;
using System.Linq;
using System.Text;
using FluentAlerts.Transformers;

namespace FluentAlerts.Renderers
{
    public interface IAlertRenderer
    {
        string RenderAlert(IAlert alert);
    }

    public class AlertRenderer : IAlertRenderer
    {
        private readonly StringBuilder _acc = new StringBuilder();
        private readonly ITemplateRender _template;
        private readonly ITransformer<string> _transformer;
        public AlertRenderer(ITransformer<string> transformer, ITemplateRender template)
        {
            _transformer = transformer;
            _template = template;
        }

        public string RenderAlert(IAlert alert)
        {
            if (alert == null) return string.Empty;

            //Begin Serialization
            Append(_template.GetSerializationHeader());
            
            //Render Internals
            Render(alert);

            //End Serialization
            Append(_template.GetSerializationFooter());

            //Return result
            return _acc.ToString(); 
        }

        private void Render(IAlert alert) 
        {
            //Alert Begin
            Append(_template.GetAlertHeader());

            //Route each item in Alert by type
            var alertWidth = GetWidth(alert);
            foreach (var item in alert)
            {
                Route(item, alertWidth);
            }
            
            //Alert End
            Append(_template.GetAlertFooter());
        }

        private void Render(AlertItem g,int alertWidth)
        {
            var groupStyle = g.ItemStyle;
            var groupLength = g.Values.Length;

            //Group Begin
            Append(_template.GetItemHeader(groupStyle, alertWidth));
            for (var index = 0; index < groupLength; index++)
            {
                //Value Begin
                Append(_template.GetValueHeader(groupStyle, index, groupLength, alertWidth));

                //Add Value
                var value = g.Values[index];
                Route(value);

                //Value End
                Append(_template.GetValueFooter(groupStyle, index, groupLength, alertWidth));

            }
            //Group End
            Append(_template.GetItemFooter(groupStyle, alertWidth));
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
                         select (item as AlertItem).Values.Length;

            return Math.Max(1, widths.Max());
        }

    }
}
