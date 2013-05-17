namespace FluentAlerts.Transformers
{
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
