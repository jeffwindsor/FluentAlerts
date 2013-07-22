using System.Collections.Generic;
using FluentAlerts.Transformers;

namespace FluentAlerts.Formatters
{
    public class FormatterRule<TResult>
    {
        public delegate bool ApplyFormattingRule(object o, MemberPath objectMemberPath);
        public delegate TResult FormattingRule(object o, MemberPath objectMemberPath);
 
        public FormatterRule(ApplyFormattingRule apply, FormattingRule format)
        {
            Apply = apply;
            Format = format;
        }

        public ApplyFormattingRule Apply { get; private set; }
        public FormattingRule Format { get; private set; }
    }
}
