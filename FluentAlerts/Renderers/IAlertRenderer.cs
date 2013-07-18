namespace FluentAlerts.Renderers
{
    public interface IAlertRenderer
    {
        string RenderAlert(IAlert alert);
    }
}