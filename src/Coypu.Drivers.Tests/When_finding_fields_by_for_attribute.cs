using System.Linq;
using Coypu.Finders;
using NUnit.Framework;

namespace Coypu.Drivers.Tests
{
    internal class When_finding_fields_by_for_attribute : DriverSpecs
    {
        [Test]
        public void Finds_text_input()
        {
            Assert.That(Field("text input field linked by for", options: Options.Exact).Id, Is.EqualTo("forLabeledTextInputFieldId"));
        }

        [Test]
        public void Finds_password_field()
        {
            Assert.That(Field("password field linked by for").Id, Is.EqualTo("forLabeledPasswordFieldId"));
        }

        [Test]
        public void Finds_select_field()
        {
            Assert.That(Field("select field linked by for").Id, Is.EqualTo("forLabeledSelectFieldId"));
        }

        [Test]
        public void Finds_checkbox()
        {
            Assert.That(Field("checkbox field linked by for").Id, Is.EqualTo("forLabeledCheckboxFieldId"));
        }

        [Test]
        public void Finds_radio_button()
        {
            Assert.That(Field("radio field linked by for").Id, Is.EqualTo("forLabeledRadioFieldId"));
        }

        [Test]
        public void Finds_textarea()
        {
            Assert.That(Field("textarea field linked by for").Id, Is.EqualTo("forLabeledTextareaFieldId"));
        }

        [Test]
        public void Finds_file_input()
        {
            Assert.That(Field("file field linked by for").Id, Is.EqualTo("forLabeledFileFieldId"));
        }

        [Test]
        public void Finds_by_substring_text()
        {
            var fields = new FieldFinder(Driver, "Some for labeled radio option", Root, DefaultOptions).Find(Options.Substring);
            Assert.That(fields.Select(e => e.Id).OrderBy(id => id), Is.EquivalentTo(new[]
                {
                    "forLabeledRadioFieldExactMatchId",
                    "forLabeledRadioFieldPartialMatchId"
                }));
        }

        [Test]
        public void Finds_by_exact_text()
        {
            var fields = new FieldFinder(Driver, "Some for labeled radio option", Root, DefaultOptions).Find(Options.Exact);
            Assert.That(fields.Select(e => e.Id).OrderBy(id => id), Is.EquivalentTo(new[]
                {
                    "forLabeledRadioFieldExactMatchId"
                }));
        }
    }
}