using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FluentAlerts.Serializers
{
    public class TemplatedStringAlertSerializer : AlertSerializer<string>
    {
        private readonly StringBuilder _acc = new StringBuilder();
        private readonly IDictionary<AlertStyle, ISerializerTemplate<string>> _templates;


        public TemplatedStringAlertSerializer(IDictionary<AlertStyle, ISerializerTemplate<string>> templates)
        {
            _templates = templates;
        }

        protected virtual ISerializerTemplate<string> GetTemplate(AlertStyle style)
        {
            return _templates[style];
        }

        protected override string GetResult()
        {
            return _acc.ToString();
        }

        protected override void BeginSerialization(AlertStyle style)
        {
            _acc.Append(GetTemplate(style).GetSerializationHeader());
        }

        protected override void EndSerialization(AlertStyle style)
        {
            _acc.Append(GetTemplate(style).GetSerializationFooter());
        }

        protected override void BeginAlert(AlertStyle style)
        {
            _acc.Append(GetTemplate(style).GetAlertHeader());
        }

        protected override void EndAlert(AlertStyle style)
        {
            _acc.Append(GetTemplate(style).GetAlertFooter());
        }

        protected override void Add(AlertTextBlock textBlock, AlertStyle style)
        {
            _acc.Append(GetTemplate(style).GetTextBlock(textBlock.Text.ToString(), textBlock.Style));
        }

        protected override void Add(AlertGroup g, AlertStyle alertStyle)
        {
            var template = GetTemplate(alertStyle);
            var groupStyle = g.Style;
            var maxValueIndex = g.Values.Length - 1;

            //Open group
            _acc.Append(template.GetGroupHeader(groupStyle));
            for (var i = 0; i <= maxValueIndex; i++)
            {
                //Open Value
                _acc.Append(template.GetValueHeader(groupStyle, i, maxValueIndex));

                //Add Value
                var value = g.Values[i];
                if (value is IAlert)
                {
                    //Embedded Alert in Group, send to base for routing
                    Add((IAlert)value);
                }
                else if (value is IAlertItem)
                {
                    //Embedded Alert Item in Group, send to base for routing
                    Add((IAlertItem)value, alertStyle);
                }
                else
                {
                    //TODO: Serialize object
                    //Add(TransformObjectToAlert(value))

                }

                //CloseValue
                _acc.Append(template.GetValueFooter(groupStyle, i, maxValueIndex));
                
            }
            //Close Group
            _acc.Append(template.GetGroupFooter(groupStyle));
        }
    }
}
