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
            Assert.That(element.Value, Is.EqualTo("Click me"));

            DriverSpecs.Driver.Click(element);

            Assert.That(DriverSpecs.Button("clickMeTest").Value, Is.EqualTo("Click me - clicked"));
        }
    }
}