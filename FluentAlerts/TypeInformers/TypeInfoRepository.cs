using System;
using System.Collections.Generic;
using System.Linq;

namespace FluentAlerts.TypeInfoSelectors
{
    public interface ITypeInfoRepository
    {
        TypeInfo Find(Type forType);
    }

    public class TypeInfoRepository: ITypeInfoRepository 
    {
        private readonly IDictionary<Type,TypeInfo> _typeInfosByType = new Dictionary<Type, TypeInfo>();
        private readonly ITypeInfoSelector[] _selectors;

        public TypeInfoRepository(params ITypeInfoSelector[] selectors)
        { 
            _selectors = selectors;
        }

        public TypeInfo Find(Type forType)
        {
            //If selected type infos are not in dictionary, create and add it
            if (!_typeInfosByType.ContainsKey(forType))
            {
                //accumulating results of all selectors in order
                _typeInfosByType[forType] = _selectors.Aggregate(new TypeInfo(),
                                                                 (acc, selector) => selector.Select(acc, forType));
            }
            //Return cached results
            return _typeInfosByType[forType];
        }

    }
}
