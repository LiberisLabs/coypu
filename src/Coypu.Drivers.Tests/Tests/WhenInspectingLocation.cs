using System;
using Coypu.Finders;
using NUnit.Framework;

namespace Coypu.Drivers.Tests.Tests
{
    [TestFixture]
    internal class WhenInspectingLocation
    {
        private Driver _driver;

        [SetUp]
        public void Given() => _driver = DriverSpecs.Instance();

        [TearDown]
        public void Kill() => _driver.Dispose();

        [Test]
        public void Go_back_and_forward_in_history()
        {
            _driver.Visit(SomeRandomStaticHelpers.TestSiteUrl("/"), DriverSpecs.Root);
            _driver.Visit(SomeRandomStaticHelpers.TestSiteUrl("/auto_login"), DriverSpecs.Root);

            _driver.GoBack(DriverSpecs.Root);
            Assert.That(_driver.Location(DriverSpecs.Root), Is.EqualTo(new Uri(SomeRandomStaticHelpers.TestSiteUrl("/"))));

            _driver.GoForward(DriverSpecs.Root);
            Assert.That(_driver.Location(DriverSpecs.Root), Is.EqualTo(new Uri(SomeRandomStaticHelpers.TestSiteUrl("/auto_login"))));
        }

        [Test]
        public void Go_back_and_forward_in_correct_window_scope()
        {
            _driver.Click(DriverSpecs.Link("Open pop up window"));
            var popUp = new BrowserWindow(DriverSpecs.DefaultSessionConfiguration, new WindowFinder(_driver, "Pop Up Window", DriverSpecs.Root, DriverSpecs.DefaultOptions), _driver, null, null,
                                          null, new ThrowsWhenMissingButNoDisambiguationStrategy());

            _driver.Visit(SomeRandomStaticHelpers.TestSiteUrl("/auto_login"), DriverSpecs.Root);
            _driver.Visit(SomeRandomStaticHelpers.TestSiteUrl("/"), popUp);

            _driver.GoBack(popUp);
            Assert.That(_driver.Location(popUp).AbsoluteUri, Does.EndWith("/html/popup.htm"));
            Assert.That(_driver.Location(DriverSpecs.Root).AbsoluteUri, Is.EqualTo(SomeRandomStaticHelpers.TestSiteUrl("/auto_login")));

            _driver.GoForward(popUp);
            Assert.That(_driver.Location(popUp).AbsoluteUri, Is.EqualTo(SomeRandomStaticHelpers.TestSiteUrl("/")));

            _driver.GoBack(DriverSpecs.Root);
            Assert.That(_driver.Location(DriverSpecs.Root).AbsoluteUri, Does.EndWith("/html/InteractionTestsPage.htm"));
            Assert.That(_driver.Location(popUp).AbsoluteUri, Is.EqualTo(SomeRandomStaticHelpers.TestSiteUrl("/")));
        }
    }
}