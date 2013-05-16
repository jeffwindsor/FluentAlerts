using System;
using System.Linq;
using System.Reflection;

namespace FluentAlerts.TypeInfoSelectors
{

    //default 
    public class AllPublicPropertiesAndFieldsSelector : ITypeInfoSelector
    {
        public TypeInfo Select(TypeInfo info, Type source)
        {
            //All public readable properties
            info.PropertyInfos = from pi in source.GetProperties()
                                    where pi.CanRead
                                    select pi;

            //All public fields
            info.FieldInfos = source.GetFields();

            //No Methods
            info.MethodInfos = Enumerable.Empty<MethodInfo>();

            //No Event Infos
            info.EventInfos = Enumerable.Empty<EventInfo>();

            return info;
        }


    }
}
