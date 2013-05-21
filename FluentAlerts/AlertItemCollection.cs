using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FluentAlerts
{
    public class AlertItemCollection : List<IAlertItem>
    {
        public void AddAlertItems(IEnumerable<IAlertItem> items)
        {
            if (items == null) return;
            foreach (var item in items)
            {
                AddAlertItem(item);
            }
        }

        public void AddAlertItem(IAlertItem item)
        {
            if (item != null)
            {
                this.Add(item);
            }
        }

        public void AddGroup(GroupStyle style, params object[] items)
        {
            AddAlertItem(new AlertGroup { Style = style, Values = items });
        }

        public void AddText(string text, TextStyle style = TextStyle.Normal)
        {
            if (!string.IsNullOrEmpty(text))
            {
                AddAlertItem(new AlertTextBlock(text) { Style = style });
            }
        }

        /// <summary>
        /// Title is defined in a document as a text block of style title in the first item position
        /// </summary>
        /// <returns>Current or new inserted Title</returns>
        public AlertTextBlock GetCreateTitle()
        {
            if (HasTitle())
            {
                return this[0] as AlertTextBlock;
            }
            return CreateTitle();
        }

        private bool HasTitle()
        {
            return (this.Any() && IsTitle(this[0]));
        }

        private AlertTextBlock CreateTitle()
        {
            //Othewise insert
            var result = new AlertTextBlock { Style = TextStyle.Header };
            this.Insert(0, result);
            return result;
        }

        /// <summary>
        /// Return true if object is a text block with a style of Title
        /// </summary>
        private static bool IsTitle(object o)
        {
            var block = o as AlertTextBlock;
            if (block != null)
            {
                return block.Style == TextStyle.Header;
            }
            return false;
        }

    }
}
