namespace FluentAlerts.Formatters
{
    public interface IFormatter<out TResult>
    {
        TResult Format(object o);
    }
}
