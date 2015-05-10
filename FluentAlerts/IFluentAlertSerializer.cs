namespace FluentAlerts
{
    public interface IFluentAlertSerializer
    {
        string Serialize(IFluentAlert alert);
    }
    public interface IFluentAlertTemplateSerializer<TSerializationTemplate> : IFluentAlertSerializer
        where TSerializationTemplate : ISerializationTemplate, new()
    {}
}