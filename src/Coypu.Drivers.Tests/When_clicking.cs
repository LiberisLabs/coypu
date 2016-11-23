using NSpec;
using NUnit.Framework;

namespace Coypu.Drivers.Tests
{
    public class WhenClicking : DriverSpecs
    {
        [Test]
        public void Clicks_the_underlying_element()
        {
            var element = Button("clickMeTest");
            element.Value.should_be("Click me");

            Driver.Click(element);

            Button("clickMeTest").Value.should_be("Click me - clicked");
        }
    }
}