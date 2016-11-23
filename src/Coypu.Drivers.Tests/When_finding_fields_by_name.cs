using NSpec;
using NUnit.Framework;

namespace Coypu.Drivers.Tests
{
    internal class WhenFindingFieldsByName : DriverSpecs {

        [Test]
        public void Finds_text_input()
        {
            Field("containerLabeledTextInputFieldName").Value.should_be("text input field two val");
        }

        [Test]
        public void Finds_textarea()
        {
            Field("containerLabeledTextareaFieldName").Value.should_be("textarea field two val");
        }

        [Test]
        public void Finds_select()
        {
            Field("containerLabeledSelectFieldName").Id.should_be("containerLabeledSelectFieldId");
        }

        [Test]
        public void Finds_checkbox()
        {
            Field("containerLabeledCheckboxFieldName").Value.should_be("checkbox field two val");
        }

        [Test]
        public void Does_NOT_find_radio_button()
        {
            Assert.Throws<MissingHtmlException>(() => Button("containerLabeledRadioFieldName"));
        }

        [Test]
        public void Finds_password_input()
        {
            Field("containerLabeledPasswordFieldName").Id.should_be("containerLabeledPasswordFieldId");
        }

        [Test]
        public void Finds_file_input()
        {
            Field("containerLabeledFileFieldName").Id.should_be("containerLabeledFileFieldId");
        }
    }
}