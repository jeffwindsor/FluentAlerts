namespace FluentAlerts.Transformers
{
    public interface ITransformer
    {
        IAlert Transform(object o, ITransformStrategy transformStrategy, int currentDepth);
    }
}