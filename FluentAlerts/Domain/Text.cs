namespace FluentAlerts.Domain
{
    public class Text
    {
        public string Content { get; set; }
    }

    public class Italic : Text { }
    public class Underscore : Text { }
    public class Bold : Text { }
    public class StrikeThrough : Text { }
    public class NewLine : Text { }
}