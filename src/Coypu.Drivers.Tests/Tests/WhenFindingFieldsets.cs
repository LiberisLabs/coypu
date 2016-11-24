using NUnit.Framework;

namespace Coypu.Drivers.Tests.Tests
{
    [TestFixture]
    internal class WhenFindingFieldsets
    {
        [OneTimeSetUp]
        public void Given() => DriverSpecs.VisitTestPage();

        [Test]
        public void Finds_by_legend_text()
        {
            Assert.That(DriverHelpers.Fieldset(DriverSpecs.Driver, "Scope 1").Id, Is.EqualTo("fieldsetScope1"));
            Assert.That(DriverHelpers.Fieldset(DriverSpecs.Driver, "Scope 2").Id, Is.EqualTo("fieldsetScope2"));
        }

        [Test]
        public void Finds_by_id()
        {
            Assert.That(DriverHelpers.Fieldset(DriverSpecs.Driver, "Scope 1").Native, Is.EqualTo(DriverHelpers.Fieldset(DriverSpecs.Driver, "fieldsetScope1").Native));
            Assert.That(DriverHelpers.Fieldset(DriverSpecs.Driver, "Scope 2").Native, Is.EqualTo(DriverHelpers.Fieldset(DriverSpecs.Driver, "fieldsetScope2").Native));
        }

        [Test]
        public void Finds_only_Fieldsets()
        {
            Assert.Throws<MissingHtmlException>(() => DriverHelpers.Fieldset(DriverSpecs.Driver, "scope1TextInputFieldId"));
            Assert.Throws<MissingHtmlException>(() => DriverHelpers.Fieldset(DriverSpecs.Driver, "sectionOne"));
        }
    }
}