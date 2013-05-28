using System;
using System.Linq;
using TechTalk.SpecFlow;
using FluentAssertions;
using System.Collections.Generic;

namespace FluentAlerts.Specs
{
    [Binding]
    public class StepsForAlertBuilding
    {
        private AlertContext _context;
        public StepsForAlertBuilding(AlertContext context)
        {
            _context = context;
        }

        [Given(@"I have an alert builder")]
        public void GivenIHaveAnAlertBuilder()
        {
            _context.Builder = Alerts.Create();
        }

        [Given(@"I have an alert builder and a title")]
        public void GivenIHaveAnAlertBuilderAndATitle()
        {
            _context.Builder = Alerts.Create(_context.TestText);
        }
        
        [Given(@"I have an filled alert builder")]
        public void GivenIHaveAFilledAlertBuilder()
        {
            _context.Builder = Alerts.Create()
                .WithTitleOf(_context.TestText)
                .WithSeperator()
                .With("Some other text");
        }

        [Given(@"I have a built alert")]
        public void GivenIHaveABuiltAlert()
        {
            GivenIHaveAFilledAlertBuilder();
            WhenIBuildAnAlert();
        }

        [When(@"I build the alert")]
        public void WhenIBuildAnAlert()
        {
            _context.Alert = _context.Builder.ToAlert();
        }
        
        [When(@"I add a seperator")]
        public void WhenIAddASeperator()
        {
            _context.Builder.WithSeperator();
        }

        [When(@"I add a url")]
        public void WhenIAddAUrl()
        {
            _context.Builder.WithUrl(_context.TestText, _context.TestUrl);
        }

        [When(@"I add an object")]
        public void WhenIAddAnObject()
        {
            _context.Builder.WithValue(_context.TestValues[0]);
        }

        [When(@"I add a list of object")]
        public void WhenIAddAListOfObject()
        {
            _context.Builder.WithValues(_context.TestValues);
        }
        
        [When(@"I add a title")]
        public void WhenIAddATitle()
        {
            _context.TestText = "Change Title";
            _context.Builder.WithTitleOf(_context.TestText);
        }

        [When(@"I add a format based title")]
        public void WhenIAddAFormatBasedTitle()
        {
            var zero = "zero";
            var one = "one";
            var format = "{0} {1}";
            _context.TestText = string.Format(format, zero, one);
            _context.Builder.WithTitleOf(format, zero, one);
        }

        [When(@"I add another alert")]
        public void WhenIAddAnotherAlert()
        {
            _context.OtherAlert = Alerts.Create("Other Title")
                .WithUrl("Other Url", "Http://otherUrl.com")
                .ToAlert();

            _context.Builder.WithAlert(_context.OtherAlert);
        }

        [When(@"I add Emphasized text")]
        public void WhenIAddEmpahsizedText()
        {
            _context.Builder.WithEmphasized( _context.TestText);
        }

        [When(@"I add Normal text")]
        public void WhenIAddText()
        {
            _context.Builder.With( _context.TestText);
        }

        [When(@"I add Header_One text")]
        public void WhenIAddHeader_OneText()
        {
            _context.Builder.WithHeaderOne( _context.TestText);
        }

        [When(@"I add a Normal row")]
        public void WhenIAddANormalRow()
        {
            _context.Builder.WithRow(_context.TestValues);
        }

        [When(@"I add a Emphasized row")]
        public void WhenIAddAEmphasizedRow()
        {
            _context.Builder.WithEmphasizedRow(_context.TestValues);
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
            for (var i = 0; i < _context.TestValues.Length; ++i)
            {
                item.Values[i].Should().Be(_context.TestValues[i]);
            }
        }



        [Then(@"the alert should contain (.*) text as the last item")]
        public void ThenTheAlertShouldContainTextAsThLastAlertItem(TextStyle style)
        {
            var item = AssertLastItemIsTypeAndConvertTo<AlertTextBlock>();
            item.ToString().Should().Be(_context.TestText); 
            item.Style = style;
        }
        
        [Then(@"the alert should be empty")]
        public void ThenTheAlertShouldBeEmpty()
        {
            _context.Alert.Count.Should().Be(0, "Alert is not empty");
        }

        [Then(@"the alert should be a list of alert items")]
        public void ThenTheAlertShouldBeAListOfAlertItems()
        {
            _context.Alert.Should().BeAssignableTo<IEnumerable<IAlertItem>>();
        }

        [Then(@"the alert should contain title as the first item")]
        public void ThenTheAlertShouldContainTitleAsTheFirstAlertItem()
        {
            var item = AssertItemIsTypeAndConvertTo<AlertTextBlock>(_context.Alert.First()); 
            item.ToString().Should().Be(_context.TestText);
        }

        [When(@"I append to the title")]
        public void WhenIAppendToTheTitle()
        {
            _context.Builder.AppendTitleWith("extra");
            _context.TestText += "extra";
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
            item.Values.First().Should().Be(_context.TestText, "Url Text");
            item.Values.Last().Should().Be(_context.TestUrl, "Url");
        }
         
        [Then(@"the alert should contain that object as the last item")]
        public void ThenTheAlertShouldContainThatObjectAsTheLastAlert()
        {
            var item = AssertLastItemIsGroupOfStyle(GroupStyle.Value);
            item.Values.Count().Should().Be(1);
            item.Values[0].Should().Be(_context.TestValues[0]);
        }
        
        [Then(@"the alert should contain each object in order")]
        public void ThenTheAlertShouldContainEachObjectInOrder()
        {
            var delta = _context.Alert.Count - _context.TestValues.Length;
            for (var i = 0; i < _context.TestValues.Length; ++i)
            {
                var value = AssertGroupIs(_context.Alert[i + delta], GroupStyle.Value);
                value.Values[0].Should().Be(_context.TestValues[i]);
            }
        }

        [Then(@"the alert should contain all the other alert's items")]
        public void ThenTheAlertShouldContainTheOtherAlertsItems()
        {
            var delta = _context.Alert.Count - _context.OtherAlert.Count;
            for (var i = 0; i < _context.OtherAlert.Count; ++i)
            {
                _context.Alert[i + delta].Should().Be(_context.OtherAlert[i]);
            }
        }
        
        private AlertGroup AssertLastItemIsGroupOfStyle(GroupStyle style)
        {
            return AssertGroupIs(_context.Alert.Last(), style);
        }
        
        private static AlertGroup AssertGroupIs(IAlertItem source, GroupStyle style) 
        {
            var result = AssertItemIsTypeAndConvertTo<AlertGroup>(source);
            result.Style.Should().Be(style);
            return result;
        }
        
        private T AssertLastItemIsTypeAndConvertTo<T>() where T : IAlertItem
        {
            return AssertItemIsTypeAndConvertTo<T>(_context.Alert.Last());
        }

        private static T AssertItemIsTypeAndConvertTo<T>(IAlertItem source) where T : IAlertItem
        {
            var result = source.As<T>();
            result.Should().NotBeNull();
            return result;
        }

    }
}
