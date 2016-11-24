using NUnit.Framework;

namespace Coypu.Drivers.Tests.Tests
{
    [TestFixture]
    internal class WhenFindingFrames
    {
        [OneTimeSetUp]
        public void Given() => DriverSpecs.DoSetUp(@"html\frameset.htm");

        [Test]
        public void Finds_by_header_text()
        {
            Assert.That(DriverSpecs.Frame("I am frame one").Name, Is.EqualTo("frame1"));
            Assert.That(DriverSpecs.Frame("I am frame two").Name, Is.EqualTo("frame2"));
        }

        [Test]
        public void Finds_by_name()
        {
            Assert.That(DriverSpecs.Frame("frame1").Name, Is.EqualTo("frame1"));
            Assert.That(DriverSpecs.Frame("frame2").Name, Is.EqualTo("frame2"));
        }

        [Test]
        public void Finds_by_id()
        {
            Assert.That(DriverSpecs.Frame("frame1id").Name, Is.EqualTo("frame1"));
            Assert.That(DriverSpecs.Frame("frame2id").Name, Is.EqualTo("frame2"));
        }
    }
}