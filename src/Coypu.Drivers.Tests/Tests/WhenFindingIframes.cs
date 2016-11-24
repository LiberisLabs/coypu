using NUnit.Framework;

namespace Coypu.Drivers.Tests.Tests
{
    [TestFixture]
    internal class WhenFindingIframes
    {
        private IDriver _driver;

        [OneTimeSetUp]
        public void Given() => _driver = TestDriver.Instance();

        [Test]
        public void Finds_by_header_text()
        {
            Assert.That(DriverHelpers.Frame(_driver, "I am iframe one").Id, Is.EqualTo("iframe1"));
            Assert.That(DriverHelpers.Frame(_driver, "I am iframe two").Id, Is.EqualTo("iframe2"));
        }

        [Test]
        public void Finds_by_id()
        {
            Assert.That(DriverHelpers.Frame(_driver, "iframe1").Id, Is.EqualTo("iframe1"));
            Assert.That(DriverHelpers.Frame(_driver, "iframe2").Id, Is.EqualTo("iframe2"));
        }

        [Test]
        public void Finds_by_title()
        {
            Assert.That(DriverHelpers.Frame(_driver, "iframe one title").Id, Is.EqualTo("iframe1"));
            Assert.That(DriverHelpers.Frame(_driver, "iframe two title").Id, Is.EqualTo("iframe2"));
        }
    }
}