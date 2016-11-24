using Coypu.Finders;
using NUnit.Framework;

namespace Coypu.Drivers.Tests.Tests
{
    [TestFixture]
    internal class WhenSelectingOptions
    {
        [OneTimeSetUp]
        public void Given() => DriverSpecs.DoSetUp();

        private static DriverScope GetSelectScope(string locator)
        {
            return new BrowserWindow(DriverSpecs.DefaultSessionConfiguration,
                                     new SelectFinder(DriverSpecs.Driver, locator, DriverSpecs.Root, DriverSpecs.DefaultOptions), DriverSpecs.Driver,
                                     null, null, null, new ThrowsWhenMissingButNoDisambiguationStrategy());
        }

        [Test]
        public void Sets_text_of_selected_option()
        {
            Assert.That("select two option one", Is.EqualTo(DriverSpecs.Field("containerLabeledSelectFieldId").SelectedOption));

            DriverSpecs.Driver.Click(DriverSpecs.FindSingle(new OptionFinder(DriverSpecs.Driver, "select two option two", GetSelectScope("containerLabeledSelectFieldId"), DriverSpecs.DefaultOptions)));

            Assert.That("select two option two", Is.EqualTo(DriverSpecs.Field("containerLabeledSelectFieldId").SelectedOption));
        }

        [Test]
        public void Selected_option_respects_TextPrecision()
        {
            Assert.That(
                DriverSpecs.FindSingle(new OptionFinder(DriverSpecs.Driver, "select two option t", GetSelectScope("containerLabeledSelectFieldId"), Options.Substring)).Text,
                Is.EqualTo("select two option two"));

            Assert.That(
                DriverSpecs.FindSingle(new OptionFinder(DriverSpecs.Driver, "select two option two", GetSelectScope("containerLabeledSelectFieldId"), Options.Exact)).Text,
                Is.EqualTo("select two option two"));

            Assert.Throws<MissingHtmlException>(
                () => DriverSpecs.FindSingle(new OptionFinder(DriverSpecs.Driver, "select two option t", GetSelectScope("containerLabeledSelectFieldId"), Options.Exact)));
        }

        [Test]
        public void Selected_option_finds_exact_by_container_label()
        {
            Assert.That(DriverSpecs.FindSingle(new OptionFinder(DriverSpecs.Driver, "one", GetSelectScope("Ambiguous select options"), Options.Exact)).Text, Is.EqualTo("one"));
        }

        [Test]
        public void Selected_option_finds_substring_by_container_label()
        {
            Assert.That(DriverSpecs.FindSingle(new OptionFinder(DriverSpecs.Driver, "one", GetSelectScope("Ambiguous select options"), Options.Substring)).Text, Is.EqualTo("one"));
        }
    }
}