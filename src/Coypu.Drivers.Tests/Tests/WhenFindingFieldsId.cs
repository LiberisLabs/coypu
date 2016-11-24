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
            Assert.That("text input field two val", Is.EqualTo(DriverSpecs.Field("containerLabeledTextInputFieldId").Value));
        }

        [Test]
        public void Finds_email_field()
        {
            Assert.That("email input field two val", Is.EqualTo(DriverSpecs.Field("containerLabeledEmailInputFieldId").Value));
        }

        [Test]
        public void Finds_tel_field()
        {
            Assert.That("0123456789", Is.EqualTo(DriverSpecs.Field("containerLabeledTelInputFieldId").Value));
        }

        [Test]
        public void Finds_number_field()
        {
            Assert.That("42", Is.EqualTo(DriverSpecs.Field("containerLabeledNumberInputFieldId").Value));
        }

        [Test]
        public void Finds_datetime_field()
        {
            Assert.That("2012-01-02T03:04:05Z", Is.EqualTo(DriverSpecs.Field("containerLabeledDatetimeInputFieldId").Value));
        }

        [Test]
        public void Finds_datetime_local_field()
        {
            Assert.That("2012-01-02T03:04:05", Is.EqualTo(DriverSpecs.Field("containerLabeledDatetimeLocalInputFieldId").Value));
        }

        [Test]
        public void Finds_date_field()
        {
            Assert.That("2012-01-02", Is.EqualTo(DriverSpecs.Field("containerLabeledDateInputFieldId").Value));
        }

        [Test]
        public void Finds_url_field()
        {
            Assert.That("http://www.example.com", Is.EqualTo(DriverSpecs.Field("containerLabeledUrlInputFieldId").Value));
        }
        [Test]
        public void Finds_color_field()
        {
            Assert.That("#ff0000", Is.EqualTo(DriverSpecs.Field("containerLabeledColorInputFieldId").Value));
        }

        [Test]
        public void Finds_textarea()
        {
            Assert.That("textarea field two val", Is.EqualTo(DriverSpecs.Field("containerLabeledTextareaFieldId").Value));
        }

        [Test]
        public void Finds_select()
        {
            Assert.That("containerLabeledSelectFieldName", Is.EqualTo(DriverSpecs.Field("containerLabeledSelectFieldId").Name));
        }

        [Test]
        public void Finds_checkbox()
        {
            Assert.That("checkbox field two val", Is.EqualTo(DriverSpecs.Field("containerLabeledCheckboxFieldId").Value));
        }

        [Test]
        public void Finds_radio()
        {
            Assert.That("radio field two val", Is.EqualTo(DriverSpecs.Field("containerLabeledRadioFieldId").Value));
        }

        [Test]
        public void Finds_password()
        {
            Assert.That("containerLabeledPasswordFieldName", Is.EqualTo(DriverSpecs.Field("containerLabeledPasswordFieldId").Name));
        }

        [Test]
        public void Finds_file()
        {
            Assert.That("containerLabeledFileFieldName", Is.EqualTo(DriverSpecs.Field("containerLabeledFileFieldId").Name));
        }
    }
}