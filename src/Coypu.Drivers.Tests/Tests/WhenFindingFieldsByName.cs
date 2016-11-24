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
            Assert.That("text input field two val", Is.EqualTo(DriverSpecs.Field("containerLabeledTextInputFieldName").Value));
        }

        [Test]
        public void Finds_textarea()
        {
            Assert.That("textarea field two val", Is.EqualTo(DriverSpecs.Field("containerLabeledTextareaFieldName").Value));
        }

        [Test]
        public void Finds_select()
        {
            Assert.That("containerLabeledSelectFieldId", Is.EqualTo(DriverSpecs.Field("containerLabeledSelectFieldName").Id));
        }

        [Test]
        public void Finds_checkbox()
        {
            Assert.That("checkbox field two val", Is.EqualTo(DriverSpecs.Field("containerLabeledCheckboxFieldName").Value));
        }

        [Test]
        public void Does_NOT_find_radio_button()
        {
            Assert.Throws<MissingHtmlException>(() => DriverSpecs.Button("containerLabeledRadioFieldName"));
        }

        [Test]
        public void Finds_password_input()
        {
            Assert.That("containerLabeledPasswordFieldId", Is.EqualTo(DriverSpecs.Field("containerLabeledPasswordFieldName").Id));
        }

        [Test]
        public void Finds_file_input()
        {
            Assert.That("containerLabeledFileFieldId", Is.EqualTo(DriverSpecs.Field("containerLabeledFileFieldName").Id));
        }
    }
}