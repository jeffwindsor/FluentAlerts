using System;
using NUnit.Framework;

namespace FluentAlerts.Examples
{
    [TestFixture]
    public class ExploringAlertCreation
    {
        //Simulating IoC 
        private readonly IFluentAlerts _alerts = Mother.CreateDefaultAlertBuilderFactory();

        /*
         * Lets play around with creating some alerts, change up what you like,
         * but for the best experience pull the rendered alert text from the your 
         * test runners output window and view it in a browser or on some site like 
         * cssdesk.com
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
        public void A_CreateASimpleAlert()
        {
            // Note the title in the create statement (this is optional)
            // and the layout of the rendered table in HTML
            // ** You may have seen the create function has an overload focusing on objects
            //    there is an example of that below
            var alert = _alerts.Create("Those pant's don't match that shirt, my man.")
                               .With("Shirt Color", "Yellow")
                               .With("Pants Color", "Purple Plaid");

            alert.RenderToConsole();
        }

        [Test]
        public void B_CreateASimpleAlertWithALittleMoreFlair()
        {
            // Note the change in the emphasized sections rendering
            // ** We will talk later about how to add and modify templates 
            //    so you can get your own formatting.
            var alert = _alerts.Create("Those pant's don't match that shirt, my man.")
                               .With("Shirt Color", "Yellow")
                               .With("Pants Color", "Purple Plaid")
                               .WithEmphasized("Top 3 Reasons")
                               .With("One", "Yellow and Purple please.")
                               .With("Two", "Didn't work for Smoochie, and it won't work for you.")
                               .With("Three", "everyone knows your jeans should be green.");

            alert.RenderToConsole();
        }

        [Test]
        public void C_PlayingWithSeperators()
        {
            var alert = _alerts.Create("Those pant's don't match that shirt, my man.")
                               .With("Shirt Color", "Yellow")
                               .With("Pants Color", "Purple Plaid")
                               .WithSeperator()
                               .WithEmphasized("Top 3 Reasons")
                               .With("One", "Yellow and Purple please.")
                               .With("Two", "Didn't work for Smoochie, and it won't work for you.")
                               .WithSeperator()
                               .With("Three", "everyone knows your jeans should be green.");

            alert.RenderToConsole();
        }
       
        [Test]
        public void E_TurnAnObjectIntoAnAlert()
        {
            // Note how the objects public properties and fields are enumerated in
            // the result, with nested classes being enumerated to given depth.  And
            // the format of the each types string representation.
            // This is driven by the Transformer and Formatter classes used
            // as well as the type info and formatter rules.
            // ** We will get into modifying each one of those later
            var testObject = Mother.CreateNestedTestClass(2);
            var alert = _alerts.Create(testObject);
            
            alert.RenderToConsole();
        }

        [Test]
        public void F_TurnAnExceptionIntoAnAlert()
        {
            // An exception, or in this a class derived from exception is just another
            // object like above, but since we will be using these a lot and I
            // needed an example of how to specify transformation by type, here it is.
            // Note how the properties are limited to a select list an ordered in 
            // a specific way (as apposed to alpha in the example above).
            var testException = Mother.CreateNestedException(4);
            var alert = _alerts.Create(testException);

            alert.RenderToConsole();
        }

        //  ** IMPORTANT SIDE NOTE **
        //  Something i have been hiding from you up to now is the ToAlert() function, which converts an
        //  IAlertBuilder (the thing with the fluent interface we have been using) to an IAlert
        //  (which is what we want).  In fact when you see the Alerts.Create(...).With(...) the 
        //  result of this fluent building process is the IAlertBuilder, not the IAlert.   
        //  But as you may have noticed the RenderToConsole() extension methods hides this fact.
        //  Not to worry I am not leading down the bad practice path, but simulating what will happen for you in the 
        //  library.  Most of the down stream objects that can take an IAlert will take an IAlertBUilder as well
        //  and will just convert it for you by calling ToAlert.
        //  So be explicit an convert the builder yourself or lean on the library and just get it for free.

        [Test]
        public void G_EmbeddingStuffOrFunWithComposingAlerts()
        {
            // Here is a simple example of how you can compose alerts and objects.
            // The builder allows you to add alerts (or alert builders) to alerts, allowing
            // you to create complex trees of composed information.
            var innerObject = Mother.CreateNestedTestClass(3);
            var innerALert = _alerts.Create("This is the child alert").With(innerObject);
            var alert = _alerts.Create("This is the parent alert")
                               .With("Inner Alert", innerALert)
                               .WithEmphasized("Some new section")
                               .With(innerObject);


            alert.RenderToConsole();
        }

    }
}
