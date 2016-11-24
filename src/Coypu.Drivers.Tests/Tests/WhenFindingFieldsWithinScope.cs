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
        public void Given() => DriverSpecs.DoSetUp();

        [SetUp]
        public void SetUpScope()
        {
            IDisambiguationStrategy disambiguationStrategy = new ThrowsWhenMissingButNoDisambiguationStrategy();
            _scope1 = new BrowserWindow(DriverSpecs.DefaultSessionConfiguration, new IdFinder(DriverSpecs.Driver, "scope1", DriverSpecs.Root, DriverSpecs.DefaultOptions), DriverSpecs.Driver, null, null, null, disambiguationStrategy);
            _scope2 = new BrowserWindow(DriverSpecs.DefaultSessionConfiguration, new IdFinder(DriverSpecs.Driver, "scope2", DriverSpecs.Root, DriverSpecs.DefaultOptions), DriverSpecs.Driver, null, null, null, disambiguationStrategy);
        }

        [Test]
        public void Finds_text_input_by_for()
        {
            Assert.That("scope1TextInputFieldId", Is.EqualTo(DriverSpecs.Field("scoped text input field linked by for", _scope1).Id));
            Assert.That("scope2TextInputFieldId", Is.EqualTo(DriverSpecs.Field("scoped text input field linked by for", _scope2).Id));
        }

        [Test]
        public void Finds_text_input_in_container_label()
        {
            Assert.That("scope1ContainerLabeledTextInputFieldId", Is.EqualTo(DriverSpecs.Field("scoped text input field in a label container", _scope1).Id));
            Assert.That("scope2ContainerLabeledTextInputFieldId", Is.EqualTo(DriverSpecs.Field("scoped text input field in a label container", _scope2).Id));
        }

        [Test]
        public void Finds_text_input_by_placeholder()
        {
            Assert.That("scope1TextInputFieldWithPlaceholder", Is.EqualTo(DriverSpecs.Field("scoped text input field with a placeholder", _scope1).Id));
            Assert.That("scope2TextInputFieldWithPlaceholder", Is.EqualTo(DriverSpecs.Field("scoped text input field with a placeholder", _scope2).Id));
        }

        [Test]
        public void Finds_text_input_by_name()
        {
            Assert.That("scope1ContainerLabeledTextInputFieldId", Is.EqualTo(DriverSpecs.Field("text input field in a label container", _scope1).Id));
            Assert.That("scope2ContainerLabeledTextInputFieldId", Is.EqualTo(DriverSpecs.Field("text input field in a label container", _scope2).Id));
        }

        [Test]
        public void Finds_radio_button_by_value()
        {
            Assert.That("scope1RadioFieldId", Is.EqualTo(DriverSpecs.Field("scoped radio field one val", _scope1).Id));
            Assert.That("scope2RadioFieldId", Is.EqualTo(DriverSpecs.Field("scoped radio field one val", _scope2).Id));
        }

        [Test]
        public void Finds_not_find_text_input_by_id_outside_scope()
        {
            Assert.Throws<MissingHtmlException>(() => DriverSpecs.Field("containerLabeledTextInputFieldId", _scope1));
            Assert.Throws<MissingHtmlException>(() => DriverSpecs.Field("containerLabeledTextInputFieldId", _scope2));
        }
    }

}