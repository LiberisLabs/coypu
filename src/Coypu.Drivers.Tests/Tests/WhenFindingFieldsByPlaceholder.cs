using NUnit.Framework;

namespace Coypu.Drivers.Tests.Tests
{
    [TestFixture]
    internal class WhenFindingFieldsByPlaceholder
    {
        [OneTimeSetUp]
        public void Given() => DriverSpecs.VisitTestPage();

        [Test]
        public void Finds_text_field_by_placeholder()
        {
            Assert.That(DriverHelpers.Field(DriverSpecs.Driver, "text input field with a placeholder").Id, Is.EqualTo("textInputFieldWithPlaceholder"));
            Assert.That(DriverHelpers.Field(DriverSpecs.Driver, "textarea field with a placeholder").Id, Is.EqualTo("textareaFieldWithPlaceholder"));
        }
    }
}