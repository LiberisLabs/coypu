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
            Assert.That("iframe1", Is.EqualTo(DriverSpecs.Frame("I am iframe one").Id));
            Assert.That("iframe2", Is.EqualTo(DriverSpecs.Frame("I am iframe two").Id));
        }

        [Test]
        public void Finds_by_id()
        {
            Assert.That("iframe1", Is.EqualTo(DriverSpecs.Frame("iframe1").Id));
            Assert.That("iframe2", Is.EqualTo(DriverSpecs.Frame("iframe2").Id));
        }

        [Test]
        public void Finds_by_title()
        {
            Assert.That("iframe1", Is.EqualTo(DriverSpecs.Frame("iframe one title").Id));
            Assert.That("iframe2", Is.EqualTo(DriverSpecs.Frame("iframe two title").Id));
        }
    }
}