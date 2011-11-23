using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentAlerts.Extensions;
using FluentAlerts.Nodes;
using NUnit.Framework;
using FluentAlerts;

namespace Tests.FluentAlerts
{
    public class AlertExceptionTests : AlertBaseTest
    {
        [Test]
        public void WhenCreatedWithMessageStringThenInnerMessageIsString()
        {
            const string message = "asfasgasgdsghashas'";
            var ex = new FluentAlertException(message);
            //Validate message is serialized with new line
            Assert.AreEqual(message + Environment.NewLine, ex.Message);
        }

        [Test]
        public void WhenCreatedWithMessageStringThenExceptionAlertIsTextBlockOfString()
        {
            const string message = "asfasgasgdsghashas'";
            var ex = new FluentAlertException(message);
            var alert = ex.Alert as FluentAlertTextBlock;
            Assert.IsNotNull(alert);
            Assert.AreEqual(message, alert.Text.ToString());
        }

        [Test]
        public void WhenCreatedWithBuilderThenExceptionAlertIsBuilderToAlert()
        {
            var builder = BuilderFactory.CreateDocument("Test Title");
            var ex = new FluentAlertException(builder);
            Assert.AreEqual(builder.ToAlert(), ex.Alert);
        }

        [Test]
        public void WhenCreatedWithAlertThenInnerMessageIsAlertToText()
        {
            var alert = BuilderFactory.CreateDocument().ToAlert();
            var ex = new FluentAlertException(alert);
            Assert.AreEqual(alert, ex.Alert);
        }

        [Test]
        public void WhenCreatedWithAlertThenExceptionAlertIsAlert()
        {
            var alert = BuilderFactory.CreateDocument().ToAlert();
            var ex = new FluentAlertException(alert);
            Assert.AreEqual(alert.ToText(), ex.Message);
        }


        [Test]
        public void WhenCreatedWithMessageStringAndInnerThenInnerMessageIsString()
        {
            const string message = "asfasgasgdsghashas'";
            var inner = Mother.GetNestedException(0);
            var ex = new FluentAlertException(message,inner);
            //Validate message is serialized with new line
            Assert.AreEqual(message + Environment.NewLine, ex.Message);
            Assert.AreEqual(inner, ex.InnerException);
        }

        [Test]
        public void WhenCreatedWithMessageStringAndInnerThenExceptionAlertIsTextBlockOfString()
        {
            const string message = "asfasgasgdsghashas'";
            var inner = Mother.GetNestedException(0);
            var ex = new FluentAlertException(message,inner);
            var alert = ex.Alert as FluentAlertTextBlock;
            Assert.IsNotNull(alert);
            Assert.AreEqual(message, alert.Text.ToString());
            Assert.AreEqual(inner, ex.InnerException);
        }

        [Test]
        public void WhenCreatedWithBuilderAndInnerThenExceptionAlertIsBuilderToAlert()
        {
            var builder = BuilderFactory.CreateDocument("Test Title");
            var inner = Mother.GetNestedException(0);
            var ex = new FluentAlertException(builder,inner);
            Assert.AreEqual(builder.ToAlert(), ex.Alert);
            Assert.AreEqual(inner, ex.InnerException);
        }
        
        [Test]
        public void WhenCreatedWithAlertAndInnerThenInnerMessageIsAlertToText()
        {
            var alert = BuilderFactory.CreateDocument().ToAlert();
            var inner = Mother.GetNestedException(0);
            var ex = new FluentAlertException(alert,inner);
            Assert.AreEqual(alert, ex.Alert);
            Assert.AreEqual(inner, ex.InnerException);
        }

        [Test]
        public void WhenCreatedWithAlertAndInnerThenExceptionAlertIsAlert()
        {
            var alert = BuilderFactory.CreateDocument().ToAlert();
            var inner = Mother.GetNestedException(0);
            var ex = new FluentAlertException(alert,inner);
            Assert.AreEqual(alert.ToText(), ex.Message);
            Assert.AreEqual(inner, ex.InnerException);
        }
    }
}
