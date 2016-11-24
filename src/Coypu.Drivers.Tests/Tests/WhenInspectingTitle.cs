using NUnit.Framework;

namespace Coypu.Drivers.Tests.Tests
{
    [TestFixture]
    internal class WhenInspectingTitle
    {
        private Driver _driver;
        private DriverScope _scope;

        [OneTimeSetUp]
        public void Given()
        {
            _driver = TestDriver.Instance();
            _scope = DriverHelpers.WindowScope(_driver);
        }

        [Test]
        public void Gets_the_current_page_title()
        {
            Assert.That(_driver.Title(_scope), Is.EqualTo("Coypu interaction tests page"));
        }
    }
}
