using NUnit.Framework;

namespace Coypu.Drivers.Tests.Tests
{
    [TestFixture]
    internal class WhenInspectingDialogText
    {
        private Driver _driver;

        [SetUp]
        public void Given() => _driver = DriverSpecs.Instance();

        [TearDown]
        public void Kill() => _driver.Dispose();

        [Test]
        public void Finds_exact_text_in_alert()
        {
            _driver.Click(DriverSpecs.Link(_driver, "Trigger an alert"));
            Assert.That(_driver.HasDialog("You have triggered an alert and this is the text.", DriverSpecs.Root), Is.True);
            Assert.That(_driver.HasDialog("You have triggered a different alert and this is the different text.", DriverSpecs.Root), Is.False);
        }

        [Test]
        public void Finds_exact_text_in_confirm()
        {
            _driver.Click(DriverSpecs.Link(_driver, "Trigger a confirm"));
            Assert.That(_driver.HasDialog("You have triggered a confirm and this is the text.", DriverSpecs.Root), Is.True);
            Assert.That(_driver.HasDialog("You have triggered a different confirm and this is the different text.", DriverSpecs.Root), Is.False);
        }
    }
}