using System;
using Coypu.Finders;
using NUnit.Framework;

namespace Coypu.Drivers.Tests.Tests
{
    [TestFixture]
    internal class WhenInspectingLocation
    {
        private Driver _driver;
        private DriverScope _scope;

        [SetUp]
        public void Given()
        {
            _driver = TestDriver.Instance();
            _scope = DriverHelpers.WindowScope(_driver);
        }

        [TearDown]
        public void Kill() => _driver.Dispose();

        [Test]
        public void Go_back_and_forward_in_history()
        {
            _driver.Visit(SomeRandomStaticHelpers.TestSiteUrl("/"), _scope);
            _driver.Visit(SomeRandomStaticHelpers.TestSiteUrl("/auto_login"), _scope);

            _driver.GoBack(_scope);
            Assert.That(new Uri(SomeRandomStaticHelpers.TestSiteUrl("/")), Is.EqualTo(_driver.Location(_scope)));

            _driver.GoForward(_scope);
            Assert.That(new Uri(SomeRandomStaticHelpers.TestSiteUrl("/auto_login")), Is.EqualTo(_driver.Location(_scope)));
        }

        [Test]
        public void Go_back_and_forward_in_correct_window_scope()
        {
            _driver.Click(DriverHelpers.Link(_driver, "Open pop up window"));
            var popUp = new BrowserWindow(Default.SessionConfiguration, new WindowFinder(_driver, "Pop Up Window", _scope, Default.Options), _driver, null, null,
                                          null, new ThrowsWhenMissingButNoDisambiguationStrategy());

            _driver.Visit(SomeRandomStaticHelpers.TestSiteUrl("/auto_login"), _scope);
            _driver.Visit(SomeRandomStaticHelpers.TestSiteUrl("/"), popUp);

            _driver.GoBack(popUp);
            Assert.That(_driver.Location(popUp).AbsoluteUri, Does.EndWith("/html/popup.htm"));
            Assert.That(SomeRandomStaticHelpers.TestSiteUrl("/auto_login"), Is.EqualTo(_driver.Location(_scope).AbsoluteUri));

            _driver.GoForward(popUp);
            Assert.That(SomeRandomStaticHelpers.TestSiteUrl("/"), Is.EqualTo(_driver.Location(popUp).AbsoluteUri));

            _driver.GoBack(_scope);
            Assert.That(_driver.Location(_scope).AbsoluteUri, Does.EndWith("/html/InteractionTestsPage.htm"));
            Assert.That(SomeRandomStaticHelpers.TestSiteUrl("/"), Is.EqualTo(_driver.Location(popUp).AbsoluteUri));
        }
    }
}