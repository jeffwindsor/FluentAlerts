namespace FluentAlerts.Serializers.Formatters
{
    public interface IFormatter<out TResult>
    {
        //TODO: benefit to making this generic??
        TResult Format(object o);
    }
}
