using NUnit.Framework;

namespace Coypu.Drivers.Tests
{
    internal class When_finding_fields_by_value : DriverSpecs
    {
        [Test]
        public void Finds_radio_button_by_value()
        {
            Assert.That(Field("radio field one val").Name, Is.EqualTo("forLabeledRadioFieldName"));
            Assert.That(Field("radio field two val").Name, Is.EqualTo("containerLabeledRadioFieldName"));
        }

        [Test]
        public void Finds_checkbox_by_value()
        {
            Assert.That(Field("checkbox one val").Name, Is.EqualTo("checkboxByValueOneFieldName"));
            Assert.That(Field("checkbox two val").Name, Is.EqualTo("checkboxByValueTwoFieldName"));
        }

    }
}
