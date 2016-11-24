using NUnit.Framework;

namespace Coypu.Drivers.Tests.Tests
{
    [TestFixture]
    internal class WhenFindingFieldsByName
    {
        [OneTimeSetUp]
        public void Given() => DriverSpecs.DoSetUp();

        [Test]
        public void Finds_text_input()
        {
            Assert.That(DriverSpecs.Field("containerLabeledTextInputFieldName").Value, Is.EqualTo("text input field two val"));
        }

        [Test]
        public void Finds_textarea()
        {
            Assert.That(DriverSpecs.Field("containerLabeledTextareaFieldName").Value, Is.EqualTo("textarea field two val"));
        }

        [Test]
        public void Finds_select()
        {
            Assert.That(DriverSpecs.Field("containerLabeledSelectFieldName").Id, Is.EqualTo("containerLabeledSelectFieldId"));
        }

        [Test]
        public void Finds_checkbox()
        {
            Assert.That(DriverSpecs.Field("containerLabeledCheckboxFieldName").Value, Is.EqualTo("checkbox field two val"));
        }

        [Test]
        public void Does_NOT_find_radio_button()
        {
            Assert.Throws<MissingHtmlException>(() => DriverSpecs.Button("containerLabeledRadioFieldName"));
        }

        [Test]
        public void Finds_password_input()
        {
            Assert.That(DriverSpecs.Field("containerLabeledPasswordFieldName").Id, Is.EqualTo("containerLabeledPasswordFieldId"));
        }

        [Test]
        public void Finds_file_input()
        {
            Assert.That(DriverSpecs.Field("containerLabeledFileFieldName").Id, Is.EqualTo("containerLabeledFileFieldId"));
        }
    }
}