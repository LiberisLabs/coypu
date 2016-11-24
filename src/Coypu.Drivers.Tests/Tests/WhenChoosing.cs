using NUnit.Framework;

namespace Coypu.Drivers.Tests.Tests
{
    [TestFixture]
    internal class WhenChoosing
    {
        [OneTimeSetUp]
        public void Given() => DriverSpecs.DoSetUp();

        [Test]
        public void Chooses_radio_button_from_list()
        {
            var radioButton1 = DriverSpecs.Field("chooseRadio1");
            Assert.That(radioButton1.Selected, Is.False);

            // Choose 1
            DriverSpecs.Driver.Choose(radioButton1);

            var radioButton2 = DriverSpecs.Field("chooseRadio2");
            Assert.That(radioButton2.Selected, Is.False);

            // Choose 2
            DriverSpecs.Driver.Choose(radioButton2);

            // New choice is now selected
            radioButton2 = DriverSpecs.Field("chooseRadio2");
            Assert.That(radioButton2.Selected, Is.True);

            // Originally selected is no longer selected
            radioButton1 = DriverSpecs.Field("chooseRadio1");
            Assert.That(radioButton1.Selected, Is.False);
        }

        [Test]
        public void Fires_onclick_event()
        {
            var radio = DriverSpecs.Field("chooseRadio2");
            Assert.That("Radio buttons - 2nd value", Is.EqualTo(radio.Value));

            DriverSpecs.Driver.Choose(radio);

            Assert.That("Radio buttons - 2nd value - clicked", Is.EqualTo(DriverSpecs.Field("chooseRadio2", DriverSpecs.Root).Value));
        }
    }
}