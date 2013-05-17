using System.Linq;

namespace FluentAlerts.Transformers.TypeInformers
{
    public class DefaultTypeInformer : TypeInformer 
    {
        public DefaultTypeInformer()
        {
            //All public readable properties
            Rules.Add((info, type) =>
                { 
                    info.PropertyInfos = from pi in type.GetProperties()
                                         where pi.CanRead
                                         select pi;
                });

            //All public fields
            Rules.Add((info, type) =>
            {
                info.FieldInfos = type.GetFields();
            });

        }
    }
}
