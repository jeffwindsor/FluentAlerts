using System;
using System.Collections.Generic;
using System.Linq;

namespace FluentAlerts.Transformers.TypeInformers
{
    public interface ITypeInfomer
    {
        TypeInfo Find(Type forType);
    }

    public abstract class TypeInformer: ITypeInfomer
    {
        protected delegate void InfoSelectionRule(TypeInfo info, Type source);
        private readonly IDictionary<Type,TypeInfo> _typeInfosByType = new Dictionary<Type, TypeInfo>();
        protected readonly ICollection<InfoSelectionRule> Rules = new List<InfoSelectionRule>();
        
        public TypeInfo Find(Type forType) 
        {
            //If selected type infos are not in dictionary, create and add it
            if (!_typeInfosByType.ContainsKey(forType))
            {
                //accumulating results of all selectors in order
                _typeInfosByType[forType] = Rules.Aggregate(new TypeInfo(),
                                                            (acc, rule) =>
                                                                {
                                                                    rule(acc, forType);
                                                                    return acc;
                                                                });
            } 
            //Return cached results
            return _typeInfosByType[forType];
        }
    }
}
 