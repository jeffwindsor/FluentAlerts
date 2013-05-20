using System.Collections.Generic;
using System.Linq;

namespace FluentAlerts.Transformers.Formatters
{
    //TODO: make object o, IEnumerable<string> objectMemberPath into RuleRequest, and maybe derived classes
    public interface IObjectFormatter<out TResult>
    {
        TResult Format(object o, IEnumerable<string> objectMemberPath);
    }

    public abstract class BaseObjectFormatter<TResult>: IObjectFormatter<TResult>
    {
        protected readonly RulesCollection Rules = new RulesCollection();

        public TResult Format(object o, IEnumerable<string> objectMemberPath)
        {
            var applicableRules = Rules.Where(rule => rule.Apply(o, objectMemberPath));
            if (!applicableRules.Any())
                throw new FluentAlertFormattingException<TResult>("No Applicable Rules Found", o, objectMemberPath, Rules);
            
            return applicableRules.First().Format(o,objectMemberPath);

        }

        protected class RulesCollection : List<FormatterRule<TResult>>
        {
            public void Add(FormatterRule<TResult>.ApplyFormattingRule apply,
                FormatterRule<TResult>.FormattingRule format)
            {
                Add( new FormatterRule<TResult>(apply, format));
            }
        }
    }
}
