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
            Assert.That("forLabeledRadioFieldName", Is.EqualTo(DriverSpecs.Field("radio field one val").Name));
            Assert.That("containerLabeledRadioFieldName", Is.EqualTo(DriverSpecs.Field("radio field two val").Name));
        }

        [Test]
        public void Finds_checkbox_by_value() {
            Assert.That("checkboxByValueOneFieldName", Is.EqualTo(DriverSpecs.Field("checkbox one val").Name));
            Assert.That("checkboxByValueTwoFieldName", Is.EqualTo(DriverSpecs.Field("checkbox two val").Name));
        }
    }
}
