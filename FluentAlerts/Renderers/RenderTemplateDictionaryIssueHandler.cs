namespace FluentAlerts.Renderers
{
    public class RenderTemplateDictionaryIssueHandler : IRenderTemplateDictionaryIssueHandler
    {
        private readonly IAlertBuilderFactory _alertBuilderFactory;
        
        public RenderTemplateDictionaryIssueHandler( IAlertBuilderFactory alertBuilderFactory)
        {
            _alertBuilderFactory = alertBuilderFactory;
        }

        //UNDONE: Render Template File not found - make rule based or configable
        public RenderTemplate TemplateNotFound(string templateName)
        {
            //Throw Exception
            var alert = _alertBuilderFactory.Create("Render Template Not Found")
                                            .With("Template Name", templateName)
                                            .ToAlert();

            throw new AlertException(alert);
        }
    }
}
