using System.Collections.Generic;
using System.Linq;
using FluentAlerts.Transformers;

namespace FluentAlerts.TypeInformers
{
    public interface ITypeInformerSelector
    {
        TypeInformer Find(object o, MemberPath objectMemberPath);
    }

    public abstract class BaseTypeInformerSelector: ITypeInformerSelector
    {
        public delegate void TypeInformerRule(TypeInformer info, object o, MemberPath objectMemberPath);
        public readonly RulesCollection Rules = new RulesCollection();

        public TypeInformer Find(object o, MemberPath objectMemberPath)
        {
            //Accumulate results using Accumulator Acceptable Wrapper for rules
            return Rules.Aggregate(new TypeInformer(),
                                   (acc, rule) =>
                                       {
                                           rule(acc, o, objectMemberPath);
                                           return acc;
                                       });
        }

        #region Inner Classses
        public class RulesCollection : List<TypeInformerRule>
        {
            public void InsertFirst(TypeInformerRule rule)
            {
                Insert(0, rule);
            }
         }
        #endregion
    }
}
 