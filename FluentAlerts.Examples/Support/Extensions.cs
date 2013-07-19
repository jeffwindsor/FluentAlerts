

using System.Diagnostics;

namespace FluentAlerts.Examples
{
    public static class TestRunnerOutputHelpers
    {
        public static void RenderToConsole(this IAlert source)
        {
            var render = ObjectFactory.CreateDefaultAlertRender();
            Trace.WriteLine(render.Render(source));
        }
    }
}
