using System;

namespace FluentAlerts
{
    public enum ArrayStyle
    {
        Emphasized,
        Normal
    }
    
    public enum TextStyle
    {
        HeaderOne,
        Emphasized,
        Normal
    }

    public interface IAlertItem { }
    public abstract class AlertItem: IAlertItem 
    {
        protected ItemStyle ItemStyle { get; set; }
        protected object[] Values { get; set; }
    }

    public class ValueItem : AlertItem
    {
        public ValueItem(object value)
        {
            ItemStyle = ItemStyle.Value;
            Values = new [] {value};
        }
    }

    public class ArrayItem : AlertItem
    {
        public ArrayItem(ArrayStyle style, object[] values)
        {
            Style = style;
            Values = values;
        }

        public ArrayStyle Style {
            get { return ItemStyle.ToArrayStyle(); }
            set { ItemStyle = value.ToItemStyle(); }
        }
    
    }

    public class SeperatorItem : AlertItem
    {
        public SeperatorItem()
        {
            ItemStyle = ItemStyle.Seperator;
            Values = new object[] {};
        }
    }

    public class TextItem : AlertItem
    {
        public TextItem(TextStyle style, string text)
        {
            Style = style;
            Values = new object[] {text};
        }

        public string Text {
            get { return (string) Values[0]; }
            set { Values[0] = value; }
        }

        public TextStyle Style
        {
            get { return ItemStyle.ToTextStyle(); }
            set { ItemStyle = value.ToItemStyle(); }
        }

    }

    public class UrlItem : AlertItem
    {
        public UrlItem(string url, string text)
        {
            ItemStyle = ItemStyle.Url;
            Values = new object[] {url, text};
        }

        public string Url
        {
            get { return (string)Values[0]; }
            set { Values[0] = value; }
        }

        public string Text
        {
            get { return (string) Values[1]; }
            set { Values[1] = value; }
        }
    }
}
