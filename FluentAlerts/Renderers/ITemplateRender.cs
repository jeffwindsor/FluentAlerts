

namespace FluentAlerts.Renderers
{
    public interface ITemplateRender
    {
        string RenderHeader();
        string RenderFooter();

        string RenderAlertHeader();
        string RenderAlertFooter();

        string RenderAlertItemHeader(AlertItemStyle style);
        string RenderAlertItemFooter(AlertItemStyle style);

        string RenderValueHeader(AlertItemStyle style, int index, int maximumItemsValueIndex, int maximumValueCount);
        string RenderValueFooter(AlertItemStyle style, int index, int maximumItemsValueIndex, int maximumValueCount);

        string Scrub(string text);
    }
}