using NUnit.Framework;

namespace Coypu.Drivers.Tests.Tests
{
    [TestFixture]
    public class WhenHovering
    {
        [OneTimeSetUp]
        public void Given() => DriverSpecs.VisitTestPage();

        [Test]
        public void Mouses_over_the_underlying_element()
        {
            var element = DriverHelpers.Id(DriverSpecs.Driver, "hoverOnMeTest");
            Assert.That(element.Text, Is.EqualTo("Hover on me"));
            DriverSpecs.Driver.Hover(element);

            Assert.That(DriverHelpers.Id(DriverSpecs.Driver, "hoverOnMeTest").Text, Is.EqualTo("Hover on me - hovered"));
        }
    }
}