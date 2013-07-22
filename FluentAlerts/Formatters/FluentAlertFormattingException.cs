using System;
using System.Collections.Generic;
using FluentAlerts.Transformers;

namespace FluentAlerts.Formatters
{
    public class FluentAlertFormattingException<TResult>: ApplicationException
    {
        public FluentAlertFormattingException(object o, MemberPath objectMemberPath)
        {
            SetValues(o, objectMemberPath);
        }
        public FluentAlertFormattingException(string message, object o, MemberPath objectMemberPath)
            :base(message)
        {
            SetValues(o, objectMemberPath);
        }
        public FluentAlertFormattingException(string message, object o, MemberPath objectMemberPath, Exception inner)
            : base(message, inner)
        {
            SetValues(o, objectMemberPath);
        }

        private void SetValues(object o, MemberPath objectMemberPath)
        {
            FormatObject = o;
            FormatObjectMemberPath = objectMemberPath;
        }


        public object FormatObject { get; private set; }
        public MemberPath FormatObjectMemberPath { get; private set; }
    }
}
