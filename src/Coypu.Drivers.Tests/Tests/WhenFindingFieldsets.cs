using NUnit.Framework;

namespace Coypu.Drivers.Tests.Tests
{
    [TestFixture]
    internal class WhenFindingFieldsets
    {
        [OneTimeSetUp]
        public void Given() => DriverSpecs.DoSetUp();

        [Test]
        public void Finds_by_legend_text()
        {
            Assert.That("fieldsetScope1", Is.EqualTo(DriverSpecs.Fieldset("Scope 1").Id));
            Assert.That("fieldsetScope2", Is.EqualTo(DriverSpecs.Fieldset("Scope 2").Id));
        }

        [Test]
        public void Finds_by_id()
        {
            Assert.That(DriverSpecs.Fieldset("fieldsetScope1").Native, Is.EqualTo(DriverSpecs.Fieldset("Scope 1").Native));
            Assert.That(DriverSpecs.Fieldset("fieldsetScope2").Native, Is.EqualTo(DriverSpecs.Fieldset("Scope 2").Native));
        }

        [Test]
        public void Finds_only_Fieldsets()
        {
            Assert.Throws<MissingHtmlException>(() => DriverSpecs.Fieldset("scope1TextInputFieldId"));
            Assert.Throws<MissingHtmlException>(() => DriverSpecs.Fieldset("sectionOne"));
        }
    }
}