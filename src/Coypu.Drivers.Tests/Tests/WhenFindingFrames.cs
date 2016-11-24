using NUnit.Framework;

namespace Coypu.Drivers.Tests.Tests
{
    [TestFixture]
    internal class WhenFindingFrames
    {
        private IDriver _driver;

        [OneTimeSetUp]
        public void Given() => _driver = TestDriver.Instance(@"html\frameset.htm");

        [Test]
        public void Finds_by_header_text()
        {
            Assert.That(DriverHelpers.Frame(_driver, "I am frame one").Name, Is.EqualTo("frame1"));
            Assert.That(DriverHelpers.Frame(_driver, "I am frame two").Name, Is.EqualTo("frame2"));
        }

        [Test]
        public void Finds_by_name()
        {
            Assert.That(DriverHelpers.Frame(_driver, "frame1").Name, Is.EqualTo("frame1"));
            Assert.That(DriverHelpers.Frame(_driver, "frame2").Name, Is.EqualTo("frame2"));
        }

        [Test]
        public void Finds_by_id()
        {
            Assert.That(DriverHelpers.Frame(_driver, "frame1id").Name, Is.EqualTo("frame1"));
            Assert.That(DriverHelpers.Frame(_driver, "frame2id").Name, Is.EqualTo("frame2"));
        }
    }
}