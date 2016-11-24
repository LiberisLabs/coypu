using NUnit.Framework;

namespace Coypu.Drivers.Tests.Tests
{
    [TestFixture]
    internal class WhenFindingFieldsByName
    {
        private Driver _driver;

        [OneTimeSetUp]
        public void Given() => _driver = TestDriver.Instance();

        [Test]
        public void Finds_text_input()
        {
            Assert.That(DriverHelpers.Field(_driver, "containerLabeledTextInputFieldName").Value, Is.EqualTo("text input field two val"));
        }

        [Test]
        public void Finds_textarea()
        {
            Assert.That(DriverHelpers.Field(_driver, "containerLabeledTextareaFieldName").Value, Is.EqualTo("textarea field two val"));
        }

        [Test]
        public void Finds_select()
        {
            Assert.That(DriverHelpers.Field(_driver, "containerLabeledSelectFieldName").Id, Is.EqualTo("containerLabeledSelectFieldId"));
        }

        [Test]
        public void Finds_checkbox()
        {
            Assert.That(DriverHelpers.Field(_driver, "containerLabeledCheckboxFieldName").Value, Is.EqualTo("checkbox field two val"));
        }

        [Test]
        public void Does_NOT_find_radio_button()
        {
            Assert.Throws<MissingHtmlException>(() => DriverHelpers.Button(_driver, "containerLabeledRadioFieldName"));
        }

        [Test]
        public void Finds_password_input()
        {
            Assert.That(DriverHelpers.Field(_driver, "containerLabeledPasswordFieldName").Id, Is.EqualTo("containerLabeledPasswordFieldId"));
        }

        [Test]
        public void Finds_file_input()
        {
            Assert.That(DriverHelpers.Field(_driver, "containerLabeledFileFieldName").Id, Is.EqualTo("containerLabeledFileFieldId"));
        }
    }
}