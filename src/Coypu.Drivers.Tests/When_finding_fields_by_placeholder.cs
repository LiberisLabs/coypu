using NUnit.Framework;

namespace Coypu.Drivers.Tests
{
    internal class When_finding_fields_by_placeholder : DriverSpecs
    {
        [Test]
        public void Finds_text_field_by_placeholder()
        {
            Assert.That(Field("text input field with a placeholder").Id, Is.EqualTo("textInputFieldWithPlaceholder"));
            Assert.That(Field("textarea field with a placeholder").Id, Is.EqualTo("textareaFieldWithPlaceholder"));
        }
    }
}