using NUnit.Framework;

namespace Coypu.Drivers.Tests.Tests
{
    [TestFixture]
    public class WhenClicking
    {
        [OneTimeSetUp]
        public void Given() => DriverSpecs.DoSetUp();

        [Test]
        public void Clicks_the_underlying_element()
        {
            var element = DriverSpecs.Button("clickMeTest");
            Assert.That("Click me", Is.EqualTo(element.Value));

            DriverSpecs.Driver.Click(element);

            Assert.That("Click me - clicked", Is.EqualTo(DriverSpecs.Button("clickMeTest").Value));
        }
    }
}