using System;
using System.Diagnostics;
using Newtonsoft.Json;
using NUnit.Framework;

namespace FluentAlerts.Examples
{
    [TestFixture]
    public class ExploringAlertCreation:BaseExample
    {        
        /*
         * Lets play around with creating some alerts.  
         * The test will serialize the alert to json and put it into the trace stream,
         * just so you can see the data.   
         * 
         * I will cover the following topics in other test files:
         *  > how to control the rendering process
         *    > how to set the default rendering template
         *    > how to set a specific template during rendering
         *    > how to create your own templates for custom output
         *  > how to control the transformation process
         *    > and advanced transformation topics like
         *      > how to customize type info rules down to a specific type and depth
         *      > how to customize formatting rules down to a specific type and depth
         *  > 
         */
        [Test]
        public void TurnAnObjectIntoAnAlert()
        {
            // Note how the objects public properties and fields are enumerated in
            // the result, with nested classes being enumerated to given depth.  And
            // the format of the each types string representation.
            // This is driven by the Transformer and Formatter classes used
            // as well as the type info and formatter rules.
            // ** We will get into modifying each one of those later
            var alerts = Get<IFluentAlerts>();
            var alert = alerts.ObjectBlock(Mother.CreateNestedTestObject(2));

            SerializeToConsole(alert);
        }

        [Test]
        public void TurnAnExceptionIntoAnAlert()
        {
            // An exception, or in this a class derived from exception is just another
            // object like above, but since we will be using these a lot and I
            // needed an example of how to specify transformation by type, here it is.
            // Note how the properties are limited to a select list an ordered in 
            // a specific way (as apposed to alpha in the example above).
            var alerts = Get<IFluentAlerts>();
            var alert = alerts.ExceptionBlock(Mother.CreateNestedException(4));

            SerializeToConsole(alert);
        }
        
        [Test]
        public void CreateSimpleTableLikeAlert()
        {
            var alerts = Get<IFluentAlerts>();
            var alert = alerts.Table("Those pant's don't match that shirt, my man.")
                .WithRow("Shirt Color", "Yellow")
                .WithRow("Pants Color", "Purple Plaid")
                .WithEmphasizedRow("Top 3 Reasons")
                .WithRow("One", "Yellow and Purple please.")
                .WithRow("Two", "Didn't work for Smoochie, and it won't work for you.")
                .WithRow("Three", "everyone knows your jeans should be green.");

            SerializeToConsole(alert);
        }
        
        [Test]
        public void CreateSimpleDocumentLikeAlerts()
        {
            var alerts = Get<IFluentAlerts>();
            var alert = alerts.Document("Create a simple document like alert")
                .WithHorizontalRule()
                .WithHeader("Hear is some code that is relevant to this alert", 2)
                .WithCodeBlock("C#", @"        private string GetSpanningDecoration(int index, int maximumValueIndex)
                    {
                        //span all remaining columns
                        var span = 1 + (maximumValueIndex - index);
                        var spanArgs = new[]
                            {
                                new RenderTemplateArguement(DecorationBasedRenderTemplateArguementType.SpanColumns,
                                                            span.ToString())
                            };
                        return RenderTemplateItem(DecorationBasedRenderTemplateItemType.ValueSpanningDecoration, spanArgs);
                    }")
                .WithLink("https://github.com/jeffwindsor/FluentAlerts", "Link to Github Source Code")
                .WithHorizontalRule()
                .WithObjectBlock(Mother.CreateNestedTestObject(3))
                .WithHorizontalRule()
                .WithExceptionBlock(Mother.CreateNestedException(2))
                .WithHorizontalRule()
                .WithOrderedList("one", "two", "three", 25, Guid.NewGuid(), "whatever you wants");

            SerializeToConsole(alert);
        }

        ////  ** IMPORTANT SIDE NOTE **
        ////  Something i have been hiding from you up to now is the ToAlert() function, which converts an
        ////  IAlertBuilder (the thing with the fluent interface we have been using) to an IAlert
        ////  (which is what we want).  In fact when you see the Alerts.Create(...).With(...) the 
        ////  result of this fluent building process is the IAlertBuilder, not the IAlert.   
        ////  But as you may have noticed the RenderToConsole() extension methods hides this fact.
        ////  Not to worry I am not leading down the bad practice path, but simulating what will happen for you in the 
        ////  library.  Most of the down stream objects that can take an IAlert will take an IAlertBUilder as well
        ////  and will just convert it for you by calling ToAlert.
        ////  So be explicit an convert the builder yourself or lean on the library and just get it for free.

        [Test]
        public void CompositionForMoreFlare()
        {
            // Here is a simple example of how you can compose alerts and objects.
            // The builder allows you to add alerts (or alert builders) to alerts, allowing
            // you to create complex trees of composed information.
            var alerts = Get<IFluentAlerts>();
            var alert = alerts.Document(
                "We are going to start with a document, but you would start with Table if you prefer")
                .With(alerts.TextBlock("A more complex text block")
                    .WithItalic(" with some italic")
                    .WithNewLine()
                    .WithStrong("And some stronger text")
                    .WithUnderscore(" And some underscore")
                    .WithText(" and some normal text"))
                .WithHorizontalRule()
                .With(alerts.Table("My Inner Table")
                    .WithHeaderRow("one", "two", "three")
                    .WithRow(1, alerts.ExceptionBlock(Mother.CreateNestedException(2)), alerts.Document("Compose what you will"))
                    .WithEmphasizedRow("one", "last cell will be spanned if not all cells are given"));
            
            SerializeToConsole(alert);
        }
        
        public void SerializeToConsole(IAlertable convertable)
        {
            SerializeToConsole(convertable.ToAlert());
        }
        public void SerializeToConsole(Alert alert)
        {
            Trace.WriteLine(JsonConvert.SerializeObject(alert));
            //var serializer = Get<IFluentAlertSerializer>();
            //Trace.WriteLine(serializer.Serialize(alert));
        }
    }
}
