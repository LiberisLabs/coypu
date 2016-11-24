using NUnit.Framework;

namespace Coypu.Drivers.Tests.Tests
{
    [TestFixture]
    internal class WhenFindingIframes
    {
        [OneTimeSetUp]
        public void Given() => DriverSpecs.DoSetUp();

        [Test]
        public void Finds_by_header_text()
        {
            Assert.That(DriverSpecs.Frame("I am iframe one").Id, Is.EqualTo("iframe1"));
            Assert.That(DriverSpecs.Frame("I am iframe two").Id, Is.EqualTo("iframe2"));
        }

        [Test]
        public void Finds_by_id()
        {
            Assert.That(DriverSpecs.Frame("iframe1").Id, Is.EqualTo("iframe1"));
            Assert.That(DriverSpecs.Frame("iframe2").Id, Is.EqualTo("iframe2"));
        }

        [Test]
        public void Finds_by_title()
        {
            Assert.That(DriverSpecs.Frame("iframe one title").Id, Is.EqualTo("iframe1"));
            Assert.That(DriverSpecs.Frame("iframe two title").Id, Is.EqualTo("iframe2"));
        }
    }
}