using NUnit.Framework;

namespace Coypu.Drivers.Tests.Tests
{
    [TestFixture]
    internal class WhenChoosing
    {
        private Driver _driver;

        [SetUp]
        public void Given() => _driver = DriverSpecs.Instance();

        [TearDown]
        public void Kill() => _driver.Dispose();

        [Test]
        public void Chooses_radio_button_from_list()
        {
            var radioButton1 = DriverSpecs.Field(_driver, "chooseRadio1");
            Assert.That(radioButton1.Selected, Is.False);

            // Choose 1
            _driver.Choose(radioButton1);

            var radioButton2 = DriverSpecs.Field(_driver, "chooseRadio2");
            Assert.That(radioButton2.Selected, Is.False);

            // Choose 2
            _driver.Choose(radioButton2);

            // New choice is now selected
            radioButton2 = DriverSpecs.Field(_driver, "chooseRadio2");
            Assert.That(radioButton2.Selected, Is.True);

            // Originally selected is no longer selected
            radioButton1 = DriverSpecs.Field(_driver, "chooseRadio1");
            Assert.That(radioButton1.Selected, Is.False);
        }

        [Test]
        public void Fires_onclick_event()
        {
            var radio = DriverSpecs.Field(_driver, "chooseRadio2");
            Assert.That(radio.Value, Is.EqualTo("Radio buttons - 2nd value"));

            _driver.Choose(radio);

            Assert.That(DriverSpecs.Field(_driver, "chooseRadio2", DriverSpecs.Root).Value, Is.EqualTo("Radio buttons - 2nd value - clicked"));
        }
    }
}