namespace FluentAlerts
{
    public interface IFluentAlertSettings
    {
        string DefaultTemplateName();
        string TemplateFileName();
        char MemberPathSeperator();
    }
}