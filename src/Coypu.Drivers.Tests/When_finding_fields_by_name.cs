using NUnit.Framework;

namespace Coypu.Drivers.Tests
{
    internal class When_finding_fields_by_name : DriverSpecs {

        [Test]
        public void Finds_text_input()
        {
            Assert.That(Field("containerLabeledTextInputFieldName").Value, Is.EqualTo("text input field two val"));
        }

        [Test]
        public void Finds_textarea()
        {
            Assert.That(Field("containerLabeledTextareaFieldName").Value, Is.EqualTo("textarea field two val"));
        }

        [Test]
        public void Finds_select()
        {
            Assert.That(Field("containerLabeledSelectFieldName").Id, Is.EqualTo("containerLabeledSelectFieldId"));
        }

        [Test]
        public void Finds_checkbox()
        {
            Assert.That(Field("containerLabeledCheckboxFieldName").Value, Is.EqualTo("checkbox field two val"));
        }

        [Test]
        public void Does_NOT_find_radio_button()
        {
            Assert.Throws<MissingHtmlException>(() => Button("containerLabeledRadioFieldName"));
        }

        [Test]
        public void Finds_password_input()
        {
            Assert.That(Field("containerLabeledPasswordFieldName").Id, Is.EqualTo("containerLabeledPasswordFieldId"));
        }

        [Test]
        public void Finds_file_input()
        {
            Assert.That(Field("containerLabeledFileFieldName").Id, Is.EqualTo("containerLabeledFileFieldId"));
        }

    }
}