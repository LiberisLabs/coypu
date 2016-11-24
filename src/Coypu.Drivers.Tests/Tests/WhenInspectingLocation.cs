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
            Assert.That(new Uri(SomeRandomStaticHelpers.TestSiteUrl("/")), Is.EqualTo(_driver.Location(DriverSpecs.Root)));

            _driver.GoForward(DriverSpecs.Root);
            Assert.That(new Uri(SomeRandomStaticHelpers.TestSiteUrl("/auto_login")), Is.EqualTo(_driver.Location(DriverSpecs.Root)));
        }

        [Test]
        public void Go_back_and_forward_in_correct_window_scope()
        {
            _driver.Click(DriverHelpers.Link(_driver, "Open pop up window"));
            var popUp = new BrowserWindow(Default.SessionConfiguration, new WindowFinder(_driver, "Pop Up Window", DriverSpecs.Root, Default.Options), _driver, null, null,
                                          null, new ThrowsWhenMissingButNoDisambiguationStrategy());

            _driver.Visit(SomeRandomStaticHelpers.TestSiteUrl("/auto_login"), DriverSpecs.Root);
            _driver.Visit(SomeRandomStaticHelpers.TestSiteUrl("/"), popUp);

            _driver.GoBack(popUp);
            Assert.That(_driver.Location(popUp).AbsoluteUri, Does.EndWith("/html/popup.htm"));
            Assert.That(SomeRandomStaticHelpers.TestSiteUrl("/auto_login"), Is.EqualTo(_driver.Location(DriverSpecs.Root).AbsoluteUri));

            _driver.GoForward(popUp);
            Assert.That(SomeRandomStaticHelpers.TestSiteUrl("/"), Is.EqualTo(_driver.Location(popUp).AbsoluteUri));

            _driver.GoBack(DriverSpecs.Root);
            Assert.That(_driver.Location(DriverSpecs.Root).AbsoluteUri, Does.EndWith("/html/InteractionTestsPage.htm"));
            Assert.That(SomeRandomStaticHelpers.TestSiteUrl("/"), Is.EqualTo(_driver.Location(popUp).AbsoluteUri));
        }
    }
}