using System.Linq;
using Coypu.Finders;
using NUnit.Framework;

namespace Coypu.Drivers.Tests
{
    internal class When_finding_fields_by_container_label : DriverSpecs
    {
        [Test]
        public void Finds_text_input()
        {
            Assert.That(Field("text input field in a label container", options: Options.Exact).Id, Is.EqualTo("containerLabeledTextInputFieldId"));
        }

        [Test]
        public void Finds_password()
        {
            Assert.That(Field("password field in a label container").Id, Is.EqualTo("containerLabeledPasswordFieldId"));
        }

        [Test]
        public void Finds_checkbox()
        {
            Assert.That(Field("checkbox field in a label container").Id, Is.EqualTo("containerLabeledCheckboxFieldId"));
        }

        [Test]
        public void Finds_radio()
        {
            Assert.That(Field("radio field in a label container").Id, Is.EqualTo("containerLabeledRadioFieldId"));
        }

        [Test]
        public void Finds_select()
        {
            Assert.That(Field("select field in a label container").Id, Is.EqualTo("containerLabeledSelectFieldId"));
        }

        [Test]
        public void Finds_textarea()
        {
            Assert.That(Field("textarea field in a label container").Id, Is.EqualTo("containerLabeledTextareaFieldId"));
        }

        [Test]
        public void Finds_file_field()
        {
            Assert.That(Field("file field in a label container", options: Options.Exact).Id, Is.EqualTo("containerLabeledFileFieldId"));
        }

        [Test]
        public void Finds_by_substring()
        {
            var fields = new FieldFinder(Driver, "Some container labeled radio option", Root, DefaultOptions).Find(Options.Substring);
            Assert.That(fields.Select(e => e.Id).OrderBy(id => id), Is.EquivalentTo(new[]
                {
                    "containerLabeledRadioFieldExactMatchId",
                    "containerLabeledRadioFieldPartialMatchId"
                }));
        }

        [Test]
        public void Finds_by_exact_text()
        {
            var fields = new FieldFinder(Driver, "Some container labeled radio option", Root, DefaultOptions).Find(Options.Exact);
            Assert.That(fields.Select(e => e.Id).OrderBy(id => id), Is.EquivalentTo(new[]
                {
                    "containerLabeledRadioFieldExactMatchId"
                }));
        }
    }
}