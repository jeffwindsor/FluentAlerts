namespace FluentAlerts.Renderers
{
    public interface IRenderTemplate
    {
        string GetSerializationHeader();
        string GetAlertHeader();

        string GetTextBlockHeader(TextStyle style);
        string GetTextBlockFooter(TextStyle style);

        string GetGroupHeader(GroupStyle style);
        string GetValueHeader(GroupStyle style, int index, int indexMax);

        string GetValueFooter(GroupStyle style, int index, int indexMax);
        string GetGroupFooter(GroupStyle style);

        string GetAlertFooter();
        string GetSerializationFooter();
    }

}
