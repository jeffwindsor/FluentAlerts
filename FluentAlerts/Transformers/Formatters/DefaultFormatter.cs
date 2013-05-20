namespace FluentAlerts.Transformers.Formatters
{
    public class DefaultFormatter : BaseObjectFormatter<string>
    {
        //TODO: ASK if the order makes since for extension.  first rule that matches/appears gets used
        //TODO: ASK constructor is not forced so you may get no rules, should this 
        public DefaultFormatter()
        {
            //Base rule: all objects return tostring()
            Rules.Add((o,path) => true,
                      (o, path) => (o == null) ? "Null" : o.ToString());
        }

    }
}
