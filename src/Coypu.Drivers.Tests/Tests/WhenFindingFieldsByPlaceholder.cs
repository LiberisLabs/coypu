using NUnit.Framework;

namespace Coypu.Drivers.Tests.Tests
{
    [TestFixture]
    internal class WhenFindingFieldsByPlaceholder
    {
        private Driver _driver;

        [OneTimeSetUp]
        public void Given() => _driver = TestDriver.Instance();

        [Test]
        public void Finds_text_field_by_placeholder()
        {
            Assert.That(DriverHelpers.Field(_driver, "text input field with a placeholder").Id, Is.EqualTo("textInputFieldWithPlaceholder"));
            Assert.That(DriverHelpers.Field(_driver, "textarea field with a placeholder").Id, Is.EqualTo("textareaFieldWithPlaceholder"));
        }
    }
}