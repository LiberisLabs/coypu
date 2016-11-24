using NUnit.Framework;

namespace Coypu.Drivers.Tests.Tests
{
    [TestFixture]
    internal class WhenFindingFieldsByPlaceholder
    {
        [OneTimeSetUp]
        public void Given() => DriverSpecs.DoSetUp();

        [Test]
        public void Finds_text_field_by_placeholder()
        {
            Assert.That("textInputFieldWithPlaceholder", Is.EqualTo(DriverSpecs.Field("text input field with a placeholder").Id));
            Assert.That("textareaFieldWithPlaceholder", Is.EqualTo(DriverSpecs.Field("textarea field with a placeholder").Id));
        }
    }
}