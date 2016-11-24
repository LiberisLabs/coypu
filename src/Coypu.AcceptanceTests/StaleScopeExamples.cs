using System;
using System.Linq;
using Coypu.Drivers;
using Coypu.Drivers.Selenium;
using NUnit.Framework;

namespace Coypu.AcceptanceTests
{
    /// <summary>
    /// Simple examples for each API method - to show usage and check everything is wired up properly
    /// </summary>
    [TestFixture]
    public class StaleScopeExamples
    {
        private BrowserSession _browser;

        [OneTimeSetUp]
        public void SetUpFixture()
        {
            var configuration = new SessionConfiguration
            {
                Timeout = TimeSpan.FromMilliseconds(2000),
                Driver = typeof(SeleniumWebDriver),
                Browser = Browser.Chrome
            };

            _browser = new BrowserSession(configuration);
        }

        [OneTimeTearDown]
        public void TearDown() => _browser.Dispose();

        private void VisitTestPage(string page) => _browser.Visit(Helper.GetProjectFile($"html\\{page}"));

        [Test]
        public void Scope_becomes_stale()
        {
            VisitTestPage("tricky.htm");

            var section1 = _browser.FindSection("section 1");
            Assert.That(section1.FindLink("the link").Exists());

            var originalLocation = _browser.Location;

            VisitTestPage("iFrame1.htm");

            Assert.That(section1.FindLink("the link").Missing());

            _browser.ExecuteScript("window.setTimeout(function() {window.location.href = '" + originalLocation + "'},1000);");

            section1.ClickLink("the link");
        }

        [Test]
        public void Scope_becomes_stale_looking_for_all_xpath()
        {
            VisitTestPage("tricky.htm");

            var section1 = _browser.FindSection("section 1");
            Assert.That(section1.FindLink("the link").Exists());

            VisitTestPage("iFrame1.htm");
            VisitTestPage("tricky.htm");
            Assert.That(section1.FindAllXPath("*").Count(), Is.GreaterThan(0));
        }

        [Test]
        public void Scope_becomes_stale_looking_for_all_css()
        {
            VisitTestPage("tricky.htm");

            var section1 = _browser.FindSection("section 1");
            Assert.That(section1.FindLink("the link").Exists());

            VisitTestPage("iFrame1.htm");
            VisitTestPage("tricky.htm");
            Assert.That(section1.FindAllCss("*").Count(), Is.GreaterThan(0));
        }


        [Test]
        public void Scope_becomes_stale_iframe()
        {
            VisitTestPage("InteractionTestsPage.htm");

            var originalLocation = _browser.Location;

            var iframe1 = _browser.FindFrame("iframe1");
            var iframe2 = _browser.FindFrame("iframe2");
            var button = iframe1.FindButton("scoped button");

            Assert.That(button.Exists());

            Assert.That(iframe1.HasContent("I am iframe one"));

            VisitTestPage("tricky.htm");

            Assert.That(iframe1.Missing());
            Assert.That(button.Missing());

            _browser.ExecuteScript("window.setTimeout(function() {window.location.href = '" + originalLocation + "'},1000);");

            Assert.That(iframe1.HasContent("I am iframe one"));
            Assert.That(iframe2.HasContent("I am iframe two"));

            Assert.That(_browser.Title, Is.EqualTo("Coypu interaction tests page"));

            button.Click();
        }

        [Test]
        public void Scope_becomes_stale_window()
        {
            VisitTestPage("InteractionTestsPage.htm");

            _browser.ClickLink("Open pop up window");

            var timeToClickManuallyInIE = new Options {Timeout = TimeSpan.FromSeconds(10)};

            var popUp = _browser.FindWindow("Pop Up Window");
            var button = popUp.FindButton("scoped button", timeToClickManuallyInIE);

            Assert.That(button.Exists());
            Assert.That(popUp.HasContent("I am a pop up window"));

            CloseWindow(popUp);
            Assert.That(popUp.Missing(), "Expected popUp window to be missing");
            Assert.That(button.Missing(), "Expected button in popup to be missing");

            _browser.ClickLink("Open pop up window");

            Assert.That(popUp.HasContent("I am a pop up window", timeToClickManuallyInIE));
            button.Click();
        }

        [Test]
        public void Window_is_not_refound_unless_stale()
        {
            VisitTestPage("InteractionTestsPage.htm");
            _browser.ClickLink("Open pop up window");

            var popUp = _browser.FindWindow("Pop Up Window");
            var popUpBody = popUp.FindCss("body");

            popUp.ExecuteScript("document.title = 'Changed title';");
            Assert.That(_browser.FindLink("Open pop up window").Exists());

            Assert.That(popUp.HasContent("I am a pop up window"));

            var findPopUpAgain = _browser.FindWindow("Pop Up Window");
            Assert.That(findPopUpAgain.Missing(), "Expected pop-up not to be found now title has changed");
        }


        private static void CloseWindow(BrowserWindow popUp)
        {
            try
            {
                popUp.ExecuteScript("self.close();");
            }
            catch (Exception InvalidCastException)
            {
                // IE permissions
            }
        }
    }
}