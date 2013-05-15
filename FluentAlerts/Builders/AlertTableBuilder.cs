using FluentAlerts.Extensions;

namespace FluentAlerts
{
    public interface IAlertTableBuilder : IAlertBuilder
    {
        /// <summary>
        /// Adds a table row
        /// </summary>
        IAlertTableBuilder WithRow(params object[] items);
        IAlertTableBuilder WithRow(TableRowStyle style, params object[] items);

        /// <summary>
        /// Adds a spanning header row
        /// </summary>
        IAlertTableBuilder WithSpanningRow(object item);
        IAlertTableBuilder WithSpanningRow(TableSpanningRowStyle style, object item);

    }

    /// <summary>
    /// Wrapper for exposing table build only
    /// </summary>
    public class AlertTableBuilder : BaseAlertBuilder, IAlertTableBuilder
    {
        public AlertTableBuilder(IAlertFactory nf)
            : base(nf){}

        public IAlertTableBuilder WithRow(params object[] items)
        {
            return WithRow(TableRowStyle.Normal, items);
        }

        public IAlertTableBuilder WithRow(TableRowStyle style, params object[] items)
        {
            AddGroup(style.ToGroupStyle(), items);
            return this;
        }

        public IAlertTableBuilder WithSpanningRow(object item)
        {
            return WithSpanningRow(TableSpanningRowStyle.Header, item);
        }

        public IAlertTableBuilder WithSpanningRow(TableSpanningRowStyle style, object item)
        {
            AddGroup(style.ToGroupStyle(), new [] { item });
            return this;
        }
    }

}
