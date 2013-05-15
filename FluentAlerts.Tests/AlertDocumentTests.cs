using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentAlerts;
using FluentAlerts.Extensions;
using FluentAlerts.Nodes;
using NUnit.Framework;

namespace Tests.FluentAlerts
{
    public class AlertDocumentTests:AlertBaseTest
    {
        private readonly IFluentAlertFactory _builderFactory = new FluentAlertFactory() ;
        
        [Test]
        public void CanCreateAnAlertDocument()
        {
            var builder = _builderFactory.CreateDocument();
            //Validate Builder
            Assert.IsInstanceOf<IAlertDocumentBuilder>(builder);
            
            //Validate Output
            var alert = builder.ToAlert();
            Assert.IsInstanceOf<IFluentAlert>(alert);
            
            //Validate As Document
            Assert.IsInstanceOf<CompositeFluentAlert>(alert);
            var n = alert as CompositeFluentAlert;
            Assert.IsNotNull( n);
            Assert.IsTrue(n.Count == 0);
        }

        [Test]
        public void CanCreateAnAlertDocumentWithTitle()
        {
            var builder = _builderFactory.CreateDocument("Title Test").ToAlert();
            var t = ExtractAndAssertAlertFromDocument<FluentAlertTextBlock>(builder, 0);
            Assert.IsFalse(t.IsEmpty() , "Empty");
            Assert.AreEqual("Title Test", t.Text.ToString());
        }

        [Test]
        public void CanCreateAnAlertDocumentWithSeperator()
        {
            var builder = _builderFactory.CreateDocument().AddSeperator().ToAlert();
            ExtractAndAssertAlertFromDocument<FluentAlertSeperator>(builder,0);
        }

        [Test]
        public void CanCreateAnAlertDocumentWithUrl()
        {
            var builder = _builderFactory.CreateDocument().AddURL("One", "Two").ToAlert();
            var o = ExtractAndAssertAlertFromDocument<FluentAlertUrl>(builder, 0);
            Assert.AreEqual("One", o.Text );
            Assert.AreEqual("Two", o.Url);
        }
        
        [Test]
        public void CanCreateAnAlertDocumentWithExceptionAsList()
        {
            var ex = Mother.GetNestedException();
            var alert = _builderFactory.CreateDocument()
                .AddTextBlock("Title Test")
                .AddExceptionAsList(ex)
                .ToAlert();

            //Title
            var title = ExtractAndAssertAlertFromDocument<FluentAlertTextBlock>(alert, 0);
            Assert.AreEqual(title.Text.ToString(), "Title Test");

            //Exception
            var exTable = ExtractAndAssertAlertFromDocument<FluentAlertTable>(alert, 1);
            AssertIsTableForException(exTable, ex);

            //Inner Exception
            var innerTable = ExtractAndAssertAlertFromDocument<FluentAlertTable>(alert, 2);
            AssertIsTableForException(innerTable, ex.InnerException);
        }

        [Test]
        public void CanCreateAnAlertDocumentWithExceptionTree()
        {
            var ex = Mother.GetNestedException();
            var alert = _builderFactory.CreateDocument()
                .AddTextBlock("Title Test")
                .AddExceptionAsTable(ex)
                .ToAlert();

            //Title
            var title = ExtractAndAssertAlertFromDocument<FluentAlertTextBlock>(alert, 0);
            Assert.AreEqual(title.Text.ToString(), "Title Test");

            //Exception
            var exTable = ExtractAndAssertAlertFromDocument<FluentAlertTable>(alert, 1);
            AssertIsTableForException(exTable, ex);

            //Inner Exception
            var innerTable = ExtractAndAssertValueFromTableRow<FluentAlertTable>(exTable, "InnerException");
            AssertIsTableForException(innerTable, ex.InnerException);
        }

        [Test]
        public void CanCreateAnAlertDocumentWithObjectTable()
        {
            var source = Mother.TestNode.Create();
            var builder = _builderFactory.CreateDocument()
                .AddAsTable(source)
                .ToAlert();
            var t = ExtractAndAssertAlertFromDocument<FluentAlertTable>(builder, 0);
            AssertIsTableForTestNode(t, source, 0);
        }

        [Test]
        public void CanCreateAnAlertDocumentWithObjectTables()
        {
            var source = Mother.TestNode.Create(1);
            var list = new [] { source, source.Inner };

            var builder = _builderFactory.CreateDocument()
                .AddAsTables(list)
                .ToAlert();

            //Validate list is sent to a table list in order
            for (var i = 0; i < list.Length; ++i)
            {
                AssertIsTableForTestNode(ExtractAndAssertAlertFromDocument<FluentAlertTable>(builder, i), list[i], 0);
            }
        }



        [Test]
        public void CanSerializeADocument()
        {

            var doc = _builderFactory
                .CreateDocument()
                .AddTextBlock("Header Block", TextStyle.Header1)
                .AddTextBlock("Text Block")
                .AddTextBlock("Bold Block", TextStyle.Bold)
                .AddSeperator()
                .AddURL("One","yahoo.com")
                .ToAlert();

            //Just verify it works without exception for now, asserts later
            doc.ToText();
            doc.ToHtml();
        }

        
    }
}
