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
            Assert.That(DriverSpecs.Fieldset("Scope 1").Id, Is.EqualTo("fieldsetScope1"));
            Assert.That(DriverSpecs.Fieldset("Scope 2").Id, Is.EqualTo("fieldsetScope2"));
        }

        [Test]
        public void Finds_by_id()
        {
            Assert.That(DriverSpecs.Fieldset("Scope 1").Native, Is.EqualTo(DriverSpecs.Fieldset("fieldsetScope1").Native));
            Assert.That(DriverSpecs.Fieldset("Scope 2").Native, Is.EqualTo(DriverSpecs.Fieldset("fieldsetScope2").Native));
        }

        [Test]
        public void Finds_only_Fieldsets()
        {
            Assert.Throws<MissingHtmlException>(() => DriverSpecs.Fieldset("scope1TextInputFieldId"));
            Assert.Throws<MissingHtmlException>(() => DriverSpecs.Fieldset("sectionOne"));
        }
    }
}