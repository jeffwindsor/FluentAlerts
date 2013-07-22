using System.Collections.Generic;
using System.Linq;
using System;
using FluentAlerts.Transformers;

namespace FluentAlerts.Formatters
{
    public abstract class BaseValueFormatter<TResult> : IValueFormatter<TResult>
    {
        public readonly RulesCollection FormatRules = new RulesCollection {Title = "Object Format Rules"};
        public readonly RulesCollection FormatAsTitleRules = new RulesCollection {Title = "Object Format Title Rules"};

        public TResult Format(object o, MemberPath objectMemberPath)
        {
            return FormatUsingRules(o, objectMemberPath, FormatRules);
        }

        public TResult FormatAsTitle(object o, MemberPath objectMemberPath)
        {
            return FormatUsingRules(o, objectMemberPath, FormatAsTitleRules);
        }

        public abstract TResult Format(Type type);

        private static TResult FormatUsingRules(object o, MemberPath objectMemberPath,
                                                IEnumerable<FormatterRule<TResult>> rules)
        {
            var applicableRules = rules.Where(rule => rule.Apply(o, objectMemberPath));
            if (!applicableRules.Any())
                throw new FluentAlertFormattingException<TResult>("No Applicable Rules Found", o, objectMemberPath);

            return applicableRules.First().Format(o, objectMemberPath);
        }

        #region Inner Classses

        public class RulesCollection : List<FormatterRule<TResult>>
        {
            public string Title { get; set; }

            public void Add(FormatterRule<TResult>.ApplyFormattingRule apply,
                            FormatterRule<TResult>.FormattingRule format)
            {
                Add(new FormatterRule<TResult>(apply, format));
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
