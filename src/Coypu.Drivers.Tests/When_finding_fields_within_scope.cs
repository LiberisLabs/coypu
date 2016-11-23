using Coypu.Finders;
using NSpec;
using NUnit.Framework;

namespace Coypu.Drivers.Tests
{
    internal class WhenFindingFieldsWithinScope : DriverSpecs
    {
        private DriverScope _scope1;
        private DriverScope _scope2;

        [SetUp]
        public void SetUpScope()
        {
            _scope1 = new BrowserWindow(DefaultSessionConfiguration, new IdFinder(Driver, "scope1", Root, DefaultOptions), Driver, null, null, null, DisambiguationStrategy);
            _scope2 = new BrowserWindow(DefaultSessionConfiguration, new IdFinder(Driver, "scope2", Root, DefaultOptions), Driver, null, null, null, DisambiguationStrategy);
        }

        [Test]
        public void Finds_text_input_by_for()
        {
            Field("scoped text input field linked by for", _scope1).Id.should_be("scope1TextInputFieldId");
            Field("scoped text input field linked by for", _scope2).Id.should_be("scope2TextInputFieldId");
        }

        [Test]
        public void Finds_text_input_in_container_label()
        {
            Field("scoped text input field in a label container", _scope1).Id.should_be("scope1ContainerLabeledTextInputFieldId");
            Field("scoped text input field in a label container", _scope2).Id.should_be("scope2ContainerLabeledTextInputFieldId");
        }

        [Test]
        public void Finds_text_input_by_placeholder()
        {
            Field("scoped text input field with a placeholder", _scope1).Id.should_be("scope1TextInputFieldWithPlaceholder");
            Field("scoped text input field with a placeholder", _scope2).Id.should_be("scope2TextInputFieldWithPlaceholder");
        }

        [Test]
        public void Finds_text_input_by_name()
        {
            Field("text input field in a label container", _scope1).Id.should_be("scope1ContainerLabeledTextInputFieldId");
            Field("text input field in a label container", _scope2).Id.should_be("scope2ContainerLabeledTextInputFieldId");
        }

        [Test]
        public void Finds_radio_button_by_value()
        {
            Field("scoped radio field one val", _scope1).Id.should_be("scope1RadioFieldId");
            Field("scoped radio field one val", _scope2).Id.should_be("scope2RadioFieldId");
        }

        [Test]
        public void Finds_not_find_text_input_by_id_outside_scope()
        {
            Assert.Throws<MissingHtmlException>(() => Field("containerLabeledTextInputFieldId", _scope1));
            Assert.Throws<MissingHtmlException>(() => Field("containerLabeledTextInputFieldId", _scope2));
        }
    }

}