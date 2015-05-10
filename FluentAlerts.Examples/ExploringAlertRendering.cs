//using Ninject;
//using NUnit.Framework;

//namespace FluentAlerts.Examples
//{
//    [TestFixture]
//    public class ExploringAlertRendering : BaseExample
//    {
//        private readonly IFluentAlerts _alerts;
//        public ExploringAlertRendering()
//        {
//            _alerts = Container.Get<IFluentAlerts>();
//        }

//        [Test]
//        public void A_HowAutoLayoutWorksInTheDefaultRender()
//        { 

//            var alert = _alerts.Create("Lets Play with layout to see what the render does.")
//                               .WithEmphasized("Putting the largest number of values in a section sets the number of columns, this is dynamic so this can happen anywhere in the alert")
//                               .With("One", "Two", "Three", "Four", "Five")
//                               .WithEmphasized("Any other value list with fewer columns will span the last column by default")
//                               .With("One", "Two", "Three", "Four")
//                               .With("One", "Two", "Three")
//                               .With("One", "Two")
//                               .With("One")
//                               .With("One", "Two")
//                               .With("One", "Two", "Three")
//                               .With("One", "Two", "Three", "Four")
//                               .WithEmphasized("This works on any style, but this is Emphasized")
//                               .WithEmphasized("One", "Two", "Three", "Four")
//                               .WithEmphasized("One", "Two", "Three")
//                               .WithEmphasized("One", "Two")
//                               .WithEmphasized("One")
//                               .WithSeperator()
//                               .WithEmphasized("Even across separators")
//                               .With("One", "Two", "Three", "Four")
//                               .With("One", "Two", "Three")
//                               .With("One", "Two")
//                               .With("One");

//            alert.RenderToConsole();
//        }

//    }
//}
