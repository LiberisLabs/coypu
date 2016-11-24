using NUnit.Framework;

namespace Coypu.Drivers.Tests.Tests
{
    [TestFixture]
    internal class WhenFindingFieldsId
    {
        private Driver _driver;

        [OneTimeSetUp]
        public void Given() => _driver = TestDriver.Instance();

        [Test]
        public void Finds_field()
        {
            Assert.That(DriverHelpers.Field(_driver, "containerLabeledTextInputFieldId").Value, Is.EqualTo("text input field two val"));
        }

        [Test]
        public void Finds_email_field()
        {
            Assert.That(DriverHelpers.Field(_driver, "containerLabeledEmailInputFieldId").Value, Is.EqualTo("email input field two val"));
        }

        [Test]
        public void Finds_tel_field()
        {
            Assert.That(DriverHelpers.Field(_driver, "containerLabeledTelInputFieldId").Value, Is.EqualTo("0123456789"));
        }

        [Test]
        public void Finds_number_field()
        {
            Assert.That(DriverHelpers.Field(_driver, "containerLabeledNumberInputFieldId").Value, Is.EqualTo("42"));
        }

        [Test]
        public void Finds_datetime_field()
        {
            Assert.That(DriverHelpers.Field(_driver, "containerLabeledDatetimeInputFieldId").Value, Is.EqualTo("2012-01-02T03:04:05Z"));
        }

        [Test]
        public void Finds_datetime_local_field()
        {
            Assert.That(DriverHelpers.Field(_driver, "containerLabeledDatetimeLocalInputFieldId").Value, Is.EqualTo("2012-01-02T03:04:05"));
        }

        [Test]
        public void Finds_date_field()
        {
            Assert.That(DriverHelpers.Field(_driver, "containerLabeledDateInputFieldId").Value, Is.EqualTo("2012-01-02"));
        }

        [Test]
        public void Finds_url_field()
        {
            Assert.That(DriverHelpers.Field(_driver, "containerLabeledUrlInputFieldId").Value, Is.EqualTo("http://www.example.com"));
        }
        [Test]
        public void Finds_color_field()
        {
            Assert.That(DriverHelpers.Field(_driver, "containerLabeledColorInputFieldId").Value, Is.EqualTo("#ff0000"));
        }

        [Test]
        public void Finds_textarea()
        {
            Assert.That(DriverHelpers.Field(_driver, "containerLabeledTextareaFieldId").Value, Is.EqualTo("textarea field two val"));
        }

        [Test]
        public void Finds_select()
        {
            Assert.That(DriverHelpers.Field(_driver, "containerLabeledSelectFieldId").Name, Is.EqualTo("containerLabeledSelectFieldName"));
        }

        [Test]
        public void Finds_checkbox()
        {
            Assert.That(DriverHelpers.Field(_driver, "containerLabeledCheckboxFieldId").Value, Is.EqualTo("checkbox field two val"));
        }

        [Test]
        public void Finds_radio()
        {
            Assert.That(DriverHelpers.Field(_driver, "containerLabeledRadioFieldId").Value, Is.EqualTo("radio field two val"));
        }

        [Test]
        public void Finds_password()
        {
            Assert.That(DriverHelpers.Field(_driver, "containerLabeledPasswordFieldId").Name, Is.EqualTo("containerLabeledPasswordFieldName"));
        }

        [Test]
        public void Finds_file()
        {
            Assert.That(DriverHelpers.Field(_driver, "containerLabeledFileFieldId").Name, Is.EqualTo("containerLabeledFileFieldName"));
        }
    }
}