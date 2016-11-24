using NUnit.Framework;

namespace Coypu.Drivers.Tests.Tests
{
    [TestFixture]
    internal class WhenInspectingTitle
    {
        [OneTimeSetUp]
        public void Given() => DriverSpecs.VisitTestPage();

        [Test]
        public void Gets_the_current_page_title()
        {
            Assert.That(DriverSpecs.Driver.Title(DriverSpecs.Root), Is.EqualTo("Coypu interaction tests page"));
        }
    }
}
