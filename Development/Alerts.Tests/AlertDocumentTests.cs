using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alerts;
using NUnit.Framework;

namespace Tests.Alerts
{
    public class AlertDocumentTests:AlertBaseTest
    {
        private readonly IAlertBuilderFactory _builderFactory = new AlertBuilderFactory();
        
        [Test]
        public void CanCreateAnAlertDocument()
        {
            var builder = _builderFactory.CreateDocument();
            //Validate Builder
            Assert.IsInstanceOf<IAlertDocumentBuilder>(builder);
            
            //Validate Output
            var alert = builder.ToAlert();
            Assert.IsInstanceOf<IAlert>(alert);
            
            //Validate As Document
            Assert.IsInstanceOf<CompositeAlert>(alert);
            var n = alert as CompositeAlert;
            Assert.IsNotNull( n);
            Assert.IsTrue(n.Count == 0);
        }

        [Test]
        public void CanCreateAnAlertDocumentWithTitle()
        {
            var builder = _builderFactory.CreateDocument("Title Test").ToAlert();
            var t = ExtractAndAssertAlertFromDocument<AlertTextBlock>(builder, 0);
            Assert.IsFalse(t.IsEmpty() , "Empty");
            Assert.AreEqual("Title Test", t.Text.ToString());
        }

        [Test]
        public void CanCreateAnAlertDocumentWithSeperator()
        {
            var builder = _builderFactory.CreateDocument().AddSeperator().ToAlert();
            ExtractAndAssertAlertFromDocument<AlertSeperator>(builder,0);
        }

        [Test]
        public void CanCreateAnAlertDocumentWithUrl()
        {
            var builder = _builderFactory.CreateDocument().AddURL("One", "Two").ToAlert();
            var o = ExtractAndAssertAlertFromDocument<AlertURL>(builder, 0);
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
            var title = ExtractAndAssertAlertFromDocument<AlertTextBlock>(alert, 0);
            Assert.AreEqual(title.Text.ToString(), "Title Test");

            //Exception
            var exTable = ExtractAndAssertAlertFromDocument<AlertTable>(alert, 1);
            AssertIsTableForException(exTable, ex);

            //Inner Exception
            var innerTable = ExtractAndAssertAlertFromDocument<AlertTable>(alert, 2);
            AssertIsTableForException(innerTable, ex.InnerException);
        }

        [Test]
        public void CanCreateAnAlertDocumentWithExceptionTree()
        {
            var ex = Mother.GetNestedException();
            var alert = _builderFactory.CreateDocument()
                .AddTextBlock("Title Test")
                .AddExceptionAsTable(ex,true)
                .ToAlert();

            //Title
            var title = ExtractAndAssertAlertFromDocument<AlertTextBlock>(alert, 0);
            Assert.AreEqual(title.Text.ToString(), "Title Test");

            //Exception
            var exTable = ExtractAndAssertAlertFromDocument<AlertTable>(alert, 1);
            AssertIsTableForException(exTable, ex);

            //Inner Exception
            var innerTable = ExtractAndAssertValueFromTableRow<AlertTable>(exTable, "InnerException");
            AssertIsTableForException(innerTable, ex.InnerException);
        }

        [Test]
        public void CanCreateAnAlertDocumentWithObjectTable()
        {
            var source = Mother.TestNode.Create();
            var builder = _builderFactory.CreateDocument()
                .AddAsTable(source)
                .ToAlert();
            var t = ExtractAndAssertAlertFromDocument<AlertTable>(builder, 0);
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
                AssertIsTableForTestNode(ExtractAndAssertAlertFromDocument<AlertTable>(builder, i), list[i], 0);
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
