using NUnit.Framework;

namespace Coypu.Drivers.Tests.Tests
{
    [TestFixture]
    internal class WhenInspectingDialogText
    {
        private IDriver _driver;

        [SetUp]
        public void Given() => _driver = TestDriver.Instance();

        [TearDown]
        public void Kill() => _driver.Dispose();

        [Test]
        public void Finds_exact_text_in_alert()
        {
            _driver.Click(DriverHelpers.Link(_driver, "Trigger an alert"));
            Assert.That(_driver.HasDialog("You have triggered an alert and this is the text.", DriverHelpers.WindowScope(_driver)), Is.True);
            Assert.That(_driver.HasDialog("You have triggered a different alert and this is the different text.", DriverHelpers.WindowScope(_driver)), Is.False);
        }

        [Test]
        public void Finds_exact_text_in_confirm()
        {
            _driver.Click(DriverHelpers.Link(_driver, "Trigger a confirm"));
            Assert.That(_driver.HasDialog("You have triggered a confirm and this is the text.", DriverHelpers.WindowScope(_driver)), Is.True);
            Assert.That(_driver.HasDialog("You have triggered a different confirm and this is the different text.", DriverHelpers.WindowScope(_driver)), Is.False);
        }
    }
}