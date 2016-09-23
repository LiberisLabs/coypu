using Coypu.Finders;
using NUnit.Framework;

namespace Coypu.Drivers.Tests
{
    internal class When_finding_fields_within_scope : DriverSpecs
    {
        private DriverScope scope1;
        private DriverScope scope2;

        [SetUp]
        public void SetUpScope()
        {
            scope1 = new BrowserWindow(DefaultSessionConfiguration, new IdFinder(Driver, "scope1", Root, DefaultOptions), Driver, null, null, null, DisambiguationStrategy);
            scope2 = new BrowserWindow(DefaultSessionConfiguration, new IdFinder(Driver, "scope2", Root, DefaultOptions), Driver, null, null, null, DisambiguationStrategy);
        }

        [Test]
        public void Finds_text_input_by_for()
        {
            Assert.That(Field("scoped text input field linked by for", scope1).Id, Is.EqualTo("scope1TextInputFieldId"));
            Assert.That(Field("scoped text input field linked by for", scope2).Id, Is.EqualTo("scope2TextInputFieldId"));
        }

        [Test]
        public void Finds_text_input_in_container_label()
        {
            Assert.That(Field("scoped text input field in a label container", scope1).Id, Is.EqualTo("scope1ContainerLabeledTextInputFieldId"));
            Assert.That(Field("scoped text input field in a label container", scope2).Id, Is.EqualTo("scope2ContainerLabeledTextInputFieldId"));
        }

        [Test]
        public void Finds_text_input_by_placeholder()
        {
            Assert.That(Field("scoped text input field with a placeholder", scope1).Id, Is.EqualTo("scope1TextInputFieldWithPlaceholder"));
            Assert.That(Field("scoped text input field with a placeholder", scope2).Id, Is.EqualTo("scope2TextInputFieldWithPlaceholder"));
        }

        [Test]
        public void Finds_text_input_by_name()
        {
            Assert.That(Field("text input field in a label container", scope1).Id, Is.EqualTo("scope1ContainerLabeledTextInputFieldId"));
            Assert.That(Field("text input field in a label container", scope2).Id, Is.EqualTo("scope2ContainerLabeledTextInputFieldId"));
        }

        [Test]
        public void Finds_radio_button_by_value()
        {
            Assert.That(Field("scoped radio field one val", scope1).Id, Is.EqualTo("scope1RadioFieldId"));
            Assert.That(Field("scoped radio field one val", scope2).Id, Is.EqualTo("scope2RadioFieldId"));
        }

        [Test]
        public void Finds_not_find_text_input_by_id_outside_scope()
        {
            Assert.Throws<MissingHtmlException>(() => Field("containerLabeledTextInputFieldId", scope1));
            Assert.Throws<MissingHtmlException>(() => Field("containerLabeledTextInputFieldId", scope2));
        }
    }

}