using NUnit.Framework;

namespace FluentAlerts.Examples
{
    [TestFixture]
    public class AlertCreation
    {
        //Simluate IOC or other creation methods in your code
        private readonly IAlertBuilderFactory _alerts = ObjectFactory.CreateDefaultAlertBuilderFactory();

        [Test]
        public void CreateASimpleTableLikeAlert()
        {
            var alert = _alerts.Create("Those pant's dont match that shirt, my man.")
                               .WithRow("Shirt Color", "Yellow")
                               .WithRow("Pants Color", "Purple Plaid")
                               .ToAlert();

            alert.RenderToConsole();
        }

        [Test]
        public void CreateASimpleTableLikeAlertWithALittleMoreFlair()
        {
            var alert = _alerts.Create("Those pant's dont match that shirt, my man.")
                               .WithRow("Shirt Color", "Yellow")
                               .WithRow("Pants Color", "Purple Plaid")
                               .WithEmphasizedRow("Top 3 Reasons" )
                               .WithRow("One", "Yellow and Purple please.")
                               .WithRow("Two", "Did'nt work for smoochie, and it won't work for you.")
                               .WithRow("Three", "everyone knows your jeans should be green.")
                               .ToAlert();

            alert.RenderToConsole();
        }
    }
}
