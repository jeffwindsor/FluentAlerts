using System;
using System.Collections.Generic;

namespace FluentAlerts.Transformers.Formatters
{
    public class FluentAlertFormattingException<TResult>: ApplicationException
    {
        public FluentAlertFormattingException(object o, IEnumerable<string> objectMemberPath, ICollection<FormatterRule<TResult>> rules)
        {
            SetValues(o, objectMemberPath, rules);
        }
        public FluentAlertFormattingException(string message, object o, IEnumerable<string> objectMemberPath, ICollection<FormatterRule<TResult>> rules)
            :base(message)
        {
            SetValues(o, objectMemberPath, rules);
        }
        public FluentAlertFormattingException(string message, object o, IEnumerable<string> objectMemberPath, ICollection<FormatterRule<TResult>> rules, Exception inner)
            : base(message, inner)
        {
            SetValues(o, objectMemberPath, rules);
        }

        private void SetValues(object o, IEnumerable<string> objectMemberPath, ICollection<FormatterRule<TResult>> rules)
        {
            FormatObject = o;
            FormatObjectMemberPath = objectMemberPath;
            FormatterRules = rules;
        }


        public object FormatObject { get; private set; }
        public IEnumerable<string> FormatObjectMemberPath { get; private set; }
        public ICollection<FormatterRule<TResult>> FormatterRules { get; private set; }
    }
}
