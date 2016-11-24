using NUnit.Framework;

namespace Coypu.Drivers.Tests.Tests
{
    [TestFixture]
    internal class WhenFindingFieldsets
    {
        private Driver _driver;

        [OneTimeSetUp]
        public void Given() => _driver = TestDriver.Instance();

        [Test]
        public void Finds_by_legend_text()
        {
            Assert.That(DriverHelpers.Fieldset(_driver, "Scope 1").Id, Is.EqualTo("fieldsetScope1"));
            Assert.That(DriverHelpers.Fieldset(_driver, "Scope 2").Id, Is.EqualTo("fieldsetScope2"));
        }

        [Test]
        public void Finds_by_id()
        {
            Assert.That(DriverHelpers.Fieldset(_driver, "Scope 1").Native, Is.EqualTo(DriverHelpers.Fieldset(_driver, "fieldsetScope1").Native));
            Assert.That(DriverHelpers.Fieldset(_driver, "Scope 2").Native, Is.EqualTo(DriverHelpers.Fieldset(_driver, "fieldsetScope2").Native));
        }

        [Test]
        public void Finds_only_Fieldsets()
        {
            Assert.Throws<MissingHtmlException>(() => DriverHelpers.Fieldset(_driver, "scope1TextInputFieldId"));
            Assert.Throws<MissingHtmlException>(() => DriverHelpers.Fieldset(_driver, "sectionOne"));
        }
    }
}