namespace FluentAlerts.Transformers
{
    public interface ITransformStrategy
    {
        bool IsTransformRequired(object o);
    }
}