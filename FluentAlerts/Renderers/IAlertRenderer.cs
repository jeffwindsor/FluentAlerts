namespace FluentAlerts.Renderers
{
    public interface IAlertRenderer
    {
        string Render(IAlert alert);
    }
}