using NUnit.Framework;

namespace Coypu.Drivers.Tests.Tests
{
    [TestFixture]
    internal class WhenFindingFieldsId
    {
        [OneTimeSetUp]
        public void Given() => DriverSpecs.DoSetUp();

        [Test]
        public void Finds_field()
        {
            Assert.That(DriverSpecs.Field("containerLabeledTextInputFieldId").Value, Is.EqualTo("text input field two val"));
        }

        [Test]
        public void Finds_email_field()
        {
            Assert.That(DriverSpecs.Field("containerLabeledEmailInputFieldId").Value, Is.EqualTo("email input field two val"));
        }

        [Test]
        public void Finds_tel_field()
        {
            Assert.That(DriverSpecs.Field("containerLabeledTelInputFieldId").Value, Is.EqualTo("0123456789"));
        }

        [Test]
        public void Finds_number_field()
        {
            Assert.That(DriverSpecs.Field("containerLabeledNumberInputFieldId").Value, Is.EqualTo("42"));
        }

        [Test]
        public void Finds_datetime_field()
        {
            Assert.That(DriverSpecs.Field("containerLabeledDatetimeInputFieldId").Value, Is.EqualTo("2012-01-02T03:04:05Z"));
        }

        [Test]
        public void Finds_datetime_local_field()
        {
            Assert.That(DriverSpecs.Field("containerLabeledDatetimeLocalInputFieldId").Value, Is.EqualTo("2012-01-02T03:04:05"));
        }

        [Test]
        public void Finds_date_field()
        {
            Assert.That(DriverSpecs.Field("containerLabeledDateInputFieldId").Value, Is.EqualTo("2012-01-02"));
        }

        [Test]
        public void Finds_url_field()
        {
            Assert.That(DriverSpecs.Field("containerLabeledUrlInputFieldId").Value, Is.EqualTo("http://www.example.com"));
        }
        [Test]
        public void Finds_color_field()
        {
            Assert.That(DriverSpecs.Field("containerLabeledColorInputFieldId").Value, Is.EqualTo("#ff0000"));
        }

        [Test]
        public void Finds_textarea()
        {
            Assert.That(DriverSpecs.Field("containerLabeledTextareaFieldId").Value, Is.EqualTo("textarea field two val"));
        }

        [Test]
        public void Finds_select()
        {
            Assert.That(DriverSpecs.Field("containerLabeledSelectFieldId").Name, Is.EqualTo("containerLabeledSelectFieldName"));
        }

        [Test]
        public void Finds_checkbox()
        {
            Assert.That(DriverSpecs.Field("containerLabeledCheckboxFieldId").Value, Is.EqualTo("checkbox field two val"));
        }

        [Test]
        public void Finds_radio()
        {
            Assert.That(DriverSpecs.Field("containerLabeledRadioFieldId").Value, Is.EqualTo("radio field two val"));
        }

        [Test]
        public void Finds_password()
        {
            Assert.That(DriverSpecs.Field("containerLabeledPasswordFieldId").Name, Is.EqualTo("containerLabeledPasswordFieldName"));
        }

        [Test]
        public void Finds_file()
        {
            Assert.That(DriverSpecs.Field("containerLabeledFileFieldId").Name, Is.EqualTo("containerLabeledFileFieldName"));
        }
    }
}