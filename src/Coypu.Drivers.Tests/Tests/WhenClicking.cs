using NUnit.Framework;

namespace Coypu.Drivers.Tests.Tests
{
    [TestFixture]
    public class WhenClicking
    {
        private Driver _driver;

        [OneTimeSetUp]
        public void Given() => _driver = TestDriver.Instance();

        [Test]
        public void Clicks_the_underlying_element()
        {
            var element = DriverHelpers.Button(_driver, "clickMeTest");
            Assert.That(element.Value, Is.EqualTo("Click me"));

            _driver.Click(element);

            Assert.That(DriverHelpers.Button(_driver, "clickMeTest").Value, Is.EqualTo("Click me - clicked"));
        }
    }
}