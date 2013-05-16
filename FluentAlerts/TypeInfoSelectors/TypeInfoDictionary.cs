using System;
using System.Collections.Generic;
using System.Linq;

namespace FluentAlerts.TypeInfoSelectors
{
    public class TypeInfoDictionary
    {
        private readonly IDictionary<Type,TypeInfo> _typeInfosByType = new Dictionary<Type, TypeInfo>();
        private readonly ITypeInfoSelector[] _selectors;

        public TypeInfoDictionary(params ITypeInfoSelector[] selectors)
        { 
            _selectors = selectors;
        }

        public TypeInfo GetTypeInfo(Type forType)
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
