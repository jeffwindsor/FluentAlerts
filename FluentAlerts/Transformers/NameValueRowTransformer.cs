﻿using System.Collections.Generic;
using System.Linq;
using FluentAlerts.Transformers.Strategies;
using FluentAlerts.Transformers.TypeFormatters;
using FluentAlerts.Transformers.TypeInformers;

namespace FluentAlerts.Transformers
{
    public class NameValueRowTransformer : BaseRowTransformer 
    {
        public NameValueRowTransformer(ITransformStrategy strategy,
            ITypeInformer informer, 
            ITypeFormatter<string> formatter):base(strategy,informer,formatter){}
        
        protected override IEnumerable<object[]> GetPropertyRowValues(object o, IEnumerable<string> objectMemberPath)
        {
            return from t in GetPropertyInfoValues(o, objectMemberPath)
                   orderby t.Info.Name
                   select new[] { t.Info.Name, t.Value };
        }

        protected override IEnumerable<object[]> GetFieldRowValues(object o, IEnumerable<string> objectMemberPath)
        {
            return from t in GetFieldInfoValues(o, objectMemberPath)
                   orderby t.Info.Name
                   select new[] { t.Info.Name, t.Value };
        }
    }
}