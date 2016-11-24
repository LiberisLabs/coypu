using NUnit.Framework;

namespace Coypu.Drivers.Tests.Tests
{
    [TestFixture]
    public class WhenClicking
    {
        [OneTimeSetUp]
        public void Given() => DriverSpecs.VisitTestPage();

        [Test]
        public void Clicks_the_underlying_element()
        {
            var element = DriverHelpers.Button(DriverSpecs.Driver, "clickMeTest");
            Assert.That(element.Value, Is.EqualTo("Click me"));

            DriverSpecs.Driver.Click(element);

            Assert.That(DriverHelpers.Button(DriverSpecs.Driver, "clickMeTest").Value, Is.EqualTo("Click me - clicked"));
        }
    }
}