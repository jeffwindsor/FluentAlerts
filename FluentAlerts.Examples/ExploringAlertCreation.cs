using System;
using Newtonsoft.Json;
using NUnit.Framework;

namespace FluentAlerts.Examples
{
    [TestFixture]
    public class ExploringAlertCreation:BaseExample
    {        
 
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
