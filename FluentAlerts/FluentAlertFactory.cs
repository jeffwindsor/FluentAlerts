using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FluentAlerts
{
    public interface IFluentAlertFactory
    {
        IFluentAlertBuilder Create();
        IFluentAlertBuilder Create(string title);
        IFluentAlertTableBuilder CreateTable();
        IFluentAlertTableBuilder CreateTable(string title);
    }

    public class FluentAlertFactory: IFluentAlertFactory
    {
        public IFluentAlertBuilder Create()
        {
            return new FluentAlertBuilder();
        }

        public IFluentAlertBuilder Create(string title)
        {
            return new FluentAlertBuilder().AddTextBlock(title, TextStyle.Header1);
        }

        public IFluentAlertTableBuilder CreateTable()
        {
            return new FluentAlertBuilder().AddTable();
        }

        public IFluentAlertTableBuilder CreateTable(string title)
        {
            return new FluentAlertBuilder()
                .AddTable()
                .AddHeaderRow(title);
        }
    }
}
