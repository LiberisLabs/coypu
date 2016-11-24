using System;
using System.Threading;
using Coypu.Finders;
using NUnit.Framework;

namespace Coypu.Drivers.Tests.Tests
{
    [TestFixture]
    internal class WhenNavigating
    {
        private Driver _driver;
        private DriverScope _scope;

        [OneTimeSetUp]
        public void Given()
        {
            _driver = TestDriver.Instance();
            _scope = DriverHelpers.WindowScope(_driver);
        }

        [Test]
        public void Gets_the_current_browser_location()
        {
            _driver.Visit(SomeRandomStaticHelpers.TestSiteUrl("/"), _scope);
            Assert.That(new Uri(SomeRandomStaticHelpers.TestSiteUrl("/")), Is.EqualTo(_driver.Location(_scope)));

            _driver.Visit(SomeRandomStaticHelpers.TestSiteUrl("/auto_login"), _scope);
            Assert.That(new Uri(SomeRandomStaticHelpers.TestSiteUrl("/auto_login")), Is.EqualTo(_driver.Location(_scope)));
        }

        [Test]
        public void Gets_location_for_correct_window_scope()
        {
            _driver.Click(DriverHelpers.Link(_driver, "Open pop up window"));
            var popUp = new BrowserWindow(Default.SessionConfiguration, new WindowFinder(_driver, "Pop Up Window", _scope, Default.Options), _driver, null, null, null, new ThrowsWhenMissingButNoDisambiguationStrategy());

            Assert.That(_driver.Location(popUp).AbsoluteUri, Does.Contain("/html/popup.htm"));
        }

        [Test]
        public void Not_just_when_set_by_visit()
        {
            _driver.Visit(SomeRandomStaticHelpers.TestSiteUrl("/auto_login"), _scope);
            _driver.ExecuteScript("document.location.href = '" + SomeRandomStaticHelpers.TestSiteUrl("/resource/bdd") + "'", _scope);

            // Seems like WebDriver is not waiting on JS, has exec been made asnyc?
            Thread.Sleep(500);

            Assert.That(new Uri(SomeRandomStaticHelpers.TestSiteUrl("/resource/bdd")), Is.EqualTo(_driver.Location(_scope)));
        }
    }
}