using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using FluentAlerts;
using FluentAlerts.Extensions;
using FluentAlerts.Nodes;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace Specs.FluentAlerts.StepDefinitions
{
    [Binding]
    public class Steps
    {
        private IFluentAlertFactory _factory;
        private IFluentAlertBuilder _builder;
        private IFluentAlertTableBuilder _tableBuilder;
        private CompositeFluentAlert _compositeAlert;
        private string _text;
        private string _url;
        private Exception _exception;
        private FluentAlertException _alertException;
        private IFluentAlert _alert;
        private Exception _innerException;
        private string _textMessage;
        private FluentAlertTable _alertTable;
        private TestObject _object;
        private int _depth;



        [Given(@"I have an alert factory")]
        public void GivenIHaveAnAlertFactory()
        {
            _factory = new FluentAlertFactory();
        }

        [Given(@"I have a document builder")]
        public void GivenIHaveADocumentBuilder()
        {
            _builder = (new FluentAlertFactory()).Create();
        }
        [Given(@"I have some text")]
        public void GivenIHaveSomeText()
        {
            _text = "asdgashgasgvqwtcqwertcqwrtc qwrt";
        }
        [Given(@"I have some url")]
        public void GivenIHaveSomeUrl()
        {
            _url = "http://afasgawegwq.sdfvas";
        }
        [Given(@"I have an exception")]
        public void GivenIHaveAnException()
        {
            _exception = Mother.GetNestedException(2);
        }
        [Given(@"I have a list of  objects")]
        public void GivenIHaveAListOfObjects()
        {
            ScenarioContext.Current.Pending();
        }
        [Given(@"a non-empty builder")]
        public void GivenANonEmptyBuilder()
        {
            _builder = _factory.Create("Test Title")
                .AddSeperator()
                .AddTextBlock("Test Text");
        }
        [Given(@"an inner exception")]
        public void GivenAnInnerException()
        {
            _innerException = Mother.GetNestedException(1);
        }
        [Given(@"an alert")]
        public void GivenAnAlert()
        {
            if (_builder == null) GivenANonEmptyBuilder();
            _alert = _builder.ToAlert();
        }
        [Given(@"a text message")]
        public void GivenATextMessage()
        {
            _textMessage = "sdghasfhastvqwvtqw erytewqrv qwry";
        }
        [Given(@"I create a table with a title '(.*)'")]
        public void GivenICreateATableWithATitle(string title)
        {
            _tableBuilder = _factory.CreateTable(title);

        }
        [Given(@"I have a table builder")]
        public void GivenIHaveATableBuilder()
        {
            _tableBuilder = _factory.CreateTable();
        }
        public RowStyle GetRowStyle(string style)
        {
            RowStyle rowStyle;
            RowStyle.TryParse(style, true, out rowStyle);
            return rowStyle;
        }
        [Given(@"I create a table with an object using depth (\d+)")]
        public void GivenICreateATableWithAnObject(int depth)
        {
            _depth = depth;
            _object = TestObject.Create(_depth + 1);  //Will show the reflector stopped before bottom
            _tableBuilder = _factory.Create().AddObjectAsTable(_object, _depth);
        }
        [Given(@"I create a table with an exception")]
        public void GivenICreateATableWithAnException()
        {
            _depth = 3;
            _exception = Mother.GetNestedException(_depth);
            _tableBuilder = _factory.Create().AddExceptionAsTable(_exception);
        }




        [When(@"I add the '(.*)' rows")]
        public void WhenIAddTheFollowingRows(string style, Table table)
        {
            var rowStyle = GetRowStyle(style);
            Func<object[], IFluentAlertTableBuilder> add;
            switch (rowStyle)
            {
                case RowStyle.Header:
                    add = _tableBuilder.AddHeaderRow;
                    break;
                case RowStyle.Highlighted:
                    add = _tableBuilder.AddHighlightedRow;
                    break;
                case RowStyle.Normal:
                    add = _tableBuilder.AddRow;
                    break;
                case RowStyle.Footer:
                    add = _tableBuilder.AddFooterRow;
                    break;
                default:
                    throw new InvalidEnumArgumentException();
            }

            foreach (var row in table.Rows)
            {
                add(row.Values.ToArray());
            }
        }
        [When(@"I build the alert")]
        public void WhenIBuildTheAlert()
        {
            _compositeAlert = _tableBuilder.Close().ToAlert() as CompositeFluentAlert;
            _alertTable = _compositeAlert[0] as FluentAlertTable;
        }
        
        [When(@"I create a document")]
        public void WhenICreateADocument()
        {
            _compositeAlert = _factory.Create().ToAlert() as CompositeFluentAlert;
        }
        [When(@"I create a document with the text")]
        public void WhenICreateADocumentWithTheTextTitle()
        {
            _compositeAlert = _factory.Create(_text).ToAlert() as CompositeFluentAlert;
        }
        [When(@"I add a exception as a list")]
        public void WhenIAddAExceptionAsAList()
        {
            _builder.AddException(_exception);
        }
        [When(@"I add a exception as a table")]
        public void WhenIAddAExceptionAsATable()
        {
            _builder.AddExceptionAsTable(_exception);
        }
        [When(@"I add a seperator")]
        public void WhenIAddASeperator()
        {
            _builder.AddSeperator();
        }
        [When(@"I add a url")]
        public void WhenIAddAUrl()
        {
            _builder.AddUrl(_text, _url);
        }
        [When(@"I add a object as a table")]
        public void WhenIAddAObjectAsATable()
        {
            _builder.AddObjectAsTable(TestObject.Create());
        }
        [When(@"I add a list of objects as a list of tables")]
        public void WhenIAddAListOfObjectsAsAListOfTables()
        {
            _builder.AddObjectsAsTables(TestObject.CreateList(3));
        }
        [When(@"I add a url wit the text as the name")]
        public void WhenIAddAUrlWitTheTextAsTheName()
        {
            ScenarioContext.Current.Pending();
        }
        [When(@"I create an alert exception with the builder")]
        public void WhenICreateAnAlertExceptionWithTheBuilder()
        {
            _alertException = new FluentAlertException(_builder);
        }
        [When(@"I create an alert exception with a builder and other exception")]
        public void WhenICreateAnAlertExceptionWithABuilderAndOtherException()
        {
            _alertException = new FluentAlertException(_builder, _innerException);
        }
        [When(@"I create an alert exception with the alert")]
        public void WhenICreateAnAlertExceptionWithTheAlert()
        {
            _alertException = new FluentAlertException(_alert);
        }
        [When(@"I create an alert exception with an alert and other exception")]
        public void WhenICreateAnAlertExceptionWithAnAlertAndOtherException()
        {
            _alertException = new FluentAlertException(_alert, _innerException);
        }
        [When(@"I create an alert exception with the text message")]
        public void WhenICreateAnAlertExceptionWithTheTextMessage()
        {
            _alertException = new FluentAlertException(_textMessage);
        }
        [When(@"I create an alert exception with text message and the inner exception")]
        public void WhenICreateAnAlertExceptionWithTextMessageAndTheInnerException()
        {
            _alertException = new FluentAlertException(_textMessage, _innerException);
        }




        [Then(@"the document should be empty")]
        public void ThenTheDocumentShouldBeEmpty()
        {
            Assert.AreEqual(0,_compositeAlert.Count);
        }
        [Then(@"the document should be a list of alerts")]
        public void ThenTheDocumentShouldBeAListOfAlerts()
        {
            Assert.IsInstanceOf<CompositeFluentAlert>(_compositeAlert);
        }
        [Then(@"the document should contain a url as the last alert")]
        public void ThenTheDocumentShouldContainAUrlAsTheLastAlert()
        {
            ScenarioContext.Current.Pending();
        }
        [Then(@"the document should contain a seperator as the last alert")]
        public void ThenTheDocumentShouldContainASeperatorAsTheLastAlert()
        {
            ScenarioContext.Current.Pending();
        }
        [Then(@"the document should contain a text block with a value of the text as the last alert")]
        public void ThenTheDocumentShouldContainATextBlockWithAValueOfTheTextTitleAsTheLastAlert()
        {
            ScenarioContext.Current.Pending();
        }
        [Then(@"the document should contain a url as the last alert with the url and text")]
        public void ThenTheDocumentShouldContainAUrlAsTheLastAlertWithTheUrlAndText()
        {
            ScenarioContext.Current.Pending();
        }
        [Then(@"the exception alert is the alert created from the builder")]
        public void ThenTheExceptionAlertIsTheAlertCreatedFromTheBuilder()
        {
            Assert.AreEqual(_builder.ToAlert(), _alertException.Alert);
        }
        [Then(@"inner exception is other exception")]
        public void ThenInnerExceptionIsOtherException()
        {
            Assert.AreEqual(_innerException, _alertException.InnerException);
        }
        [Then(@"the exception alert is the alert")]
        public void ThenTheExceptionAlertIsTheAlert()
        {
            Assert.AreEqual(_alert, _alertException.Alert);
        }
        [Then(@"the exception message is the alert serialized to text")]
        public void ThenTheExceptionMessageIsTheAlertSerializedToText()
        {
            Assert.AreEqual(
                _alert.ToText(o => o.Replace(Environment.NewLine, string.Empty)),
                _alertException.Message);
        }
        [Then(@"the exception alert contains a text block containing the text message")]
        public void ThenTheExceptionAlertContainsATextBlockContainingSomeText()
        {
            Assert.IsInstanceOf<FluentAlertTextBlock>(_alertException.Alert);
            var alert = _alertException.Alert as FluentAlertTextBlock;
            var actual = alert.Text.ToString();
            Assert.AreEqual(_textMessage, actual);
        }
        [Then(@"exception message is the text message")]
        public void ThenExceptionMessageIsTheSomeText()
        {
            var actual = _alertException.Message;
            Assert.AreEqual(_textMessage, actual);
        }
        [Then(@"the table should have a row for the type, message and stack trace for all exception in chain")]
        public void ThenTheTableShouldHaveARowForTheTypeMessageAndStackTraceForAllExceptionInChain()
        {
            Assert_TableMatchesException(_alertTable, _exception);
        }
        [Then(@"the title is the objects type")]
        public void ThenTitleIsTheObjectsType()
        {
            Assert_TablesTitleIsTheObjectsType(_alertTable, _object, _depth);
        }
        [Then(@"a row is added for each property with cells for property name and property value")]
        public void ThenARowIsAddedForEachPropertyWithCellsForPropertyNameAndPropertyValue()
        {
            Assert_HasSingleRowForEachPropertyWithCellsForNameAndValue(_alertTable, _object, _depth);
        }
        [Then(@"the alert should have (\d+) rows")]
        public void ThenTheAlertShouldHaveXRows(int rows)
        {
            Assert.AreEqual(rows, _alertTable.Rows.Count);
        }
        [Then(@"the alert should have (\d+) columns")]
        public void ThenTheAlertShouldHaveXColumns(int columnCount)
        {
            Assert.AreEqual(columnCount, _alertTable.ColumnCount);
        }
        [Then(@"row (\d+) should have (\d+) cells")]
        public void ThenRowXShouldHaveYCells(int row, int cells)
        {
            Assert.AreEqual(cells, GetRow(_alertTable, row).Values.Length);
        }
        [Then(@"row (\d+) should be a '(.*)' row")]
        public void ThenRowXShouldBeAHeaderRow(int row, string style)
        {
            Assert.AreEqual(GetRowStyle(style), GetRow(_alertTable, row).Style);
        }
        [Then(@"row (\d+) cell (\d+) should be '(.*)'")]
        public void ThenRowXCellXShouldBeATypeEqualToValue(int row, int cell, string value)
        {
            Assert.AreEqual(value, GetCell<string>(_alertTable, row, cell));
        }



        #region helpers
        private const int ROW_NAME_CELL = 1;
        private const int ROW_VALUE_CELL = 2;
        private static string GetTitle(FluentAlertTable alertTable)
        {
            return GetCell<string>(alertTable, r => r.Style == RowStyle.Header && r.Values.Length == 1, 1);
        }
        private static FluentAlertTable.Row GetRow(FluentAlertTable alertTable, int row)
        {
            return alertTable.Rows[row - 1];
        }
        private static FluentAlertTable.Row GetRow(FluentAlertTable alertTable, Func<FluentAlertTable.Row, bool> rowMatchPredicate)
        {
            return alertTable.Rows.FirstOrDefault(rowMatchPredicate);
        }
        private static TCell GetNameCell<TCell>(FluentAlertTable alertTable, int row)
        {
            return GetCell<TCell>(GetRow(alertTable, row), ROW_NAME_CELL);
        }
        private static TCell GetValueCell<TCell>(FluentAlertTable alertTable, int row)
        {
            return GetCell<TCell>(GetRow(alertTable, row), ROW_VALUE_CELL);
        }
        private static TCell GetCell<TCell>(FluentAlertTable alertTable, int row, int cell)
        {
            return GetCell<TCell>(GetRow(alertTable, row), cell);
        }
        private static TCell GetCell<TCell>(FluentAlertTable alert, string rowName, int cell)
        {
            return GetCell<TCell>(alert, r => GetCell<string>(r, ROW_NAME_CELL) == rowName, cell);
        }
        private static TCell GetCell<TCell>(FluentAlertTable alert, string rowName, RowStyle rowStyle, int cell)
        {
            return GetCell<TCell>(alert, r => GetCell<string>(r, ROW_NAME_CELL) == rowName && r.Style == rowStyle, cell);
        }
        private static TCell GetCell<TCell>(FluentAlertTable alertTable, Func<FluentAlertTable.Row, bool> rowMatchPredicate, int cell)
        {
            var row = GetRow(alertTable, rowMatchPredicate);
            return GetCell<TCell>(row, cell);
        }
        private static TCell GetCell<TCell>(FluentAlertTable.Row row, int cell)
        {
            var result = (TCell)row.Values[cell - 1];
            return result;
        }

        private static void Assert_TableMatchesException(FluentAlertTable alert, Exception ex)
        {
            var row = 1;
            foreach (var e in ex.ToList())
            {
                //Scrolled list of Title, Message, StackTrace
                Assert.AreEqual(e.GetType().Name, GetNameCell<string>(alert, row), "Title");
                Assert.AreEqual(e.Message, GetValueCell<string>(alert, row + 1), "Message");
                Assert.AreEqual(e.StackTrace, GetValueCell<string>(alert, row + 2), "Stack Trace");

                //increment three rows
                row += 3;
            }
        }
        private static void Assert_TablesTitleIsTheObjectsType(FluentAlertTable alert, TestObject source, int toDepth = 0)
        {
            Assert.AreEqual(source.GetType().Name, GetTitle(alert), "Title");

            if (toDepth == 0) return;
            var innerAlert = GetCell<FluentAlertTable>(alert, "Inner", ROW_VALUE_CELL);
            Assert_TablesTitleIsTheObjectsType(innerAlert, source.Inner, --toDepth);
        }
        private static void Assert_HasSingleRowForEachPropertyWithCellsForNameAndValue(FluentAlertTable alert, TestObject source, int toDepth)
        {
            Assert.AreEqual(source.One, GetCell<string>(alert, "One", RowStyle.Normal, ROW_VALUE_CELL));
            Assert.AreEqual(source.Two, GetCell<decimal>(alert, "Two", RowStyle.Normal, ROW_VALUE_CELL));
            if (toDepth == 0) return;
            Assert_HasSingleRowForEachPropertyWithCellsForNameAndValue(
                GetCell<FluentAlertTable>(alert, "Inner", ROW_VALUE_CELL), source.Inner, --toDepth);
        }
        #endregion
    }
}
