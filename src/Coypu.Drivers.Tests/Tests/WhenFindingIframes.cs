using NUnit.Framework;

namespace Coypu.Drivers.Tests.Tests
{
    [TestFixture]
    internal class WhenFindingIframes
    {
        [OneTimeSetUp]
        public void Given() => DriverSpecs.VisitTestPage();

        [Test]
        public void Finds_by_header_text()
        {
            Assert.That(DriverHelpers.Frame(DriverSpecs.Driver, "I am iframe one").Id, Is.EqualTo("iframe1"));
            Assert.That(DriverHelpers.Frame(DriverSpecs.Driver, "I am iframe two").Id, Is.EqualTo("iframe2"));
        }

        [Test]
        public void Finds_by_id()
        {
            Assert.That(DriverHelpers.Frame(DriverSpecs.Driver, "iframe1").Id, Is.EqualTo("iframe1"));
            Assert.That(DriverHelpers.Frame(DriverSpecs.Driver, "iframe2").Id, Is.EqualTo("iframe2"));
        }

        [Test]
        public void Finds_by_title()
        {
            Assert.That(DriverHelpers.Frame(DriverSpecs.Driver, "iframe one title").Id, Is.EqualTo("iframe1"));
            Assert.That(DriverHelpers.Frame(DriverSpecs.Driver, "iframe two title").Id, Is.EqualTo("iframe2"));
        }
    }
}