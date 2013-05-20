//TODO: split out interfaces into files
//TODO: helper/extension methods methods for serialization of exception and alert?
//TODO: add full comments and documentation (see using code comments with code examples)
//TODO: ASK make (object o, IEnumerable<string> objectMemberPath) into RuleRequest, and maybe derived classes for format, strat and transform?
//HACK: use of static Alerts class internally poses IOC and extendability issues...
namespace FluentAlerts
{
    public interface IAlertItem { }
}
