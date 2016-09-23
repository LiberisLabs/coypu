using NUnit.Framework;

namespace Coypu.Drivers.Tests
{
    internal class When_finding_fieldsets : DriverSpecs
    {
        [Test]
        public void Finds_by_legend_text()
        {
            Assert.That(Fieldset("Scope 1").Id, Is.EqualTo("fieldsetScope1"));
            Assert.That(Fieldset("Scope 2").Id, Is.EqualTo("fieldsetScope2"));
        }

        [Test]
        public void Finds_by_id()
        {
            Assert.That(Fieldset("fieldsetScope1").Native, Is.EqualTo(Fieldset("Scope 1").Native));
            Assert.That(Fieldset("fieldsetScope2").Native, Is.EqualTo(Fieldset("Scope 2").Native));
        }

        [Test]
        public void Finds_only_fieldsets()
        {
            Assert.Throws<MissingHtmlException>(() => Fieldset("scope1TextInputFieldId"));
            Assert.Throws<MissingHtmlException>(() => Fieldset("sectionOne"));
        }
    }
}