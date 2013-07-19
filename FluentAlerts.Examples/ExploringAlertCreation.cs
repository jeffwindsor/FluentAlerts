using System;
using NUnit.Framework;

namespace FluentAlerts.Examples
{
    [TestFixture]
    public class ExploringAlertCreation
    {
        //Simluate IOC or other creation methods in your code
        private readonly IAlertBuilderFactory _alerts = ObjectFactory.CreateDefaultAlertBuilderFactory();
        
        /*
         * Lets play around with creating some alerts, change up what you like,
         * but for the best experience pull the rendered alert text from the your 
         * test runners output window and view it in a browser or on some site like 
         * cssdesk.com
         * 
         * I will cover the follwoing topics in other test files:
         *  > how to set the default rendering template
         *  > how to set a specific template during rendering
         *  > alerts, objects, and how to control the transformation process
         *  > advanced transformation topics like setting type and format rules by object type and depth (currently slated for future dev)
         */
        
        [Test]
        public void A_CreateALikeAlert()
        {
            // Note the title in the create statement (this is optional)
            // and the layout of the rendered table in thml
            // ** You may have seen the create function has an overload focusing on exceptions
            //    there is an example of that below
            var alert = _alerts.Create("Those pant's dont match that shirt, my man.")
                               .With("Shirt Color", "Yellow")
                               .With("Pants Color", "Purple Plaid");

            alert.RenderToConsole();
        }

        [Test]
        public void B_CreateASimpleAlertWithALittleMoreFlair()
        {
            // Note the change in the emphasized sections rendering
            // ** We will talk later about how to add and modify templates 
            // so you can get your own formatting.
            var alert = _alerts.Create("Those pant's dont match that shirt, my man.")
                               .With("Shirt Color", "Yellow")
                               .With("Pants Color", "Purple Plaid")
                               .WithEmphasized("Top 3 Reasons")
                               .With("One", "Yellow and Purple please.")
                               .With("Two", "Did'nt work for smoochie, and it won't work for you.")
                               .With("Three", "everyone knows your jeans should be green.");

            alert.RenderToConsole();
        }

        [Test]
        public void C_PlayingWithSeperators()
        {
            var alert = _alerts.Create("Those pant's dont match that shirt, my man.")
                               .With("Shirt Color", "Yellow")
                               .With("Pants Color", "Purple Plaid")
                               .WithSeperator()
                               .WithEmphasized("Top 3 Reasons")
                               .With("One", "Yellow and Purple please.")
                               .With("Two", "Did'nt work for smoochie, and it won't work for you.")
                               .WithSeperator()
                               .With("Three", "everyone knows your jeans should be green.");

            alert.RenderToConsole();
        }
         
        [Test]
        public void D_PlayingWithAutoLayout()
        {
            var alert = _alerts.Create("Lets Play with layout to see what the render does.")
                               .WithEmphasized("Putting the largest number of values in a section sets the number of columns, this dynamic so this can happen anywhere in the alert")
                               .With("One","Two","Three","Four","Five")
                               .WithEmphasized("Any other value list with fewer columns will span the last column by default")
                               .With("One", "Two", "Three", "Four")
                               .With("One", "Two", "Three")
                               .With("One", "Two")
                               .With("One")
                               .WithEmphasized("This works on any style")
                               .WithEmphasized("One", "Two", "Three", "Four")
                               .WithEmphasized("One", "Two", "Three")
                               .WithEmphasized("One", "Two")
                               .WithEmphasized("One")
                               .WithSeperator()
                               .WithEmphasized("Even across seperators, make a new alert if yuo wnat different behavior")
                               .With("One", "Two", "Three", "Four")
                               .With("One", "Two", "Three")
                               .With("One", "Two")
                               .With("One");

            alert.RenderToConsole();
        }

        [Test]
        public void E_TurnAnObjectIntoAnAlert()
        {
            try
            {
                var testObject = ObjectFactory.CreateNestedTestClass(3);
                var alert = _alerts.Create(testObject);
                alert.RenderToConsole();

            }
            catch (Exception ex)
            {
                var s = ex;
            }
        }

        [Test]
        public void F_EmbeddingStuffOrFunWithAlertTrees()
        {
            var manualAlert = _alerts.Create("This is an inner manually created alert");
            var testObject = ObjectFactory.CreateNestedTestClass(3);
            var objectBasedAlert = _alerts.Create(testObject);
            var alert = _alerts.Create("Lets Play with layout to see what the render does.")
                               .WithEmphasized("Putting the largest number of values in a section sets the number of columns, this dynamic so this can happen anywhere in the alert")
                               .With("One", "Two", "Three", "Four", "Five")
                               .WithEmphasized("Any other value list with fewer columns will span the last column by default")
                               .With("One", "Two", "Three", "Four")
                               .With("One", "Two", "Three")
                               .With("One", "Two")
                               .With("One")
                               .WithEmphasized("This works on any style")
                               .WithEmphasized("One", "Two", "Three", "Four")
                               .WithEmphasized("One", "Two", "Three")
                               .WithEmphasized("One", "Two")
                               .WithEmphasized("One")
                               .WithSeperator()
                               .WithEmphasized("Even across seperators, make a new alert if yuo wnat different behavior")
                               .With("One", "Two", "Three", "Four")
                               .With("One", "Two", "Three")
                               .With("One", "Two")
                               .With("One");

            alert.RenderToConsole();
        }

        //  Something i have been hiding from you , the ToAlert() function, which converts an
        //  IAlertBuilder to an IAlert (which is what we want).  In fact when you see the
        //  _alerts.Create(...).With(...) the result of this fluent building process 
        //  is the IAlertBuilder, not the IAlert.  
        //  But as you may have noticed the RenderToConsole() extension methods hide this fact
        //  Not to worry I am not leading down the bad practice path, but simulating what will happen for you in the 
        //  library.  Most of the down stream objects that can take an IAlert will take an IAlertBUilder as well
        //  and will just convert it for you by calling ToAlert.
        //  So be explicit an convert the builder yourself or lean on the library and jsut get it for free.

    }
}
