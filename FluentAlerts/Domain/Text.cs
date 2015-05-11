namespace FluentAlerts.Domain
{
    public class Text
    {
        public string Content { get; set; }
    }

    public class Italic : Text { }
    public class Underscore : Text { }
    public class Strong : Text { }
    public class NewLine : Text { }
}