using System.Collections.Generic;
using System.Text;
using FluentAlerts.Serializers.Formatters;
using FluentAlerts.Transformers;

namespace FluentAlerts.Serializers
{
    public class TemplatedStringAlertSerializer : AlertSerializer<string>
    {
        //String Accumulator
        private readonly StringBuilder _acc = new StringBuilder();
        //Templates by Style
        private readonly IDictionary<AlertStyle, ISerializerTemplate<string>> _templates;
        
        public TemplatedStringAlertSerializer(
            ITransformer transformer,
            ITransformStrategy transformStrategy,
            IFormatter<string> formatter,
            IDictionary<AlertStyle, ISerializerTemplate<string>> templates)
            :base(transformer,transformStrategy,formatter)
        {
            _templates = templates;
        }

        protected virtual ISerializerTemplate<string> GetTemplate(AlertStyle style)
        {
            return _templates[style];
        }

        protected override void Add(string text)
        {
            _acc.Append(text);
        }

        protected override string GetResult()
        {
            return _acc.ToString();
        }

        protected override void BeginSerialization(AlertStyle style)
        {
           Add(GetTemplate(style).GetSerializationHeader());
        }

        protected override void EndSerialization(AlertStyle style)
        {
           Add(GetTemplate(style).GetSerializationFooter());
        }

        protected override void BeginAlert(AlertStyle style)
        {
           Add(GetTemplate(style).GetAlertHeader());
        }

        protected override void EndAlert(AlertStyle style)
        {
           Add(GetTemplate(style).GetAlertFooter());
        }

        protected override void Add(AlertTextBlock textBlock, AlertStyle style)
        {
           Add(GetTemplate(style).GetTextBlock(textBlock.Text.ToString(), textBlock.Style));
        }

        protected override void Add(AlertGroup g, AlertStyle alertStyle)
        {
            var template = GetTemplate(alertStyle);
            var groupStyle = g.Style;
            var maxValueIndex = g.Values.Length - 1;

            //Open group
           Add(template.GetGroupHeader(groupStyle));
            for (var i = 0; i <= maxValueIndex; i++)
            {
                //Open Value
               Add(template.GetValueHeader(groupStyle, i, maxValueIndex));

                //Add Value
                var value = g.Values[i];
                AddValue(value, alertStyle);

                //CloseValue
               Add(template.GetValueFooter(groupStyle, i, maxValueIndex));
                
            }
            //Close Group
           Add(template.GetGroupFooter(groupStyle));
        }
    }
}
