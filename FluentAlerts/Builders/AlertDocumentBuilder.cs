 using System.Collections.Generic;
using System.Linq;

namespace FluentAlerts
{
    public interface IAlertDocumentBuilder : IAlertBuilder
    {
        /// <summary>
        /// Appends text to first item if it is a text block, otherwise it inserts a text block at
        /// the first position
        /// </summary>

        void AppendTitleWith(string text);
        IAlertDocumentBuilder WithTitleOf(string text);
        IAlertDocumentBuilder WithTitleOf(string format, params object[] args);
        //TODO : WIth footer of 
        IAlertDocumentBuilder WithSeperator();
        IAlertDocumentBuilder WithTextBlock(string text);
        IAlertDocumentBuilder WithTextBlock(TextStyle style, string text);
        IAlertDocumentBuilder WithTextBlock(string format, params object[] args);
        IAlertDocumentBuilder WithTextBlock(TextStyle style, string format, params object[] args);
        IAlertDocumentBuilder WithUrl(string text, string url);
        IAlertDocumentBuilder WithValue(object value);
        IAlertDocumentBuilder WithValues(IEnumerable<object> values);
        IAlertDocumentBuilder WithAlert(IAlert n);

    }

    /// <summary>
    /// Wrapper for exposing doc build only
    /// </summary>
    public class AlertDocumentBuilder : BaseAlertBuilder, IAlertDocumentBuilder
    {
        public AlertDocumentBuilder(IAlertFactory nf)
            : base(nf){}

        /// <summary>
        /// Appends text to first item if it is a text block, otherwise it inserts a text block at
        /// the first position
        /// </summary>
        public void AppendTitleWith(string text)
        {
            if (!string.IsNullOrWhiteSpace(text))
            {
                GetCreateTitle().Text.Append(text);
            }
        }

        public IAlertDocumentBuilder WithTitleOf(string text)
        {
            if (!string.IsNullOrWhiteSpace(text))
            {
                var title = GetCreateTitle().Text;
                //Clear and Append Text
                title.Length = 0;
                title.Append(text);
            }
            return this;
        }

        public IAlertDocumentBuilder WithTitleOf(string format, params object[] args)
        {
            return WithTitleOf(string.Format(format, args));
        }

        public IAlertDocumentBuilder WithSeperator()
        {
            AddGroup(GroupStyle.Seperator, string.Empty);
            return this;
        } 

        public IAlertDocumentBuilder WithUrl(string text, string url)
        {
            AddGroup(GroupStyle.Url, text, url);
            return this;
        }

        public IAlertDocumentBuilder WithValue(object value)
        {
            AddGroup(GroupStyle.Value, value);
            return this;
        }

        public IAlertDocumentBuilder WithValues(IEnumerable<object> values)
        {
            if (values != null) {
                foreach (var v in values) {
                    WithValue(v);
                }
            }
            return this;
        }

        public IAlertDocumentBuilder WithAlert(IAlert n)
        {
            AddNotificationItems(n);
            return this;
        }

        public IAlertDocumentBuilder WithTextBlock(TextStyle style, string text)
        {
            AddText(text, style);
            return this;
        }

        public IAlertDocumentBuilder WithTextBlock(TextStyle style, string format, params object[] args)
        {
            return WithTextBlock(style, string.Format(format, args));
        }

        public IAlertDocumentBuilder WithTextBlock(string text)
        {
            return WithTextBlock(TextStyle.Normal, text);
        }

        public IAlertDocumentBuilder WithTextBlock(string format, params object[] args)
        {
            return WithTextBlock(TextStyle.Normal, format, args);
        }
        
        /// <summary>
        /// Title is defined in a document as a text block of style title in the first item position
        /// </summary>
        /// <returns>Current or new inserted Title</returns>
        private AlertTextBlock GetCreateTitle()
        {
            if (HasTitle())
            {
                return (AlertTextBlock)NotificationItems[0];
            }
            return CreateTitle();
        }
        
        private bool HasTitle()
        {
            return (NotificationItems.Any() && IsTitle(NotificationItems[0]));
        }

        private AlertTextBlock CreateTitle()
        {
            //Othewise insert
            var result = new AlertTextBlock { Style = TextStyle.Title };
            NotificationItems.Insert(0, result);
            return result;
        }

        /// <summary>
        /// Return true if object is a text block with a style of Title
        /// </summary>
        private static bool IsTitle(object o)
        {
            var block = o as AlertTextBlock;
            if (block != null )
            {
                return block.Style == TextStyle.Title;
            }
            return false;
        }
    }
}
