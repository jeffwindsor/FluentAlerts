using System;
using System.Linq;
using System.Collections.Generic;

namespace FluentAlerts.Transformers
{
    public interface ITransformStrategy
    {
        bool IsTransformRequired(object o, int depth);
    }

    /// <summary>
    /// Allows for transformation depth by type, Inherit from this class and add type-depth pairs
    /// </summary>
    public abstract class TransformStrategy : ITransformStrategy
    {
        protected delegate bool TransformationAtDepthRule(Type type, int currentDepth);
        protected ICollection<TransformationAtDepthRule> Rules { get; private set; }

        protected TransformStrategy()
        {
            Rules = new List<TransformationAtDepthRule>();
        }

        public virtual bool IsTransformRequired(object o, int depth)
        {
            var type = o.GetType();
            return Rules.Any(rule => rule(type, depth));
        }
    }
}