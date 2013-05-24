using System.Linq;

namespace FluentAlerts.Transformers.TypeInformers
{
    public class DefaultTypeInfoSelector : BaseTypeInfoSelector 
    {
        public DefaultTypeInfoSelector()
        {
            //All public readable properties
            Rules.Add( (info, obj, path) =>
                { 
                    info.PropertyInfos = from pi in obj.GetType().GetProperties()
                                         where pi.CanRead
                                         select pi;
                });

            //All public fields
            Rules.Add((info, obj, path) =>
                {
                    info.FieldInfos = obj.GetType().GetFields();
                });

        }
    }
}
