using System.Text;
using FluentAlerts.Serializers.Templates;
using FluentAlerts.Transformers;
using FluentAlerts.Transformers.Formatters;
using FluentAlerts.Transformers.Strategies;

namespace FluentAlerts.Serializers
{
    public class TemplatedStringAlertSerializer : AlertSerializer<string>
        {
        //String Accumulator
        private readonly StringBuilder _acc = new StringBuilder();
        private readonly ISerializerTemplate<string> _template;
        
        public TemplatedStringAlertSerializer(ITransformer<string> transformer,
                    ISerializerTemplate<string> template)
            :base(transformer)
        {
            _template = template;
        }

        protected override bool IsResultType(object value)
        {
            return (value as string) != null;
        }

        protected override void Add(string text)
        {
            _acc.Append(text);
        }

        protected override string GetResult()
        {
            return _acc.ToString();
        }

        protected override void BeginSerialization()
        {
           Add(_template.GetSerializationHeader());
        }

        protected override void EndSerialization()
        {
           Add(_template.GetSerializationFooter());
        }

        protected override void BeginAlert()
        {
           Add(_template.GetAlertHeader());
        }

        protected override void EndAlert()
        {
           Add(_template.GetAlertFooter());
        }

        protected override void Add(AlertTextBlock textBlock )
        {
           Add(_template.GetTextBlock(textBlock.Text.ToString(), textBlock.Style));
        }

        protected override void Add(AlertGroup g)
        {
            var groupStyle = g.Style;
            var maxValueIndex = g.Values.Length - 1;

            //Open group
           Add(_template.GetGroupHeader(groupStyle));
            for (var i = 0; i <= maxValueIndex; i++)
            {
                //Open Value
               Add(_template.GetValueHeader(groupStyle, i, maxValueIndex));

                //Add Value
                var value = g.Values[i];
                RouteGroupValue(value);

                //CloseValue
               Add(_template.GetValueFooter(groupStyle, i, maxValueIndex));
                
            }
            //Close Group
           Add(_template.GetGroupFooter(groupStyle));
        }
    }
}
