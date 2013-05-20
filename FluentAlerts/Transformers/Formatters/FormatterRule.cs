using System.Collections.Generic;

namespace FluentAlerts.Transformers.Formatters
{
    public class FormatterRule<TResult>
    {
        public delegate bool ApplyFormattingRule(object o, IEnumerable<string> objectMemberPath);
        public delegate TResult FormattingRule(object o, IEnumerable<string> objectMemberPath);
 
        public FormatterRule(ApplyFormattingRule apply, FormattingRule format)
        {
            Apply = apply;
            Format = format;
        }

        public ApplyFormattingRule Apply { get; private set; }
        public FormattingRule Format { get; private set; }
    }
}
