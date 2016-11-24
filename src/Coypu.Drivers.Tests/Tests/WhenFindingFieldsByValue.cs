using NUnit.Framework;

namespace Coypu.Drivers.Tests.Tests
{
    [TestFixture]
    internal class WhenFindingFieldsByValue
    {
        [OneTimeSetUp]
        public void Given() => DriverSpecs.VisitTestPage();

        [Test]
        public void Finds_radio_button_by_value() {
            Assert.That(DriverHelpers.Field(DriverSpecs.Driver, "radio field one val").Name, Is.EqualTo("forLabeledRadioFieldName"));
            Assert.That(DriverHelpers.Field(DriverSpecs.Driver, "radio field two val").Name, Is.EqualTo("containerLabeledRadioFieldName"));
        }

        [Test]
        public void Finds_checkbox_by_value() {
            Assert.That(DriverHelpers.Field(DriverSpecs.Driver, "checkbox one val").Name, Is.EqualTo("checkboxByValueOneFieldName"));
            Assert.That(DriverHelpers.Field(DriverSpecs.Driver, "checkbox two val").Name, Is.EqualTo("checkboxByValueTwoFieldName"));
        }
    }
}
