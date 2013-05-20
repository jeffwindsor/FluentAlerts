using System.Collections.Generic;

namespace FluentAlerts.Transformers.TypeFormatters
{
    public interface ITypeFormatter<out TResult>
    {
        TResult Format(object o, IEnumerable<string> objectMemberPath);
    }
    public class BaseTypeFormatter<TResult>: ITypeFormatter<TResult>
    {

        protected delegate bool F

        public TResult Format(object o, IEnumerable<string> objectMemberPath)
        {
            
        }
    }
}
