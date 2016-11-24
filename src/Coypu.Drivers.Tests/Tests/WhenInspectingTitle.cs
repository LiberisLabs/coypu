using NUnit.Framework;

namespace Coypu.Drivers.Tests.Tests
{
    [TestFixture]
    internal class WhenInspectingTitle
    {
        [OneTimeSetUp]
        public void Given() => DriverSpecs.DoSetUp();

        [Test]
        public void Gets_the_current_page_title()
        {
            Assert.That("Coypu interaction tests page", Is.EqualTo(DriverSpecs.Driver.Title(DriverSpecs.Root)));
        }
    }
}
