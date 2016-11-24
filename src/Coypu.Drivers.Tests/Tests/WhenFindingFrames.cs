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
            Assert.That("frame1", Is.EqualTo(DriverSpecs.Frame("I am frame one").Name));
            Assert.That("frame2", Is.EqualTo(DriverSpecs.Frame("I am frame two").Name));
        }

        [Test]
        public void Finds_by_name()
        {
            Assert.That("frame1", Is.EqualTo(DriverSpecs.Frame("frame1").Name));
            Assert.That("frame2", Is.EqualTo(DriverSpecs.Frame("frame2").Name));
        }

        [Test]
        public void Finds_by_id()
        {
            Assert.That("frame1", Is.EqualTo(DriverSpecs.Frame("frame1id").Name));
            Assert.That("frame2", Is.EqualTo(DriverSpecs.Frame("frame2id").Name));
        }
    }
}