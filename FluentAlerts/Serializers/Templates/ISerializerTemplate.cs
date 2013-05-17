using System;

namespace FluentAlerts.Serializers
{
    public interface ISerializerTemplate<TResult>
    {
        TResult GetSerializationHeader();
        TResult GetAlertHeader();

        TResult GetTextBlock(string text, TextStyle style);

        TResult GetGroupHeader(GroupStyle style);
        TResult GetValueHeader(GroupStyle style, int index, int indexMax);

        TResult GetValueFooter(GroupStyle style, int index, int indexMax);
        TResult GetGroupFooter(GroupStyle style);

        TResult GetAlertFooter();
        TResult GetSerializationFooter();
    }

}
