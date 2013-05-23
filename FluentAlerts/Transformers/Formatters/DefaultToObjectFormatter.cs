namespace FluentAlerts.Transformers.Formatters
{
    /// <summary>
    /// Produces a Title of the Object's Type Name
    /// or a return the object unformatted
    /// turns nulls into "Null" string
    /// </summary>
    /// <remarks>Useful when you want to leave the formatting of types not transformed to some later process like the razor engine</remarks>
    public class DefaultToObjectFormatter : BaseObjectFormatter<object>
    {
        public DefaultToObjectFormatter()
        {
            //Base rule: all objects return tostring()
            FormatRules.Add((o,path) => true,
                      (o, path) => o ?? "Null");

            //Base rule: all objects return title of type tostring()
            FormatAsTitleRules.Add((o, path) => true,
                      (o, path) => (o == null) ? "Null" : o.GetType().Name);
        }

    }
}
