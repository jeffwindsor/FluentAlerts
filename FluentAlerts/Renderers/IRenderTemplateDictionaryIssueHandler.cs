namespace FluentAlerts.Renderers
{
    public interface IRenderTemplateDictionaryIssueHandler
    {
        RenderTemplate TemplateNotFound(string templateName);
    }
}