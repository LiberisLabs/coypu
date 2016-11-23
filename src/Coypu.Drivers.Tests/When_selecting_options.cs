﻿using Coypu.Finders;
using NSpec;
using NUnit.Framework;

namespace Coypu.Drivers.Tests
{
    internal class WhenSelectingOptions : DriverSpecs
    {
        private static DriverScope GetSelectScope(string locator)
        {
            return new BrowserWindow(DefaultSessionConfiguration,
                                     new SelectFinder(Driver, locator, Root, DefaultOptions), Driver,
                                     null, null, null, DisambiguationStrategy);
        }

        [Test]
        public void Sets_text_of_selected_option()
        {
            Field("containerLabeledSelectFieldId").SelectedOption.should_be("select two option one");

            Driver.Click(FindSingle(new OptionFinder(Driver, "select two option two", GetSelectScope("containerLabeledSelectFieldId"), DefaultOptions)));

            Field("containerLabeledSelectFieldId").SelectedOption.should_be("select two option two");
        }

        [Test]
        public void Selected_option_respects_TextPrecision()
        {
            Assert.That(
                FindSingle(new OptionFinder(Driver, "select two option t", GetSelectScope("containerLabeledSelectFieldId"), Options.Substring)).Text,
                Is.EqualTo("select two option two"));

            Assert.That(
                FindSingle(new OptionFinder(Driver, "select two option two", GetSelectScope("containerLabeledSelectFieldId"), Options.Exact)).Text,
                Is.EqualTo("select two option two"));

            Assert.Throws<MissingHtmlException>(
                () => FindSingle(new OptionFinder(Driver, "select two option t", GetSelectScope("containerLabeledSelectFieldId"), Options.Exact)));
        }

        [Test]
        public void Selected_option_finds_exact_by_container_label()
        {
            Assert.That(FindSingle(new OptionFinder(Driver, "one", GetSelectScope("Ambiguous select options"), Options.Exact)).Text, Is.EqualTo("one"));
        }

        [Test]
        public void Selected_option_finds_substring_by_container_label()
        {
            Assert.That(FindSingle(new OptionFinder(Driver, "one", GetSelectScope("Ambiguous select options"), Options.Substring)).Text, Is.EqualTo("one"));
        }
    }
}