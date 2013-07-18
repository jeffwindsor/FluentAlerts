namespace FluentAlerts.Renderers
{
    public interface ITemplateIssueHandler
    {
        Template TemplateNotFound(string templateName);
    }

    public class TemplateIssueHandler : ITemplateIssueHandler
    {
        private readonly IAlertBuilderFactory _alertBuilderFactory;
        private readonly IFluentAlertSettings _fluentAlertSettings;

        public TemplateIssueHandler(IFluentAlertSettings fluentAlertSettings, IAlertBuilderFactory alertBuilderFactory)
        {
            _fluentAlertSettings = fluentAlertSettings;
            _alertBuilderFactory = alertBuilderFactory;
        }

        //UNDONE: Render Template File not found - make rule based or configable
        public Template TemplateNotFound(string templateName)
        {
            //Throw Exception
            var alert = _alertBuilderFactory.Create("Render Template Not Found")
                                            .WithRow("Template Name", templateName)
                                            .WithRow("Default Template Name", _fluentAlertSettings.DefaultTemplateName())
                                            .WithRow("Templates File", _fluentAlertSettings.TemplateFileName())
                                            .ToAlert();

            throw new AlertException(alert);
        }
    }
}
