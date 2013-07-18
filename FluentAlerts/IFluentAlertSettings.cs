namespace FluentAlerts
{
    public interface IFluentAlertSettings
    {
        string DefaultTemplateDictionary();
        string DefaultTemplateName();
        string TemplateFileName();
        char MemberPathSeperator();
    }
}