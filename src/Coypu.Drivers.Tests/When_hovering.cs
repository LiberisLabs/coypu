using NUnit.Framework;

namespace Coypu.Drivers.Tests
{
    public class When_hovering : DriverSpecs
    {
        [Test]
        public void Mouses_over_the_underlying_element()

        {
            var element = Id("hoverOnMeTest");
            Assert.That(element.Text, Is.EqualTo("Hover on me"));
            Driver.Hover(element);

            Assert.That(Id("hoverOnMeTest").Text, Is.EqualTo("Hover on me - hovered"));
        }
    }
}