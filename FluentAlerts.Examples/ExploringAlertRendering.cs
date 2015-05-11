using System.Diagnostics;
using Newtonsoft.Json;
using Ninject;
using NUnit.Framework;

namespace FluentAlerts.Examples
{

    /*
     * Lets play around with serializing some alerts, change up what you like,
     * but for the best experience pull the rendered alert text from the your 
     * test runners output window and view it in a browser or on some site like 
     * cssdesk.com
     */

    [TestFixture]
    public class ExploringAlertRendering : BaseExample
    {
        //[Test]
        //public void AlertsAreJustObjectTreesSoYouCanUseYourOwnObjectsTurnAnObjectIntoAnAlert()
        //{
        //    // Note how the objects public properties and fields are enumerated in
        //    // the result, with nested classes being enumerated to given depth.  And
        //    // the format of the each types string representation.
        //    // This is driven by the Transformer and Formatter classes used
        //    // as well as the type info and formatter rules.
        //    // ** We will get into modifying each one of those later
        //    var alerts = Get<IFluentAlerts>();
        //    var alert = alerts.From(Mother.CreateNestedTestObject(2));

        //    SerializeToConsole(alert);
        //}

        //[Test]
        //public void TurnAnExceptionIntoAnAlert()
        //{
        //    // An exception, or in this a class derived from exception is just another
        //    // object like above, but since we will be using these a lot and I
        //    // needed an example of how to specify transformation by type, here it is.
        //    // Note how the properties are limited to a select list an ordered in 
        //    // a specific way (as apposed to alpha in the example above).
        //    var alerts = Get<IFluentAlerts>();
        //    try
        //    {
        //        throw Mother.CreateNestedException(4);
        //    }
        //    catch (Exception ex)
        //    {
        //        var alert = alerts.From(ex);
        //        SerializeToConsole(alert);
        //    }
        //}

        //TODO: show alerts are just object trees so no need for anything but your own object
        //TODO: Show 
        [Test]
        public void A_HowTableAutoLayoutWorksInTheDefaultRender()
        {
            var serializer = Get<IFluentAlertSerializer>();
            var alert = GetTestTableAlert();
            SendToConsole(serializer.Serialize(alert));
        }

        private object GetTestTableAlert()
        {
            var alerts = Get<IFluentAlerts>();
            return alerts.Table("Lets Play with layout to see what the render does.")
                               .WithEmphasizedRow("Putting the largest number of values in a section sets the number of columns, this is dynamic so this can happen anywhere in the alert")
                               .WithRow("One", "Two", "Three", "Four", "Five")
                               .WithEmphasizedRow("Any other value list with fewer columns will span the last column by default")
                               .WithRow("One", "Two", "Three", "Four")
                               .WithRow("One", "Two", "Three")
                               .WithRow("One", "Two")
                               .WithRow("One")
                               .WithRow("One", "Two")
                               .WithRow("One", "Two", "Three")
                               .WithRow("One", "Two", "Three", "Four")
                               .WithEmphasizedRow("This works on any style, but this is Emphasized")
                               .WithEmphasizedRow("One", "Two", "Three", "Four")
                               .WithEmphasizedRow("One", "Two", "Three")
                               .WithEmphasizedRow("One", "Two")
                               .WithEmphasizedRow("One")
                               .ToAlert();
        }
    }
}
