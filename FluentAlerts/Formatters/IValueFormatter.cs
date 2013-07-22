using System;
using FluentAlerts.Transformers;

namespace FluentAlerts.Formatters
{
    public interface IValueFormatter<out TResult>
    {
        TResult FormatAsTitle(object o, MemberPath objectMemberPath);
        TResult Format(object o, MemberPath objectMemberPath);
        TResult Format(Type type);
    }
}