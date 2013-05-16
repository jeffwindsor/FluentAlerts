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
        private string _title = "Test TItle";
        private AlertException _caughtException;
        private Exception _originalException;

        [Given(@"I have an filled alert builder")]
        public void GivenIHaveAnAlertBuilder()
        {
            _builder = Alerts.Create().WithTitleOf(_title);
        }

        [When(@"I throw the alert")]
        public void WhenIThrowTheAlert()
        {
            try
            {
                _alert = _builder.ToAlert();
                _builder.Throw();
            }
            catch (AlertException ex)
            {
                _caughtException = ex;
            }
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
            _caughtException.Alert.Title.Should().Be(_title);
        }

        [When(@"I throw the alert as some dervied alert exception")]
        public void WhenIThrowTheAlertAsSomeDerviedAlertException()
        {
            try
            {
                _alert = _builder.ToAlert();
                _builder.ThrowAs<SpecsAlertException>((alert, inner) => new SpecsAlertException(alert, inner));
            }
            catch (AlertException ex)
            {
                _caughtException = ex;
            }
        }

        [Then(@"the exception is of the derived alert exception type")]
        public void ThenTheExceptionIsOfTheDerivedAlertExceptionType()
        {
            _caughtException.Should().BeOfType<SpecsAlertException>();
        }

        [Given(@"I have an exception")]
        public void GivenIHaveAnException()
        {
            _originalException = StepsMother.GetNestedException(0);
        }

        [When(@"I wrap it in an alert")]
        public void WhenIWrapItInAnAlert()
        {
            _builder = _originalException.WrapInAlert().WithTitleOf(_title);
        }

        [Then(@"the original exception is now the inner exception")]
        public void ThenTheOriginalExceptionIsNowTheInnerException()
        {
            _caughtException.InnerException.Should().Be(_originalException);
        }

    }
}
