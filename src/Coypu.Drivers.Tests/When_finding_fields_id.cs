using NUnit.Framework;

namespace Coypu.Drivers.Tests
{
    internal class When_finding_fields_id : DriverSpecs
    {
        [Test]
        public void Finds_field()
        {
            Assert.That(Field("containerLabeledTextInputFieldId").Value, Is.EqualTo("text input field two val"));
        }

        [Test]
        public void Finds_email_field()
        {
            Assert.That(Field("containerLabeledEmailInputFieldId").Value, Is.EqualTo("email input field two val"));
        }

        [Test]
        public void Finds_tel_field()
        {
            Assert.That(Field("containerLabeledTelInputFieldId").Value, Is.EqualTo("0123456789"));
        }

        [Test]
        public void Finds_number_field()
        {
            Assert.That(Field("containerLabeledNumberInputFieldId").Value, Is.EqualTo("42"));
        }

        [Test]
        public void Finds_datetime_field()
        {
            Assert.That(Field("containerLabeledDatetimeInputFieldId").Value, Is.EqualTo("2012-01-02T03:04:05Z"));
        }

        [Test]
        public void Finds_datetime_local_field()
        {
            Assert.That(Field("containerLabeledDatetimeLocalInputFieldId").Value, Is.EqualTo("2012-01-02T03:04:05"));
        }

        [Test]
        public void Finds_date_field()
        {
            Assert.That(Field("containerLabeledDateInputFieldId").Value, Is.EqualTo("2012-01-02"));
        }

        [Test]
        public void Finds_url_field()
        {
            Assert.That(Field("containerLabeledUrlInputFieldId").Value, Is.EqualTo("http://www.example.com"));
        }
        [Test]
        public void Finds_color_field()
        {
            Assert.That(Field("containerLabeledColorInputFieldId").Value, Is.EqualTo("#ff0000"));
        }

        [Test]
        public void Finds_textarea()
        {
            Assert.That(Field("containerLabeledTextareaFieldId").Value, Is.EqualTo("textarea field two val"));
        }

        [Test]
        public void Finds_select()
        {
            Assert.That(Field("containerLabeledSelectFieldId").Name, Is.EqualTo("containerLabeledSelectFieldName"));
        }

        [Test]
        public void Finds_checkbox()
        {
            Assert.That(Field("containerLabeledCheckboxFieldId").Value, Is.EqualTo("checkbox field two val"));
        }

        [Test]
        public void Finds_radio()
        {
            Assert.That(Field("containerLabeledRadioFieldId").Value, Is.EqualTo("radio field two val"));
        }

        [Test]
        public void Finds_password()
        {
            Assert.That(Field("containerLabeledPasswordFieldId").Name, Is.EqualTo("containerLabeledPasswordFieldName"));
        }

        [Test]
        public void Finds_file()
        {
            Assert.That(Field("containerLabeledFileFieldId").Name, Is.EqualTo("containerLabeledFileFieldName"));
        }
    }
}