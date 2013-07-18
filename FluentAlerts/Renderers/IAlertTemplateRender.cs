namespace FluentAlerts.Renderers
{
    public interface IAlertTemplateRender
    {
        string GetSerializationHeader();
        string GetSerializationFooter();

        string GetAlertHeader();
        string GetAlertFooter();

        string GetItemHeader(ItemStyle style, int alertWidth);
        string GetItemFooter(ItemStyle style, int alertWidth);

        string GetValueHeader(ItemStyle style, int index, int groupLength, int alertWidth);
        string GetValueFooter(ItemStyle style, int index, int groupLength, int alertWidth);
    }
}