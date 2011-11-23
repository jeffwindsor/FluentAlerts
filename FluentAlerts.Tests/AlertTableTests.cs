using System;
using System.Diagnostics;
using System.Linq;
using FluentAlerts;
using FluentAlerts.Extensions;
using FluentAlerts.Nodes;
using NUnit.Framework;

namespace Tests.FluentAlerts
{
    public class AlertTableTests : AlertBaseTest 
    {
        [Test] 
        public void CanCreateAnAlertTable()
        {
            var builder = BuilderFactory
                .CreateTable("Title")
                .WithNumberOfColumns(2)
                .AddRow("Row One", 1)
                .AddHighlightedRow("Highlight", 2)
                .AddHeaderRow("Header", 3)
                .AddFooterRow("Footer", 4);
            Assert.IsInstanceOf<IAlertTableBuilder>(builder);

            var alert = builder.ToAlert();
            Assert.IsInstanceOf<IFluentAlert>(alert);
            Assert.IsInstanceOf<FluentAlertTable>(alert);


            var n = alert as FluentAlertTable;
            Assert.IsNotNull(n);
            Assert.AreEqual(5, n.Rows.Count());
            
            Assert.IsTrue(n.Rows.Any(r => r.Style == RowStyle.Header
                && r.Values.Length == 1
                && r.Values[0].ToString() == "Title"
                ));

            Assert.IsTrue( n.Rows.Any( 
                r => r.Style== RowStyle.Highlighted
                && r.Values.Length == 2
                && r.Values[0].ToString() == "Highlight"
                && (int)r.Values[1] == 2
                ));

            Assert.IsTrue(n.Rows.Any(r => r.Style == RowStyle.Header
                && r.Values.Length == 2
                && r.Values[0].ToString() == "Header"
                && (int)r.Values[1] == 3
                ));

            Assert.IsTrue(n.Rows.Any(r => r.Style == RowStyle. Footer
                && r.Values.Length == 2
                && r.Values[0].ToString() == "Footer"
                && (int)r.Values[1] == 4
                ));

            Assert.IsTrue(n.Rows.Any(r => r.Style == RowStyle.Normal
                && r.Values.Length == 2
                && r.Values[0].ToString() == "Row One"
                && (int)r.Values[1] == 1
                ));
        }
        
        [Test]
        public void CanCreateAlertTableWithReflectedValues()
        {
            const int toDepth = 0;
            var source = Mother.TestNode.Create(toDepth);
            var alert = BuilderFactory.CreateTable(source, toDepth).ToAlert();
            AssertIsTableForTestNode(alert, source, toDepth);
        }

        [Test]
        public void CanCreateAlertTableWithReflectedValuesToDepth()
        {
            //Create a class with a toDepth 1 lower than serialize toDepth to test it stopped
            const int toDepth = 1;
            var source = Mother.TestNode.Create(toDepth+1);
            var alert = BuilderFactory.CreateTable(source, toDepth).ToAlert();
            AssertIsTableForTestNode(alert, source, toDepth);
        }

        [Test]
        public void CanCreateAlertTableOfExceptionTree()
        {
            var ex = Mother.GetNestedException();
            var alert = BuilderFactory.CreateTable(ex).ToAlert();

            //Exception
            AssertIsTableForException(alert, ex);

            //Inner Exception
            var innerTable = ExtractAndAssertValueFromTableRow<FluentAlertTable>(alert, "InnerException");
            AssertIsTableForException(innerTable, ex.InnerException);
        }
        
        [Test]
        public void CanSerializeATable()
        {
            var table = BuilderFactory.CreateTable()
                .WithNumberOfColumns(3)
                .AddHeaderRow("Header")
                .AddHighlightedRow("Top1", "Top2", "Top3")
                .AddRow("InnerTable", 1,
                        BuilderFactory.CreateTable(Mother.TestNode.Create(3), 3).ToAlert())
                .AddRow("InnerComposite", 2,
                        BuilderFactory.CreateDocument("Title").AddAsTable(Mother.TestNode.Create(3), 3).ToAlert())
                .AddFooterRow("Footer")
                .ToAlert();

            //Just verify it works without exception for now, asserts later
            table.ToText();
            table.ToHtml();
        }
    }
}
