
using System;

namespace FluentAlerts.TypeInfoSelectors
{
    public interface ITypeInfoSelector
    {
        TypeInfo Select(TypeInfo info, Type source);
    }
}
