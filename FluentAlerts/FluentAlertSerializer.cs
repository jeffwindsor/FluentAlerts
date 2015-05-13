using System.Text;

namespace FluentAlerts
{
    public class FluentAlertSerializer<TTemplate> : 
        IFluentAlertSerializer where TTemplate : FluentAlertSerializerTemplate, new()
    {
        private readonly TTemplate _template;
        public FluentAlertSerializer():this(new TTemplate()){}
        public FluentAlertSerializer(TTemplate template)
        {
            _template = template;
        }

        /// <summary>
        /// Serialize the given object using serialization bindings and rules
        /// </summary>
        /// <param name="source">object to be serialized</param>
        /// <returns>string result of serialization bindings and rules</returns>
        public string Serialize(object source)
        {
            var result = new StringBuilder();
            _template.PreSerializationHook(result);
            _template.Serialize(source, result);
            _template.PostSerializationHook(result);
            return result.ToString();
        }
    }
}

// TODO: Serialization Stop If Rules
//protected delegate bool SerializationRule(object o, SerializationPath path);
//private readonly List<SerializationRule> _stopSerializationRules = new List<SerializationRule>();
//private bool IsSerializationStopped(object source)
//{
//    return _stopSerializationRules.Any(rule => rule(source, path));
//}
//internal class SerializationPath
//{
//    private const char Seperator = '.';
//    public static readonly SerializationPath Empty = new SerializationPath(Enumerable.Empty<string>());

//    private readonly string[] _SerializationPath;

//    public SerializationPath(string SerializationPath) : this(SerializationPath.Split(Seperator)) { }
//    public SerializationPath(IEnumerable<string> SerializationPath) { _SerializationPath = SerializationPath.ToArray(); }

//    public int Length
//    {
//        get { return _SerializationPath.Length; }
//    }

//    public SerializationPath Extend(string extension)
//    {
//        return new SerializationPath(_SerializationPath.Concat(new[] { extension }));
//    }

//    public override string ToString()
//    {
//        return string.Join(Seperator.ToString(), _SerializationPath);
//    }
//}