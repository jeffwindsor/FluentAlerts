namespace FluentAlerts.Renderers
{
    public class RenderTemplateDictionaryIssueHandler : IRenderTemplateDictionaryIssueHandler
    {
        private readonly IFluentAlerts _alerts;
        
        public RenderTemplateDictionaryIssueHandler( IFluentAlerts alerts)
        {
            _alerts = alerts;
        }

        //UNDONE: Render Template File not found - make rule based or configable
        public RenderTemplate TemplateNotFound(string templateName)
        {
            //Throw Exception
            var alert = _alerts.Create("Render Template Not Found")
                                            .With("Template Name", templateName)
                                            .ToAlert();

            throw new FluentAlertException(alert);
        }
    }
}
