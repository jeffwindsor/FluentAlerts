using FluentAlerts.TypeInfoSelectors;

namespace FluentAlerts.Transformers
{
    //HACK: effeciency of this approach, gien the types will be tested again later when this resulting alert is add to serializer
    public class AllNonPrimitiveTransformStrategy : ITransformStrategy 
    {
        public bool IsTransformRequired(object o)
        {
            //Transformation required for anything that is not
            // a primitive, string, enum or datetime.
            var type = o.GetType();
            return !(type.IsFundamental() || type.IsEnum);
        }
    }
}
