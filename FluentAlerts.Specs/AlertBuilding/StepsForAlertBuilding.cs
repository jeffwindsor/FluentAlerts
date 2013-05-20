using System;
using System.Linq;
using TechTalk.SpecFlow;
using FluentAssertions;
using System.Collections.Generic;

namespace FluentAlerts.Specs.AlertBuilding
{
    [Binding]
    public class StepsForAlertBuilding
    {
        private IAlertBuilder _builder;
        private IAlert _alert;
        private string _text = "Test Text String";
        private const string _url = "Test Url String";
        private readonly object[] _values = new object[] {"one",12,DateTime.UtcNow};
        private IAlert _otherAlert;

        [Given(@"I have an alert builder")]
        public void GivenIHaveAnAlertBuilder()
        {
            _builder = Alerts.Create();
        }

        [Given(@"I have an alert builder and a title")]
        public void GivenIHaveAnAlertBuilderAndATitle()
        {
            _builder = Alerts.Create(_text);
        }


        [When(@"I build the alert")]
        public void WhenICreateAnAlert()
        {
            _alert = _builder.ToAlert();
        }
        
        [When(@"I add a seperator")]
        public void WhenIAddASeperator()
        {
            _builder.WithSeperator();
        }

        [When(@"I add a url")]
        public void WhenIAddAUrl()
        {
            _builder.WithUrl(_text, _url);
        }

        [When(@"I add an object")]
        public void WhenIAddAnObject()
        {
            _builder.WithValue(_values[0]);
        }

        [When(@"I add a list of object")]
        public void WhenIAddAListOfObject()
        {
            _builder.WithValues(_values);
        }
        
        [When(@"I add a title")]
        public void WhenIAddATitle()
        {
            _text = "Change Title";
            _builder.WithTitleOf(_text);
        }

        [When(@"I add a format based title")]
        public void WhenIAddAFormatBasedTitle()
        {
            var zero = "zero";
            var one = "one";
            var format = "{0} {1}";
            _text = string.Format(format, zero, one);
            _builder.WithTitleOf(format, zero, one);
        }

        [When(@"I add another alert")]
        public void WhenIAddAnotherAlert()
        {
            _otherAlert = Alerts.Create("Other Title")
                .WithUrl("Other Url", "Http://otherUrl.com")
                .ToAlert();

            _builder.WithAlert(_otherAlert);
        }

        [When(@"I add Emphasized text")]
        public void WhenIAddEmpahsizedText()
        {
            _builder.WithEmphasized( _text);
        }

        [When(@"I add Normal text")]
        public void WhenIAddText()
        {
            _builder.With( _text);
        }

        [When(@"I add Header text")]
        public void WhenIAddHeaderText()
        {
            _builder.WithHeader( _text);
        }

        [When(@"I add a Normal row")]
        public void WhenIAddANormalRow()
        {
            _builder.WithRow(_values);
        }

        [When(@"I add a Emphasized row")]
        public void WhenIAddAEmphasizedRow()
        {
            _builder.WithEmphasizedRow(_values);
        }
        
        [Then(@"the alert should contain that Normal row as the last item")]
        public void ThenTheAlertShouldContainThatNormalRowAsTheLastItem()
        {
            ThenTheAlertShouldContainThatGroupStyleRowAsTheLastItem(GroupStyle.Row);
        }

        [Then(@"the alert should contain that Emphasized row as the last item")]
        public void ThenTheAlertShouldContainThatEmphasizedRowAsTheLastItem()
        {
            ThenTheAlertShouldContainThatGroupStyleRowAsTheLastItem(GroupStyle.EmphasizedRow);
        }

        public void ThenTheAlertShouldContainThatGroupStyleRowAsTheLastItem(GroupStyle style)
        {
            var item = AssertLastItemIsGroupOfStyle(style);
            for (var i = 0; i < _values.Length; ++i)
            {
                item.Values[i].Should().Be(_values[i]);
            }
        }



        [Then(@"the alert should contain (.*) text as the last item")]
        public void ThenTheAlertShouldContainTextAsThLastAlertItem(TextStyle style)
        {
            var item = AssertLastItemIsTypeAndConvertTo<AlertTextBlock>();
            item.ToString().Should().Be(_text); 
            item.Style = style;
        }
        
        [Then(@"the alert should be empty")]
        public void ThenTheAlertShouldBeEmpty()
        {
            _alert.Count.Should().Be(0, "Alert is not empty");
        }

        [Then(@"the alert should be a list of alert items")]
        public void ThenTheAlertShouldBeAListOfAlertItems()
        {
            _alert.Should().BeAssignableTo<IEnumerable<IAlertItem>>();
        }

        [Then(@"the alert should contain title as the first item")]
        public void ThenTheAlertShouldContainTitleAsTheFirstAlertItem()
        {
            var item = AssertItemIsTypeAndConvertTo<AlertTextBlock>(_alert.First()); 
            item.ToString().Should().Be(_text);
        }
        
        [Then(@"the alert should contain a seperator as the last item")]
        public void ThenTheAlertShouldContainASeperatorAsTheLastAlert()
        {
            AssertLastItemIsGroupOfStyle(GroupStyle.Seperator); 
        }
        
        [Then(@"the alert should contain a url as the last item with the url and text")]
        public void ThenTheAlertShouldContainAUrlAsTheLastAlertWithTheUrlAndText()
        {
            var item = AssertLastItemIsGroupOfStyle(GroupStyle.Url);
            item.Values.First().Should().Be(_text, "Url Text");
            item.Values.Last().Should().Be(_url, "Url");
        }
         
        [Then(@"the alert should contain that object as the last item")]
        public void ThenTheAlertShouldContainThatObjectAsTheLastAlert()
        {
            var item = AssertLastItemIsGroupOfStyle(GroupStyle.Value);
            item.Values.Count().Should().Be(1);
            item.Values[0].Should().Be(_values[0]);
        }
        
        [Then(@"the alert should contain each object in order")]
        public void ThenTheAlertShouldContainEachObjectInOrder()
        {
            var delta = _alert.Count - _values.Length;
            for (var i = 0; i < _values.Length; ++i)
            {
                var value = AssertGroupIs(_alert[i + delta], GroupStyle.Value);
                value.Values[0].Should().Be(_values[i]);
            }
        }

        [Then(@"the alert should contain all the other alert's items")]
        public void ThenTheAlertShouldContainTheOtherAlertsItems()
        {
            var delta = _alert.Count - _otherAlert.Count;
            for (var i = 0; i < _otherAlert.Count; ++i)
            {
                _alert[i + delta].Should().Be(_otherAlert[i]);
            }
        }
        
        private AlertGroup AssertLastItemIsGroupOfStyle(GroupStyle style)
        {
            return AssertGroupIs(_alert.Last(), style);
        }
        
        private static AlertGroup AssertGroupIs(IAlertItem source, GroupStyle style) 
        {
            var result = AssertItemIsTypeAndConvertTo<AlertGroup>(source);
            result.Style.Should().Be(style);
            return result;
        }
        
        private T AssertLastItemIsTypeAndConvertTo<T>() where T : IAlertItem
        {
            return AssertItemIsTypeAndConvertTo<T>(_alert.Last());
        }

        private static T AssertItemIsTypeAndConvertTo<T>(IAlertItem source) where T : IAlertItem
        {
            var result = source.As<T>();
            result.Should().NotBeNull();
            return result;
        }

    }
}
