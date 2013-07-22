namespace FluentAlerts.Renderers
{
    public interface ITemplateRender
    {
        string RenderHeader();
        string RenderFooter();

        string RenderAlertHeader();
        string RenderAlertFooter();

        string RenderAlertItemHeader(ItemStyle style);
        string RenderAlertItemFooter(ItemStyle style);

        string RenderValueHeader(ItemStyle style, int index, int maximumItemsValueIndex, int maximumValueCount);
        string RenderValueFooter(ItemStyle style, int index, int maximumItemsValueIndex, int maximumValueCount);

        string Scrub(string text);
    }
}