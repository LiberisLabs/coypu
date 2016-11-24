using NUnit.Framework;

namespace Coypu.Drivers.Tests.Tests
{
    [TestFixture]
    internal class WhenFindingFieldsByValue
    {
        private Driver _driver;

        [OneTimeSetUp]
        public void Given() => _driver = TestDriver.Instance();

        [Test]
        public void Finds_radio_button_by_value() {
            Assert.That(DriverHelpers.Field(_driver, "radio field one val").Name, Is.EqualTo("forLabeledRadioFieldName"));
            Assert.That(DriverHelpers.Field(_driver, "radio field two val").Name, Is.EqualTo("containerLabeledRadioFieldName"));
        }

        [Test]
        public void Finds_checkbox_by_value() {
            Assert.That(DriverHelpers.Field(_driver, "checkbox one val").Name, Is.EqualTo("checkboxByValueOneFieldName"));
            Assert.That(DriverHelpers.Field(_driver, "checkbox two val").Name, Is.EqualTo("checkboxByValueTwoFieldName"));
        }
    }
}
