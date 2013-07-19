using System.Linq;
using System.Collections.Generic;

namespace FluentAlerts.Transformers
{
    public interface ITransformStrategy
    {
        bool IsTransformRequired(object o, IEnumerable<string> objectMemberPath);
    }

    /// <summary>
    /// Allows for transformation depth by type, Inherit from this class and add type-depth pairs
    /// </summary>
    public abstract class TransformStrategy : ITransformStrategy
    {
        /// <summary>
        /// Returns true if the system should transform and object at a given depth
        /// </summary>
        protected delegate bool TransformationRequiredRule(object o, IEnumerable<string> objectMemberPath);
        
        /// <summary>
        /// List of rules, which return true if transformation is required for the given parameters
        /// </summary>
        protected readonly ICollection<TransformationRequiredRule> TransformationRequiredRules = new List<TransformationRequiredRule>();

        public virtual bool IsTransformRequired(object o, IEnumerable<string> objectMemberPath)
        {
            return TransformationRequiredRules.Any(rule => rule(o, objectMemberPath));
        }
    }
}