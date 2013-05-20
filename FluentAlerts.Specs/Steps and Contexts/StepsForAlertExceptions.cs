using System;
using TechTalk.SpecFlow;
using FluentAssertions;

namespace FluentAlerts.Specs
{
    [Binding] 
    public class StepsForAlertsExceptions
    {
        private AlertException _caughtException;
        private Exception _originalException;

        private AlertContext _context;
        public StepsForAlertsExceptions(AlertContext context)
        {
            _context = context;
        }

        [Given(@"I have an filled alert builder")]
        public void GivenIHaveAnAlertBuilder()
        {
            _context.Builder = Alerts.Create().WithTitleOf(_context.TestText);
        }

        [Given(@"I have an exception")]
        public void GivenIHaveAnException()
        {
            _originalException = Mother.GetNestedException(0);
        }


        [When(@"I wrap it in an alert")]
        public void WhenIWrapItInAnAlert()
        {
            _context.Builder = _originalException.WrapInAlert().WithTitleOf(_context.TestText);
        }

        [When(@"I throw the alert")]
        public void WhenIThrowTheAlert()
        {
            try
            {
                _context.Alert = _context.Builder.ToAlert();  //for comparison later
                _context.Builder.Throw();
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
                _context.Alert = _context.Builder.ToAlert();  //for comparison later
                _context.Builder.ThrowAs((alert, inner) => new SpecsAlertException(alert, inner));
            }
            catch (AlertException ex)
            {
                _caughtException = ex;
            }
        }
        
        [When(@"I create an alert exception with the builder")]
        public void WhenICreateAnAlertExceptionWithTheBuilder()
        {
            _context.Alert = _context.Builder.ToAlert();  //for comparison later
            _caughtException = new AlertException(_context.Builder);
        }

        [When(@"I create an alert exception with a builder and other exception")]
        public void WhenICreateAnAlertExceptionWithABuilderAndOtherException()
        {
            _context.Alert = _context.Builder.ToAlert();  //for comparison later
            _caughtException = new AlertException(_context.Builder,_originalException);
        }

        [When(@"I create an alert exception with the text message")]
        public void WhenICreateAnAlertExceptionWithTheTextMessage()
        {
            _caughtException = new AlertException(_context.TestText);
        }

        [When(@"I create an alert exception with text message and the inner exception")]
        public void WhenICreateAnAlertExceptionWithTextMessageAndTheInnerException()
        {
            _caughtException = new AlertException(_context.TestText,_originalException);
        }

        [Then(@"the exception is an alert exception")]
        public void ThenTheExceptionIsAnAlertException()
        {
            _caughtException.Should().BeOfType<AlertException>();
        }

        [Then(@"the exception contains the alert")]
        public void ThenTheExceptionContainsTheAlert()
        {
            _context.Alert.Should().BeEquivalentTo(_caughtException.Alert);
        }

        [Then(@"the exception's message is the alerts title")]
        public void ThenTheExceptionsMessageIsTheAlertsTitle()
        {
            _caughtException.Alert.Title.Should().Be(_context.TestText);
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
            _caughtException.Message.Should().Be(_context.TestText);
        }
        
        //Simulates an external exception derived from the alert exception
        public class SpecsAlertException : AlertException
        {
            public SpecsAlertException(IAlert alert) : base(alert) { }
            public SpecsAlertException(IAlert alert, Exception inner) : base(alert, inner) { }
        }
    }

}
