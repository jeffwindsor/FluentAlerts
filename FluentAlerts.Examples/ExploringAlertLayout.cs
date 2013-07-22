using NUnit.Framework;

namespace FluentAlerts.Examples
{
    [TestFixture]
    public class ExploringAlertLayout
    {
        //Simluate IOC or other creation methods in your code
        private readonly IAlertBuilderFactory _alerts = ObjectFactory.CreateDefaultAlertBuilderFactory();

        [Test]
        public void A_PlayingWithAutoLayout()
        {
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


        //[Test]
        //public void CreateASimpleTableLikeAlert()
        //{
        //    var alert = _alerts.Create("Those pant's dont match that shirt, my man.")
        //                       .WithRow("Shirt Color", "Yellow")
        //                       .WithRow("Pants Color", "Purple Plaid")
        //                       .ToAlert();

        //    alert.RenderToConsole();
        //}

        //[Test]
        //public void CreateASimpleTableLikeAlertWithALittleMoreFlair()
        //{
        //    var alert = _alerts.Create("Those pant's dont match that shirt, my man.")
        //                       .WithRow("Shirt Color", "Yellow")
        //                       .WithRow("Pants Color", "Purple Plaid")
        //                       .WithEmphasizedRow("Top 3 Reasons" )
        //                       .WithRow("One", "Yellow and Purple please.")
        //                       .WithRow("Two", "Did'nt work for smoochie, and it won't work for you.")
        //                       .WithRow("Three", "everyone knows your jeans should be green.")
        //                       .ToAlert();

        //    alert.RenderToConsole();
        //}
    }
}
