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
            Assert.That("Hover on me", Is.EqualTo(element.Text));
            DriverSpecs.Driver.Hover(element);

            Assert.That("Hover on me - hovered", Is.EqualTo(DriverSpecs.Id("hoverOnMeTest").Text));
        }
    }
}