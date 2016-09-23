using NUnit.Framework;

namespace Coypu.Drivers.Tests
{
    public class When_clicking : DriverSpecs
    {
        [Test]
        public void Clicks_the_underlying_element()
        {
            var element = Button("clickMeTest");
            Assert.That(element.Value, Is.EqualTo("Click me"));

            Driver.Click(element);

            Assert.That(Button("clickMeTest").Value, Is.EqualTo("Click me - clicked"));
        }
    }
}