using Coypu.Finders;
using NUnit.Framework;

namespace Coypu.Drivers.Tests.Tests
{
    [TestFixture]
    internal class WhenUsingIframesAsScope
    {
        private static class Helpers
        {
            public static void Finds_elements_among_multiple_scopes(Driver driver, ElementFinder elementFinder1, ElementFinder elementFinder2)
            {
                IDisambiguationStrategy disambiguationStrategy = new ThrowsWhenMissingButNoDisambiguationStrategy();
                var iframeOne = new BrowserWindow(DriverSpecs.DefaultSessionConfiguration, elementFinder1, driver, null, null, null, disambiguationStrategy);
                var iframeTwo = new BrowserWindow(DriverSpecs.DefaultSessionConfiguration, elementFinder2, driver, null, null, null, disambiguationStrategy);

                Assert.That(DriverSpecs.Button(driver, "scoped button", iframeOne, DriverSpecs.DefaultOptions).Id, Is.EqualTo("iframe1ButtonId"));
                Assert.That(DriverSpecs.Button(driver, "scoped button", iframeTwo, DriverSpecs.DefaultOptions).Id, Is.EqualTo("iframe2ButtonId"));
            }

            public static Element FindField(Driver driver, string locator, DriverScope scope)
            {
                return DriverSpecs.Field(driver, locator, scope, DriverSpecs.DefaultOptions);
            }
        }

        private Driver _driver;

        [SetUp]
        public void Given() => _driver = DriverSpecs.Instance();

        [TearDown]
        public void Kill() => _driver.Dispose();

        [Test]
        public void Does_not_find_something_in_an_iframe()
        {
            Assert.Throws<MissingHtmlException>(() => DriverSpecs.Id("iframe1ButtonId", DriverSpecs.Root, DriverSpecs.DefaultOptions));
        }

        [Test, Explicit("Didn't work from original fork")]
        public void Finds_elements_among_multiple_scopes()
        {
            Helpers.Finds_elements_among_multiple_scopes(_driver,
                                                         new FrameFinder(_driver, "I am iframe one", DriverSpecs.Root, DriverSpecs.DefaultOptions),
                                                         new FrameFinder(_driver, "I am iframe two", DriverSpecs.Root, DriverSpecs.DefaultOptions));
        }

        [Test]
        public void Finds_elements_among_multiple_scopes_when_finding_by_css()
        {
            Helpers.Finds_elements_among_multiple_scopes(_driver,
                                                         new CssFinder(_driver, "iframe#iframe1", DriverSpecs.Root, DriverSpecs.DefaultOptions),
                                                         new CssFinder(_driver, "iframe#iframe2", DriverSpecs.Root, DriverSpecs.DefaultOptions));
        }

        [Test]
        public void Finds_elements_among_multiple_scopes_when_finding_by_xpath()
        {
            Helpers.Finds_elements_among_multiple_scopes(_driver,
                                                         new XPathFinder(_driver, "//iframe[@id='iframe1']", DriverSpecs.Root, DriverSpecs.DefaultOptions),
                                                         new XPathFinder(_driver, "//iframe[@id='iframe2']", DriverSpecs.Root, DriverSpecs.DefaultOptions));
        }

        [Test]
        public void Finds_elements_among_multiple_scopes_when_finding_by_id()
        {
            Helpers.Finds_elements_among_multiple_scopes(_driver,
                                                         new IdFinder(_driver, "iframe1", DriverSpecs.Root, DriverSpecs.DefaultOptions),
                                                         new IdFinder(_driver, "iframe2", DriverSpecs.Root, DriverSpecs.DefaultOptions));
        }

        [Test]
        public void Finds_clear_scope_back_to_the_whole_window()
        {
            var iframeOne = new BrowserWindow(DriverSpecs.DefaultSessionConfiguration, new FrameFinder(_driver, "I am iframe one", DriverSpecs.Root, DriverSpecs.DefaultOptions),
                                              _driver, null, null, null, new ThrowsWhenMissingButNoDisambiguationStrategy());
            Assert.That(DriverSpecs.Button("scoped button", iframeOne, DriverSpecs.DefaultOptions).Id, Is.EqualTo("iframe1ButtonId"));

            Assert.That(DriverSpecs.Button("scoped button", DriverSpecs.Root, Options.PreferExact).Id, Is.EqualTo("scope1ButtonId"));
        }

        [Test]
        public void Can_fill_in_a_text_input_within_an_iframe()
        {
            var iframeOne = new BrowserWindow(DriverSpecs.DefaultSessionConfiguration, new FrameFinder(_driver, "I am iframe one", DriverSpecs.Root, DriverSpecs.DefaultOptions),
                                              _driver, null, null, null, new ThrowsWhenMissingButNoDisambiguationStrategy());
            _driver.Set(Helpers.FindField(_driver, "text input in iframe", iframeOne), "filled in");

            Assert.That(Helpers.FindField(_driver, "text input in iframe", iframeOne).Value, Is.EqualTo("filled in"));
        }

        [Test]
        public void Can_scope_around_an_iframe()
        {
            IDisambiguationStrategy disambiguationStrategy = new ThrowsWhenMissingButNoDisambiguationStrategy();
            var iframeOne = new BrowserWindow(DriverSpecs.DefaultSessionConfiguration, new FrameFinder(_driver, "I am iframe one", DriverSpecs.Root, DriverSpecs.DefaultOptions),
                                              _driver, null, null, null, disambiguationStrategy);
            Assert.That(DriverSpecs.Button("scoped button", iframeOne, DriverSpecs.DefaultOptions).Id, Is.EqualTo("iframe1ButtonId"));

            var body = new BrowserWindow(DriverSpecs.DefaultSessionConfiguration, new CssFinder(_driver, "body", DriverSpecs.Root, DriverSpecs.DefaultOptions), _driver, null,
                                         null, null, disambiguationStrategy);
            Assert.That(DriverSpecs.Button("scoped button", body, Options.PreferExact).Id, Is.EqualTo("scope1ButtonId"));
        }

        [Test]
        public void Can_scope_inside_an_iframe()
        {
            var iframeOne = new BrowserWindow(DriverSpecs.DefaultSessionConfiguration, new FrameFinder(_driver, "I am iframe one", DriverSpecs.Root, DriverSpecs.DefaultOptions),
                                              _driver, null, null, null, null);
            var iframeForm = new BrowserWindow(DriverSpecs.DefaultSessionConfiguration, new CssFinder(_driver, "form", iframeOne, DriverSpecs.DefaultOptions), _driver, null, null,
                                               null, null);
        }
    }
}