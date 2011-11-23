using System;
using System.Text;
using FluentAlerts.Nodes;

namespace FluentAlerts.Serializers
{
    
    internal abstract class AlertSerializer 
    {
        /// <summary>
        /// Serializes any IAlert stack, using the derived classes Append Methods
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public virtual string Serialize(IFluentAlert n, Func<string, string> modifyResults = null)
        {
            var sb = new StringBuilder();  //new string builder used so nothing will carry forward for each message
            ConvertAndAppend(n, sb);
            var results = sb.ToString();
            return (modifyResults == null)
                ? results
                : modifyResults(results);
        }

        /// <summary>
        /// Turns IAlert into most derived type so that serialization can occur at the most detailed level
        /// </summary>
        /// <param name="n"></param>
        /// <param name="sb"></param>
        /// <remarks></remarks>
        private void ConvertAndAppend(IFluentAlert n, StringBuilder sb)
        {
            if (n == null) return;
            if (n is CompositeFluentAlert)
            {
                AppendSerialization((CompositeFluentAlert)n, sb);
            }
            else if (n is FluentAlertTable)
            {
                AppendSerialization((FluentAlertTable)n, sb);
            }
            else if (n is FluentAlertSeperator)
            {
                AppendSerialization((FluentAlertSeperator)n, sb);
            }
            else if (n is FluentAlertTextBlock)
            {
                AppendSerialization((FluentAlertTextBlock)n, sb);
            }
            else if (n is FluentAlertUrl)
            {
                AppendSerialization((FluentAlertUrl)n, sb);
            }
        }

        /// <summary>
        /// Splits Alert into its IAlert parts, then serializes each in turn
        /// </summary>
        /// <param name="source"></param>
        /// <param name="sb"></param>
        /// <remarks></remarks>
        protected virtual void AppendSerialization(CompositeFluentAlert source, StringBuilder sb)
        {
            source.ForEach(inner=>ConvertAndAppend(inner,sb ));
        }
        protected abstract void AppendSerialization(FluentAlertSeperator source, StringBuilder sb);
        protected abstract void AppendSerialization(FluentAlertTextBlock source, StringBuilder sb);
        protected abstract void AppendSerialization(FluentAlertUrl source, StringBuilder sb);
        protected abstract void AppendSerialization(FluentAlertTable source, StringBuilder sb);
        protected abstract void AppendSerialization(FluentAlertTable.Row source, int columns, StringBuilder sb);
    }
}