using NUnit.Framework;

namespace Coypu.Drivers.Tests
{
    internal class When_finding_iframes : DriverSpecs
    {
        [Test]
        public void Finds_by_header_text()
        {
            Assert.That(Frame("I am iframe one").Id, Is.EqualTo("iframe1"));
            Assert.That(Frame("I am iframe two").Id, Is.EqualTo("iframe2"));
        }

        [Test]
        public void Finds_by_id()
        {
            Assert.That(Frame("iframe1").Id, Is.EqualTo("iframe1"));
            Assert.That(Frame("iframe2").Id, Is.EqualTo("iframe2"));
        }

        [Test]
        public void Finds_by_title()
        {
            Assert.That(Frame("iframe one title").Id, Is.EqualTo("iframe1"));
            Assert.That(Frame("iframe two title").Id, Is.EqualTo("iframe2"));
        }
    }
}