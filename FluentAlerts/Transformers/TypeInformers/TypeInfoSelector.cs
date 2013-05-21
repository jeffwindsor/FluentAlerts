using System.Collections.Generic;
using System.Linq;

namespace FluentAlerts.Transformers.TypeInformers
{
    public interface ITypeInfoSelector
    {
        TypeInfo Find(object o, IEnumerable<string> objectMemberPath);
    }

    public abstract class BaseTypeInfoSelector: ITypeInfoSelector
    {
        public delegate void TypeInformerRule(TypeInfo info, object o, IEnumerable<string> objectMemberPath);
        public readonly RulesCollection Rules = new RulesCollection();

        public TypeInfo Find(object o, IEnumerable<string> objectMemberPath)
        {
            //Accumulate results using Accumulator Acceptable Wrapper for rules
            return Rules.Aggregate(new TypeInfo(),
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
 