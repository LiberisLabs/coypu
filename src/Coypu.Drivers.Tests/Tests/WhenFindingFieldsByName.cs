using NUnit.Framework;

namespace Coypu.Drivers.Tests.Tests
{
    [TestFixture]
    internal class WhenFindingFieldsByName
    {
        [OneTimeSetUp]
        public void Given() => DriverSpecs.VisitTestPage();

        [Test]
        public void Finds_text_input()
        {
            Assert.That(DriverHelpers.Field(DriverSpecs.Driver, "containerLabeledTextInputFieldName").Value, Is.EqualTo("text input field two val"));
        }

        [Test]
        public void Finds_textarea()
        {
            Assert.That(DriverHelpers.Field(DriverSpecs.Driver, "containerLabeledTextareaFieldName").Value, Is.EqualTo("textarea field two val"));
        }

        [Test]
        public void Finds_select()
        {
            Assert.That(DriverHelpers.Field(DriverSpecs.Driver, "containerLabeledSelectFieldName").Id, Is.EqualTo("containerLabeledSelectFieldId"));
        }

        [Test]
        public void Finds_checkbox()
        {
            Assert.That(DriverHelpers.Field(DriverSpecs.Driver, "containerLabeledCheckboxFieldName").Value, Is.EqualTo("checkbox field two val"));
        }

        [Test]
        public void Does_NOT_find_radio_button()
        {
            Assert.Throws<MissingHtmlException>(() => DriverHelpers.Button(DriverSpecs.Driver, "containerLabeledRadioFieldName"));
        }

        [Test]
        public void Finds_password_input()
        {
            Assert.That(DriverHelpers.Field(DriverSpecs.Driver, "containerLabeledPasswordFieldName").Id, Is.EqualTo("containerLabeledPasswordFieldId"));
        }

        [Test]
        public void Finds_file_input()
        {
            Assert.That(DriverHelpers.Field(DriverSpecs.Driver, "containerLabeledFileFieldName").Id, Is.EqualTo("containerLabeledFileFieldId"));
        }
    }
}