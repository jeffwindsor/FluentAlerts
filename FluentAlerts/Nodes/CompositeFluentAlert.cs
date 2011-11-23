using System.Collections.Generic;

namespace FluentAlerts.Nodes
{
    internal class CompositeFluentAlert: List<IFluentAlert>, IFluentAlert{}
}

