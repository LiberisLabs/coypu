using NUnit.Framework;

namespace Coypu.Drivers.Tests.Tests
{
    [TestFixture]
    internal class WhenFindingFieldsByValue
    {
        [OneTimeSetUp]
        public void Given() => DriverSpecs.DoSetUp();

        [Test]
        public void Finds_radio_button_by_value() {
            Assert.That(DriverSpecs.Field("radio field one val").Name, Is.EqualTo("forLabeledRadioFieldName"));
            Assert.That(DriverSpecs.Field("radio field two val").Name, Is.EqualTo("containerLabeledRadioFieldName"));
        }

        [Test]
        public void Finds_checkbox_by_value() {
            Assert.That(DriverSpecs.Field("checkbox one val").Name, Is.EqualTo("checkboxByValueOneFieldName"));
            Assert.That(DriverSpecs.Field("checkbox two val").Name, Is.EqualTo("checkboxByValueTwoFieldName"));
        }
    }
}
