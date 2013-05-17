namespace FluentAlerts.Formatters
{
    public class ToStringFormatter:IFormatter<string>
    {
        public string Format(object o)
        {
            return o.ToString();
        }
    }
}
