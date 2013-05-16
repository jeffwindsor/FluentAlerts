using System;
using TechTalk.SpecFlow;
using FluentAssertions;

namespace FluentAlerts.Specs
{
    [Binding]
    public class StepsForExceptions
    {
        private IAlertBuilder _builder;
        private IAlert _alert;
        private const string _testText = "Test Title";
        private AlertException _caughtException;
        private Exception _originalException;

        [Given(@"I have an filled alert builder")]
        public void GivenIHaveAnAlertBuilder()
        {
            _builder = Alerts.Create().WithTitleOf(_testText);
        }

        [Given(@"I have an exception")]
        public void GivenIHaveAnException()
        {
            _originalException = StepsMother.GetNestedException(0);
        }


        [When(@"I wrap it in an alert")]
        public void WhenIWrapItInAnAlert()
        {
            _builder = _originalException.WrapInAlert().WithTitleOf(_testText);
        }

        [When(@"I throw the alert")]
        public void WhenIThrowTheAlert()
        {
            try
            {
                _alert = _builder.ToAlert();  //for comparison later
                _builder.Throw();
            }
            catch (AlertException ex)
            {
                _caughtException = ex;
            }
        }

        /// <summary>
        /// Throw an alert inside an exception that inherits AlertException and exposes
        /// at a minimum a constructor of (IAlert alert, Exception inner)
        /// </summary>
        [When(@"I throw the alert as some dervied alert exception")]
        public void WhenIThrowTheAlertAsSomeDerviedAlertException()
        {
            try
            {
                _alert = _builder.ToAlert();  //for comparison later
                _builder.ThrowAs((alert, inner) => new SpecsAlertException(alert, inner));
            }
            catch (AlertException ex)
            {
                _caughtException = ex;
            }
        }
        
        [When(@"I create an alert exception with the builder")]
        public void WhenICreateAnAlertExceptionWithTheBuilder()
        {
            _alert = _builder.ToAlert();  //for comparison later
            _caughtException = new AlertException(_builder);
        }

        [When(@"I create an alert exception with a builder and other exception")]
        public void WhenICreateAnAlertExceptionWithABuilderAndOtherException()
        {
            _alert = _builder.ToAlert();  //for comparison later
            _caughtException = new AlertException(_builder,_originalException);
        }

        [When(@"I create an alert exception with the text message")]
        public void WhenICreateAnAlertExceptionWithTheTextMessage()
        {
            _caughtException = new AlertException(_testText);
        }

        [When(@"I create an alert exception with text message and the inner exception")]
        public void WhenICreateAnAlertExceptionWithTextMessageAndTheInnerException()
        {
            _caughtException = new AlertException(_testText,_originalException);
        }

        [Then(@"the exception is an alert exception")]
        public void ThenTheExceptionIsAnAlertException()
        {
            _caughtException.Should().BeOfType<AlertException>();
        }

        [Then(@"the exception contains the alert")]
        public void ThenTheExceptionContainsTheAlert()
        {
            _alert.Should().BeEquivalentTo(_caughtException.Alert);
        }

        [Then(@"the exception's message is the alerts title")]
        public void ThenTheExceptionsMessageIsTheAlertsTitle()
        {
            _caughtException.Alert.Title.Should().Be(_testText);
        }
        
        [Then(@"the exception is of the derived alert exception type")]
        public void ThenTheExceptionIsOfTheDerivedAlertExceptionType()
        {
            _caughtException.Should().BeOfType<SpecsAlertException>();
        }

        [Then(@"the original exception is now the inner exception")]
        public void ThenTheOriginalExceptionIsNowTheInnerException()
        {
            _caughtException.InnerException.Should().Be(_originalException);
        }
       
        [Then(@"exception message is the simple text")]
        public void ThenExceptionMessageIsTheTextMessage()
        {
            _caughtException.Message.Should().Be(_testText);
        }
        
    }
}
