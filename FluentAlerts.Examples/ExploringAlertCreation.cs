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
        public void CreateASimpleTableLikeAlert()
        {
            // Note the title in the create statement (this is optional)
            // and the layout of the rendered table in thml
            // ** You may have seen the create function has an overload focusing on exceptions
            //    there is an example of that below
            var alert = _alerts.Create("Those pant's dont match that shirt, my man.")
                               .With("Shirt Color", "Yellow")
                               .With("Pants Color", "Purple Plaid")
                               .ToAlert();

            alert.RenderToConsole();
        }

        [Test]
        public void CreateASimpleTableLikeAlertWithALittleMoreFlair()
        {
            // Note the change in the emphasized row's rendering
            var alert = _alerts.Create("Those pant's dont match that shirt, my man.")
                               .With("Shirt Color", "Yellow")
                               .With("Pants Color", "Purple Plaid")
                               .WithEmphasized("Top 3 Reasons")
                               .With("One", "Yellow and Purple please.")
                               .With("Two", "Did'nt work for smoochie, and it won't work for you.")
                               .With("Three", "everyone knows your jeans should be green.")
                               .ToAlert();

            alert.RenderToConsole();
        }

        [Test]
        public void PlayingWithSeperators()
        {
             
            var alert = _alerts.Create("Those pant's dont match that shirt, my man.")
                               .With("Shirt Color", "Yellow")
                               .With("Pants Color", "Purple Plaid")
                               .WithSeperator()
                               .WithEmphasized("Top 3 Reasons")
                               .With("One", "Yellow and Purple please.")
                               .With("Two", "Did'nt work for smoochie, and it won't work for you.")
                               .WithSeperator()
                               .With("Three", "everyone knows your jeans should be green.")
                               .ToAlert();

            alert.RenderToConsole();
        }

        //urls

        //
    }
}
