
using System;

namespace FluentAlerts.TypeInformers
{
    public interface ITypeInfoSelector
    {
        TypeInfo Select(TypeInfo info, Type source);
    }
}
