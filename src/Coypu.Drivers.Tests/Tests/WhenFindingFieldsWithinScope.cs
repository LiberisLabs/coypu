using Coypu.Finders;
using NUnit.Framework;

namespace Coypu.Drivers.Tests.Tests
{
    [TestFixture]
    internal class WhenFindingFieldsWithinScope
    {
        private DriverScope _scope1;
        private DriverScope _scope2;

        [OneTimeSetUp]
        public void Given() => DriverSpecs.VisitTestPage();

        [SetUp]
        public void SetUpScope()
        {
            IDisambiguationStrategy disambiguationStrategy = new ThrowsWhenMissingButNoDisambiguationStrategy();
            _scope1 = new BrowserWindow(Default.SessionConfiguration, new IdFinder(DriverSpecs.Driver, "scope1", DriverSpecs.Root, Default.Options), DriverSpecs.Driver, null, null, null, disambiguationStrategy);
            _scope2 = new BrowserWindow(Default.SessionConfiguration, new IdFinder(DriverSpecs.Driver, "scope2", DriverSpecs.Root, Default.Options), DriverSpecs.Driver, null, null, null, disambiguationStrategy);
        }

        [Test]
        public void Finds_text_input_by_for()
        {
            Assert.That(DriverHelpers.Field(DriverSpecs.Driver, "scoped text input field linked by for", _scope1).Id, Is.EqualTo("scope1TextInputFieldId"));
            Assert.That(DriverHelpers.Field(DriverSpecs.Driver, "scoped text input field linked by for", _scope2).Id, Is.EqualTo("scope2TextInputFieldId"));
        }

        [Test]
        public void Finds_text_input_in_container_label()
        {
            Assert.That(DriverHelpers.Field(DriverSpecs.Driver, "scoped text input field in a label container", _scope1).Id, Is.EqualTo("scope1ContainerLabeledTextInputFieldId"));
            Assert.That(DriverHelpers.Field(DriverSpecs.Driver, "scoped text input field in a label container", _scope2).Id, Is.EqualTo("scope2ContainerLabeledTextInputFieldId"));
        }

        [Test]
        public void Finds_text_input_by_placeholder()
        {
            Assert.That(DriverHelpers.Field(DriverSpecs.Driver, "scoped text input field with a placeholder", _scope1).Id, Is.EqualTo("scope1TextInputFieldWithPlaceholder"));
            Assert.That(DriverHelpers.Field(DriverSpecs.Driver, "scoped text input field with a placeholder", _scope2).Id, Is.EqualTo("scope2TextInputFieldWithPlaceholder"));
        }

        [Test]
        public void Finds_text_input_by_name()
        {
            Assert.That(DriverHelpers.Field(DriverSpecs.Driver, "text input field in a label container", _scope1).Id, Is.EqualTo("scope1ContainerLabeledTextInputFieldId"));
            Assert.That(DriverHelpers.Field(DriverSpecs.Driver, "text input field in a label container", _scope2).Id, Is.EqualTo("scope2ContainerLabeledTextInputFieldId"));
        }

        [Test]
        public void Finds_radio_button_by_value()
        {
            Assert.That(DriverHelpers.Field(DriverSpecs.Driver, "scoped radio field one val", _scope1).Id, Is.EqualTo("scope1RadioFieldId"));
            Assert.That(DriverHelpers.Field(DriverSpecs.Driver, "scoped radio field one val", _scope2).Id, Is.EqualTo("scope2RadioFieldId"));
        }

        [Test]
        public void Finds_not_find_text_input_by_id_outside_scope()
        {
            Assert.Throws<MissingHtmlException>(() => DriverHelpers.Field(DriverSpecs.Driver, "containerLabeledTextInputFieldId", _scope1));
            Assert.Throws<MissingHtmlException>(() => DriverHelpers.Field(DriverSpecs.Driver, "containerLabeledTextInputFieldId", _scope2));
        }
    }

}