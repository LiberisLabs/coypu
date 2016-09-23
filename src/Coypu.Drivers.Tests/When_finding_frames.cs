using NUnit.Framework;

namespace Coypu.Drivers.Tests
{
    internal class When_finding_frames : DriverSpecs
    {
        protected override string TestPage => @"html\frameset.htm";

        [Test]
        public void Finds_by_header_text()
        {
            Assert.That(Frame("I am frame one").Name, Is.EqualTo("frame1"));
            Assert.That(Frame("I am frame two").Name, Is.EqualTo("frame2"));
        }

        [Test]
        public void Finds_by_name()
        {
            Assert.That(Frame("frame1").Name, Is.EqualTo("frame1"));
            Assert.That(Frame("frame2").Name, Is.EqualTo("frame2"));
        }

        [Test]
        public void Finds_by_id()
        {
            Assert.That(Frame("frame1id").Name, Is.EqualTo("frame1"));
            Assert.That(Frame("frame2id").Name, Is.EqualTo("frame2"));
        }
    }
}