using System;
using System.Diagnostics;
using FluentAlerts.Builders;
using Newtonsoft.Json;
using NUnit.Framework;

namespace FluentAlerts.Examples
{
    [TestFixture]
    public class ExploringAlertCreation:BaseExample
    {        
        /*
         * Lets play around with creating some Alerts.  
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
        public void CreateSimpleTableLikeAlert()
        {
            var alert = Alerts.Table("Those pant's don't match that shirt, my man.")
                .WithRow("Shirt Color", "Yellow")
                .WithRow("Pants Color", "Purple Plaid")
                .WithEmphasizedRow("Top 3 Reasons")
                .WithRow("One", "Yellow and Purple please.")
                .WithRow("Two", "Didn't work for Smoochie, and it won't work for you.")
                .WithRow("Three", "everyone knows your jeans should be green.");

            SerializeToConsoleAsJson(alert.ToTable());
        }
        
        [Test]
        public void CreateSimpleDocumentLikeAlert()
        {
            var alert = Alerts.Document("Create a simple document like alert")
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
                .With(Mother.CreateTestObject(3))
                .WithHorizontalRule()
                .With(Mother.CreateNestedException(2))
                .WithHorizontalRule()
                .WithOrderedList("one", "two", "three", 25, Guid.NewGuid(), "whatever you wants");

            SerializeToConsoleAsJson(alert.ToDocument());
        }

        [Test]
        public void ComposeAnythingForMoreFlare()
        {
            // Here is a simple example of how you can compose Alerts and objects.
            // The builder allows you to add Alerts (or alert builders) to Alerts, allowing
            // you to create complex trees of composed information.
            var alert = Alerts.Document(
                "We are going to start with a document, but you would start with Table if you prefer")
                .With(Alerts.TextBlock("A more complex text block")
                    .WithItalic(" with some italic")
                    .WithNewLine()
                    .WithBold("And some stronger text")
                    .WithUnderscore(" And some underscore")
                    .WithNormal(" and some normal text"))
                .WithHorizontalRule()
                .WithHeader("Note you can but any object into this tree")
                .With(Mother.CreateTestObject(3))
                .With(Guid.NewGuid())
                .With(DateTime.UtcNow)
                .WithHorizontalRule()
                .With(Alerts.Table("My Inner Table")
                    .WithHeaderRow("one", "two", "three")
                    .WithRow(1, Mother.CreateNestedException(2), Alerts.Document("Note this row will take any type, Compose what you will"))
                    .WithEmphasizedRow("one", "last cell will be spanned if not all cells are given"));
            
            SerializeToConsoleAsJson(alert.ToDocument());
        }

        private static void SerializeToConsoleAsJson(object o)
        {
            SendToConsole(JsonConvert.SerializeObject(o));
        }
    }
}
