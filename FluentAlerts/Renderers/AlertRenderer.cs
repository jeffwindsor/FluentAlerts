using System;
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
            foreach (var item in alert)
            {
                Route(item);
            }
            
            //Alert End
            Append(_template.GetAlertFooter());
        }
        
        private void Render(AlertGroup g)
        {
            var groupStyle = g.Style;
            var maxValueIndex = g.Values.Length - 1;

            //Group Begin
            Append(_template.GetGroupHeader(groupStyle));
            for (var i = 0; i <= maxValueIndex; i++)
            {
                //Value Begin
                Append(_template.GetValueHeader(groupStyle, i, maxValueIndex));

                //Add Value
                var value = g.Values[i];
                Route(value);

                //Value End
                Append(_template.GetValueFooter(groupStyle, i, maxValueIndex));

            }
            //Group End
            Append(_template.GetGroupFooter(groupStyle));
        }

        private void Render(AlertTextBlock textBlock)
        {
            //Text Block Begin
            Append(_template.GetTextBlockHeader(textBlock.Style));
            //Append Text Block Text
            Append(textBlock.ToString());
            //Text Block End
            Append(_template.GetTextBlockFooter(textBlock.Style));
        }

        private void Route(IAlertItem item)
        {
            //Embedded Alerts
            var itemAsAlert = item as IAlert;
            if (itemAsAlert != null)
            {
                Render(itemAsAlert);
                return;
            }

            //Text Block Items
            var itemAsTextBlock = item as AlertTextBlock;
            if (itemAsTextBlock != null)
            {
                Render(itemAsTextBlock);
                return;
            }

            //Group Items
            var itemAsGroup = item as AlertGroup;
            if (itemAsGroup != null)
            {
                Render(itemAsGroup);
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
    }
}
