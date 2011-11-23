using System;
using System.Linq;
using FluentAlerts;
using FluentAlerts.Nodes;
using NUnit.Framework;

namespace Tests.FluentAlerts
{
    public class AlertBaseTest 
    {
        protected readonly IAlertBuilderFactory BuilderFactory = new AlertBuilderFactory();

        [TestFixtureSetUp] public void AttributedTestFixtureSetup() { TestFixtureSetUp(); }
        [SetUp] public void AttributedTestSetup(){TestSetUp();}
        [TearDown] public void AttributedTestTearDown() { TestTearDown(); }
        [TestFixtureTearDown] public void AttributedTestFixtureTearDown() { TestFixtureTearDown();}

        protected virtual void TestFixtureSetUp(){}
        protected virtual void TestSetUp(){}
        protected virtual void TestTearDown(){}
        protected virtual void TestFixtureTearDown(){}
        
        protected static TOut ExtractAndAssertAlertFromDocument<TOut>(IFluentAlert alert, int index) where TOut : class, IFluentAlert
        {
            var ca = alert as CompositeFluentAlert;
            Assert.IsNotNull(ca, "Not Composite Alert");
            var result = ca[index] as TOut;
            Assert.IsNotNull(result, "Not Expected Type: " + typeof(TOut).Name);
            return result;
        }
        
        protected static TOut ExtractAndAssertValueFromTableRow<TOut>(IFluentAlert alert, string title, int valueIndex = 1) where TOut: class
        {
            return ExtractAndAssertValueFromTableRow<TOut>(alert, valueIndex,
                r => r.Values[0].ToString() == title);
        }
        
        protected static TOut ExtractAndAssertValueFromTableRow<TOut>(IFluentAlert alert, string title, RowStyle style, int valueIndex = 1) where TOut : class
        {
            return ExtractAndAssertValueFromTableRow<TOut>(alert, valueIndex,
                r => r.Values[0].ToString() == title
                     && r.Style == style);
        }
        
        protected static string ExtractAndAssertTitleFromTableRow(IFluentAlert alert)
        {
            return ExtractAndAssertValueFromTableRow<string>(alert, 0, 
                r => r.Style == RowStyle.Header
                     && r.Values.Length == 1);
        }

        private static TOut ExtractAndAssertValueFromTableRow<TOut>(IFluentAlert alert, int valueIndex, Func<FluentAlertTable.Row, bool> rowSelector) where TOut : class
        {
            var table = alert as FluentAlertTable;
            Assert.IsNotNull(table, "Not Alert Table");
            var row = table.Rows.FirstOrDefault(rowSelector);
            Assert.IsNotNull(row, "Row Not Found ");
            var result = row.Values[valueIndex] as TOut;
            Assert.IsNotNull(result, "Not Expected Type: " + typeof(TOut).Name);
            return result;
        }


        protected static void AssertIsTableForException(IFluentAlert alert, Exception ex)
        {
            //Validate Title, Message, Stack Trace
            Assert.AreEqual(ex.GetType().Name, ExtractAndAssertTitleFromTableRow(alert), "Incorrect Title");
            Assert.AreEqual(ex.Message,ExtractAndAssertValueFromTableRow<string>(alert, "Message", RowStyle.Normal), "Incorrect Message");
            Assert.AreEqual(ex.StackTrace, ExtractAndAssertValueFromTableRow<string>(alert, "StackTrace", RowStyle.Normal),"Incorrect Stack Trace");
        }       
        protected static void AssertIsTableForTestNode(IFluentAlert alert, Mother.TestNode source, int toDepth)
        {
            //Validate Title and Properties
            Assert.AreEqual(source.GetType().Name, ExtractAndAssertTitleFromTableRow(alert), "Incorrect Title");
            Assert.AreEqual(source.One, ExtractAndAssertValueFromTableRow<string>(alert, "One", RowStyle.Normal), "Incorrect One");
            Assert.AreEqual(source.Two.ToString(), ExtractAndAssertValueFromTableRow<object>(alert, "Two", RowStyle.Normal).ToString(), "Incorrect Two");
            
            //If depth to go, validate inner 
            if(toDepth == 0) return;
            AssertIsTableForTestNode(ExtractAndAssertValueFromTableRow<FluentAlertTable>(alert, "Inner"), source.Inner, --toDepth);
        }
    }
}
