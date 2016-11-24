using System;
using Coypu.AcceptanceTests.Sites;
using NUnit.Framework;

namespace Coypu.AcceptanceTests
{
    [TestFixture]
    public class Location
    {
        private BrowserSession _browser;
        private SelfishSite _site;

        [SetUp]
        public void SetUp()
        {
            _site = new SelfishSite();

            var sessionConfiguration = new SessionConfiguration
            {
                Timeout = TimeSpan.FromMilliseconds(1000),
                Port = _site.BaseUri.Port
            };
            _browser = new BrowserSession(sessionConfiguration);

            _browser.Visit("/");
        }

        [TearDown]
        public void TearDown()
        {
            _browser.Dispose();
            _site.Dispose();
        }

        [Test]
        public void It_exposes_the_current_page_location()
        {
            _browser.Visit("/");
            Assert.That(_browser.Location, Is.EqualTo(new Uri(_site.BaseUri, "/")));

            _browser.Visit("/auto_login");
            Assert.That(_browser.Location, Is.EqualTo(new Uri(_site.BaseUri, "/auto_login")));
        }

        [Test]
        public void Go_back_and_forward_in_history()
        {
            _browser.Visit("/");
            _browser.Visit("/auto_login");
            Assert.That(_browser.Location, Is.EqualTo(new Uri(_site.BaseUri, "/auto_login")));

            _browser.GoBack();
            Assert.That(_browser.Location, Is.EqualTo(new Uri(_site.BaseUri, "/")));

            _browser.GoForward();
            Assert.That(_browser.Location, Is.EqualTo(new Uri(_site.BaseUri, "/auto_login")));
        }

        private void ReloadTestPage()
        {
            _browser.Visit(Helper.GetProjectFile(@"html\InteractionTestsPage.htm"));
        }

        [Test, Ignore("Didn't work from original fork")]
        public void It_exposes_the_location_of_an_iframe_scope()
        {
            ReloadTestPage();
            Assert.That(_browser.FindFrame("iframe1").Location.AbsolutePath, Does.Contain("iFrame1.htm"));
        }
    }
}