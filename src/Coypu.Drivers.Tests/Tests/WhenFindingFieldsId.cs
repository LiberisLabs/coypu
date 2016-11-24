using NUnit.Framework;

namespace Coypu.Drivers.Tests.Tests
{
    [TestFixture]
    internal class WhenFindingFieldsId
    {
        [OneTimeSetUp]
        public void Given() => DriverSpecs.VisitTestPage();

        [Test]
        public void Finds_field()
        {
            Assert.That(DriverHelpers.Field(DriverSpecs.Driver, "containerLabeledTextInputFieldId").Value, Is.EqualTo("text input field two val"));
        }

        [Test]
        public void Finds_email_field()
        {
            Assert.That(DriverHelpers.Field(DriverSpecs.Driver, "containerLabeledEmailInputFieldId").Value, Is.EqualTo("email input field two val"));
        }

        [Test]
        public void Finds_tel_field()
        {
            Assert.That(DriverHelpers.Field(DriverSpecs.Driver, "containerLabeledTelInputFieldId").Value, Is.EqualTo("0123456789"));
        }

        [Test]
        public void Finds_number_field()
        {
            Assert.That(DriverHelpers.Field(DriverSpecs.Driver, "containerLabeledNumberInputFieldId").Value, Is.EqualTo("42"));
        }

        [Test]
        public void Finds_datetime_field()
        {
            Assert.That(DriverHelpers.Field(DriverSpecs.Driver, "containerLabeledDatetimeInputFieldId").Value, Is.EqualTo("2012-01-02T03:04:05Z"));
        }

        [Test]
        public void Finds_datetime_local_field()
        {
            Assert.That(DriverHelpers.Field(DriverSpecs.Driver, "containerLabeledDatetimeLocalInputFieldId").Value, Is.EqualTo("2012-01-02T03:04:05"));
        }

        [Test]
        public void Finds_date_field()
        {
            Assert.That(DriverHelpers.Field(DriverSpecs.Driver, "containerLabeledDateInputFieldId").Value, Is.EqualTo("2012-01-02"));
        }

        [Test]
        public void Finds_url_field()
        {
            Assert.That(DriverHelpers.Field(DriverSpecs.Driver, "containerLabeledUrlInputFieldId").Value, Is.EqualTo("http://www.example.com"));
        }
        [Test]
        public void Finds_color_field()
        {
            Assert.That(DriverHelpers.Field(DriverSpecs.Driver, "containerLabeledColorInputFieldId").Value, Is.EqualTo("#ff0000"));
        }

        [Test]
        public void Finds_textarea()
        {
            Assert.That(DriverHelpers.Field(DriverSpecs.Driver, "containerLabeledTextareaFieldId").Value, Is.EqualTo("textarea field two val"));
        }

        [Test]
        public void Finds_select()
        {
            Assert.That(DriverHelpers.Field(DriverSpecs.Driver, "containerLabeledSelectFieldId").Name, Is.EqualTo("containerLabeledSelectFieldName"));
        }

        [Test]
        public void Finds_checkbox()
        {
            Assert.That(DriverHelpers.Field(DriverSpecs.Driver, "containerLabeledCheckboxFieldId").Value, Is.EqualTo("checkbox field two val"));
        }

        [Test]
        public void Finds_radio()
        {
            Assert.That(DriverHelpers.Field(DriverSpecs.Driver, "containerLabeledRadioFieldId").Value, Is.EqualTo("radio field two val"));
        }

        [Test]
        public void Finds_password()
        {
            Assert.That(DriverHelpers.Field(DriverSpecs.Driver, "containerLabeledPasswordFieldId").Name, Is.EqualTo("containerLabeledPasswordFieldName"));
        }

        [Test]
        public void Finds_file()
        {
            Assert.That(DriverHelpers.Field(DriverSpecs.Driver, "containerLabeledFileFieldId").Name, Is.EqualTo("containerLabeledFileFieldName"));
        }
    }
}