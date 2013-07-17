using FluentAlerts.Settings;

namespace FluentAlerts.Renderers
{
    public interface ITemplateIssueHandler
    {
        Template TemplateNotFound(string templateName);
    }

    public class TemplateIssueHandler : ITemplateIssueHandler
    {
        private readonly IAlertBuilderFactory _alertBuilderFactory;
        private readonly IAppSettings _appSettings;

        public TemplateIssueHandler(IAppSettings appSettings, IAlertBuilderFactory alertBuilderFactory)
        {
            _appSettings = appSettings;
            _alertBuilderFactory = alertBuilderFactory;
        }

        //UNDONE: Render Template File not found - make rule based or configable
        public Template TemplateNotFound(string templateName)
        {
            //Throw Exception
            var alert = _alertBuilderFactory.Create("Render Template Not Found")
                                            .WithRow("Template Name", templateName)
                                            .WithRow("Default Template Name", _appSettings.DefaultTemplateName())
                                            .WithRow("Templates File", _appSettings.TemplateFileName())
                                            .ToAlert();

            throw new AlertException(alert);
        }
    }
}
