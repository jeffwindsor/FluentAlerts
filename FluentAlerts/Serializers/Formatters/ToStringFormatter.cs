namespace FluentAlerts.Serializers.Formatters
{
    public class ToStringFormatter:IFormatter<string>
    {
        public string Format(object o)
        {
            //TODO how to make tyoe specific? like date formats, enum formats?
            return o.ToString();
        }
    }
}
