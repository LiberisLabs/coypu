using NUnit.Framework;

namespace Coypu.Drivers.Tests
{
    internal class When_choosing : DriverSpecs
    {
        [Test]
        public void Chooses_radio_button_from_list()
        {
            var radioButton1 = Field("chooseRadio1");
            Assert.That(radioButton1.Selected, Is.False);

            // Choose 1
            Driver.Choose(radioButton1);

            var radioButton2 = Field("chooseRadio2");
            Assert.That(radioButton2.Selected, Is.False);

            // Choose 2
            Driver.Choose(radioButton2);

            // New choice is now selected
            radioButton2 = Field("chooseRadio2");
            Assert.That(radioButton2.Selected, Is.True);

            // Originally selected is no longer selected
            radioButton1 = Field("chooseRadio1");
            Assert.That(radioButton1.Selected, Is.False);
        }


        [Test]
        public void Fires_onclick_event()
        {
            var radio = Field("chooseRadio2");
            Assert.That(radio.Value, Is.EqualTo("Radio buttons - 2nd value"));

            Driver.Choose(radio);

            Assert.That(Field("chooseRadio2", Root).Value, Is.EqualTo("Radio buttons - 2nd value - clicked"));
        }
    }
}