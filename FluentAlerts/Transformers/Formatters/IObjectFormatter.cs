using System.Collections.Generic;
using System.Linq;

namespace FluentAlerts.Transformers.Formatters
{
    public interface IObjectFormatter<out TResult>
    {
        TResult FormatAsTitle(object o, IEnumerable<string> objectMemberPath);
        TResult Format(object o, IEnumerable<string> objectMemberPath);
    }

    public abstract class BaseObjectFormatter<TResult>: IObjectFormatter<TResult>
    {
        public readonly RulesCollection FormatRules = new RulesCollection {Title="Object Format Rules"};
        public readonly RulesCollection FormatAsTitleRules = new RulesCollection { Title = "Object Format Title Rules" };
        
        #region IObjectFormatter
        public TResult Format(object o, IEnumerable<string> objectMemberPath)
        {
            return FormatUsingRules(o, objectMemberPath, FormatRules);
        }

        public TResult FormatAsTitle(object o, IEnumerable<string> objectMemberPath)
        {
            return FormatUsingRules(o, objectMemberPath, FormatAsTitleRules);
        }

        private static TResult FormatUsingRules(object o, IEnumerable<string> objectMemberPath, RulesCollection rules)
        {
            var applicableRules = rules.Where(rule => rule.Apply(o, objectMemberPath));
            if (!applicableRules.Any())
                throw new FluentAlertFormattingException<TResult>("No Applicable Rules Found", o, objectMemberPath, rules);

            return applicableRules.First().Format(o, objectMemberPath);
        }
        #endregion

        #region Inner Classses
        public class RulesCollection : List<FormatterRule<TResult>>
        {
            public string Title { get; set; }
            public void Add(FormatterRule<TResult>.ApplyFormattingRule apply,
                FormatterRule<TResult>.FormattingRule format)
            {
                Add( new FormatterRule<TResult>(apply, format));
            }

            public void InsertFirst(FormatterRule<TResult>.ApplyFormattingRule apply,
                FormatterRule<TResult>.FormattingRule format)
            {
                Insert(0, new FormatterRule<TResult>(apply, format));
            }

            public void Insert(int index, FormatterRule<TResult>.ApplyFormattingRule apply,
                FormatterRule<TResult>.FormattingRule format)
            {
                Insert(index, new FormatterRule<TResult>(apply, format));
            }
        }
        #endregion
    }
}
