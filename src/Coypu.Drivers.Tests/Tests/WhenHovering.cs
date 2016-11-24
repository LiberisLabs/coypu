using NUnit.Framework;

namespace Coypu.Drivers.Tests.Tests
{
    [TestFixture]
    public class WhenHovering
    {
        private IDriver _driver;

        [OneTimeSetUp]
        public void Given() => _driver = TestDriver.Instance();

        [Test]
        public void Mouses_over_the_underlying_element()
        {
            var element = DriverHelpers.Id(_driver, "hoverOnMeTest");
            Assert.That(element.Text, Is.EqualTo("Hover on me"));
            _driver.Hover(element);

            Assert.That(DriverHelpers.Id(_driver, "hoverOnMeTest").Text, Is.EqualTo("Hover on me - hovered"));
        }
    }
}