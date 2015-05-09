

using System.Diagnostics;


namespace FluentAlerts.Examples
{
    public static class TestRunnerOutputHelpers
    {
        public static void RenderToConsole(this IAlert source)
        {
            var render = Mother.CreateDefaultAlertRender();
            Trace.WriteLine(render.Render(source));
        }
        public static void RenderToConsole(this IFluentAlertsBuilder source)
        {
            RenderToConsole(source.ToAlert());
        }
    }
}
