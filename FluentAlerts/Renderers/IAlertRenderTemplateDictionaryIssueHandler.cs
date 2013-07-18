namespace FluentAlerts.Renderers
{
    public interface IAlertRenderTemplateDictionaryIssueHandler
    {
        AlertRenderTemplate TemplateNotFound(string templateName);
    }
}