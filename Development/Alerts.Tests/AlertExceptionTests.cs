using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Alerts;

namespace Tests.Alerts
{
    public class AlertExceptionTests : AlertBaseTest
    {
        [Test]
        public void WhenCreatedWithMessageStringThenInnerMessageIsString()
        {
            const string message = "asfasgasgdsghashas'";
            var ex = new AlertException(message);
            //Validate message is serialized with new line
            Assert.AreEqual(message + Environment.NewLine, ex.Message);
        }

        [Test]
        public void WhenCreatedWithMessageStringThenExceptionAlertIsTextBlockOfString()
        {
            const string message = "asfasgasgdsghashas'";
            var ex = new AlertException(message);
            var alert = ex.Alert as AlertTextBlock;
            Assert.IsNotNull(alert);
            Assert.AreEqual(message, alert.Text.ToString());
        }

        [Test]
        public void WhenCreatedWithBuilderThenExceptionAlertIsBuilderToAlert()
        {
            var builder = BuilderFactory.CreateDocument("Test Title");
            var ex = new AlertException(builder);
            Assert.AreEqual(builder.ToAlert(), ex.Alert);
        }

        [Test]
        public void WhenCreatedWithAlertThenInnerMessageIsAlertToText()
        {
            var alert = BuilderFactory.CreateDocument().ToAlert();
            var ex = new AlertException(alert);
            Assert.AreEqual(alert, ex.Alert);
        }

        [Test]
        public void WhenCreatedWithAlertThenExceptionAlertIsAlert()
        {
            var alert = BuilderFactory.CreateDocument().ToAlert();
            var ex = new AlertException(alert);
            Assert.AreEqual(alert.ToText(), ex.Message);
        }


        [Test]
        public void WhenCreatedWithMessageStringAndInnerThenInnerMessageIsString()
        {
            const string message = "asfasgasgdsghashas'";
            var inner = Mother.GetNestedException(0);
            var ex = new AlertException(message,inner);
            //Validate message is serialized with new line
            Assert.AreEqual(message + Environment.NewLine, ex.Message);
            Assert.AreEqual(inner, ex.InnerException);
        }

        [Test]
        public void WhenCreatedWithMessageStringAndInnerThenExceptionAlertIsTextBlockOfString()
        {
            const string message = "asfasgasgdsghashas'";
            var inner = Mother.GetNestedException(0);
            var ex = new AlertException(message,inner);
            var alert = ex.Alert as AlertTextBlock;
            Assert.IsNotNull(alert);
            Assert.AreEqual(message, alert.Text.ToString());
            Assert.AreEqual(inner, ex.InnerException);
        }

        [Test]
        public void WhenCreatedWithBuilderAndInnerThenExceptionAlertIsBuilderToAlert()
        {
            var builder = BuilderFactory.CreateDocument("Test Title");
            var inner = Mother.GetNestedException(0);
            var ex = new AlertException(builder,inner);
            Assert.AreEqual(builder.ToAlert(), ex.Alert);
            Assert.AreEqual(inner, ex.InnerException);
        }
        
        [Test]
        public void WhenCreatedWithAlertAndInnerThenInnerMessageIsAlertToText()
        {
            var alert = BuilderFactory.CreateDocument().ToAlert();
            var inner = Mother.GetNestedException(0);
            var ex = new AlertException(alert,inner);
            Assert.AreEqual(alert, ex.Alert);
            Assert.AreEqual(inner, ex.InnerException);
        }

        [Test]
        public void WhenCreatedWithAlertAndInnerThenExceptionAlertIsAlert()
        {
            var alert = BuilderFactory.CreateDocument().ToAlert();
            var inner = Mother.GetNestedException(0);
            var ex = new AlertException(alert,inner);
            Assert.AreEqual(alert.ToText(), ex.Message);
            Assert.AreEqual(inner, ex.InnerException);
        }
    }
}
