using System.Collections.Generic;
using FluentAlerts.Domain;

namespace FluentAlerts.Builders
{
    public class FluentTextBlockBuilder : IAlertable
    {
        private readonly List<Text> _textItems;
        private readonly uint _headerLevel;
        public FluentTextBlockBuilder(uint headerLevel = 0)
        {
            _textItems = new List<Text>();
            _headerLevel = headerLevel;
        }

        public FluentTextBlockBuilder WithText(string text)
        {
            return With(new Text { Content = text });
        }

        public FluentTextBlockBuilder WithItalic(string text)
        {
            return With(new Italic{Content= text});
        }

        public FluentTextBlockBuilder WithUnderscore(string text)
        {
            return With(new Underscore{Content= text});
        }

        public FluentTextBlockBuilder WithStrong(string text)
        {
            return With(new Strong{Content= text});
        }

        public FluentTextBlockBuilder WithNewLine()
        {
            return With(new NewLine());
        }

        private FluentTextBlockBuilder With<T>(T item) where T: Text
        {
            _textItems.Add(item);
            return this;
        }

        public TextBlock ToTextBox()
        {
            switch (_headerLevel)
            {
                case 0:
                    return new TextBlock(_textItems);
                default:
                    return new HeaderTextBlock(_textItems, _headerLevel);
            }
        }

        public object ToAlert()
        {
            return ToTextBox();
        }
    }
}