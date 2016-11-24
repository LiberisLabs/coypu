using NUnit.Framework;

namespace Coypu.Drivers.Tests.Tests
{
    [TestFixture]
    public class WhenHovering
    {
        [OneTimeSetUp]
        public void Given() => DriverSpecs.DoSetUp();

        [Test]
        public void Mouses_over_the_underlying_element()
        {
            var element = DriverSpecs.Id("hoverOnMeTest");
            Assert.That(element.Text, Is.EqualTo("Hover on me"));
            DriverSpecs.Driver.Hover(element);

            Assert.That(DriverSpecs.Id("hoverOnMeTest").Text, Is.EqualTo("Hover on me - hovered"));
        }
    }
}