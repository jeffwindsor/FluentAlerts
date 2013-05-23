using System;
using System.Collections.Generic;

namespace FluentAlerts.Transformers.Formatters
{
    public class FluentAlertFormattingException<TResult>: ApplicationException
    {
        public FluentAlertFormattingException(object o, IEnumerable<string> objectMemberPath)
        {
            SetValues(o, objectMemberPath);
        }
        public FluentAlertFormattingException(string message, object o, IEnumerable<string> objectMemberPath)
            :base(message)
        {
            SetValues(o, objectMemberPath);
        }
        public FluentAlertFormattingException(string message, object o, IEnumerable<string> objectMemberPath, Exception inner)
            : base(message, inner)
        {
            SetValues(o, objectMemberPath);
        }

        private void SetValues(object o, IEnumerable<string> objectMemberPath)
        {
            FormatObject = o;
            FormatObjectMemberPath = objectMemberPath;
        }


        public object FormatObject { get; private set; }
        public IEnumerable<string> FormatObjectMemberPath { get; private set; }
    }
}
