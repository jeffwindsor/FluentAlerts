using System.Text;

namespace Alerts
{
    internal abstract class AlertSerializer 
    {
        /// <summary>
        /// Serializes any IAlert stack, using the derived classes Append Methods
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public virtual string Serialize(IAlert n)
        {
            var result = new StringBuilder();  //new string builder used so nothing will carry forward for each message
            ConvertAndAppend(n, result);
            return result.ToString();
        }

        /// <summary>
        /// Turns IAlert into most derived type so that serialization can occur at the most detailed level
        /// </summary>
        /// <param name="n"></param>
        /// <param name="sb"></param>
        /// <remarks></remarks>
        private void ConvertAndAppend(IAlert n, StringBuilder sb)
        {
            if (n == null) return;
            if (n is CompositeAlert)
            {
                AppendSerialization((CompositeAlert)n, sb);
            }
            else if (n is AlertTable)
            {
                AppendSerialization((AlertTable)n, sb);
            }
            else if (n is AlertSeperator)
            {
                AppendSerialization((AlertSeperator)n, sb);
            }
            else if (n is AlertTextBlock)
            {
                AppendSerialization((AlertTextBlock)n, sb);
            }
            else if (n is AlertURL)
            {
                AppendSerialization((AlertURL)n, sb);
            }
        }

        /// <summary>
        /// Splits Alert into its IAlert parts, then serializes each in turn
        /// </summary>
        /// <param name="source"></param>
        /// <param name="sb"></param>
        /// <remarks></remarks>
        protected virtual void AppendSerialization(CompositeAlert source, StringBuilder sb)
        {
            source.ForEach(inner=>ConvertAndAppend(inner,sb ));
        }
        protected abstract void AppendSerialization(AlertSeperator source, StringBuilder sb);
        protected abstract void AppendSerialization(AlertTextBlock source, StringBuilder sb);
        protected abstract void AppendSerialization(AlertURL source, StringBuilder sb);
        protected abstract void AppendSerialization(AlertTable source, StringBuilder sb);
        protected abstract void AppendSerialization(AlertTable.Row source, int columns, StringBuilder sb);
    }
}