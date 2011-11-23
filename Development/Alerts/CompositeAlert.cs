using System.Collections.Generic;

namespace Alerts
{
    internal class CompositeAlert: List<IAlert>, IAlert{}
}

