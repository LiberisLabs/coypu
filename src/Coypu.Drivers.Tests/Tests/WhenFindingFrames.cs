using NUnit.Framework;

namespace Coypu.Drivers.Tests.Tests
{
    [TestFixture]
    internal class WhenFindingFrames
    {
        [OneTimeSetUp]
        public void Given() => DriverSpecs.VisitTestPage(@"html\frameset.htm");

        [Test]
        public void Finds_by_header_text()
        {
            Assert.That(DriverHelpers.Frame(DriverSpecs.Driver, "I am frame one").Name, Is.EqualTo("frame1"));
            Assert.That(DriverHelpers.Frame(DriverSpecs.Driver, "I am frame two").Name, Is.EqualTo("frame2"));
        }

        [Test]
        public void Finds_by_name()
        {
            Assert.That(DriverHelpers.Frame(DriverSpecs.Driver, "frame1").Name, Is.EqualTo("frame1"));
            Assert.That(DriverHelpers.Frame(DriverSpecs.Driver, "frame2").Name, Is.EqualTo("frame2"));
        }

        [Test]
        public void Finds_by_id()
        {
            Assert.That(DriverHelpers.Frame(DriverSpecs.Driver, "frame1id").Name, Is.EqualTo("frame1"));
            Assert.That(DriverHelpers.Frame(DriverSpecs.Driver, "frame2id").Name, Is.EqualTo("frame2"));
        }
    }
}