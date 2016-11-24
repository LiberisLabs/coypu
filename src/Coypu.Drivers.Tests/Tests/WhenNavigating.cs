using System;
using System.Threading;
using Coypu.Finders;
using NUnit.Framework;

namespace Coypu.Drivers.Tests.Tests
{
    [TestFixture]
    internal class WhenNavigating
    {
        [OneTimeSetUp]
        public void Given() => DriverSpecs.DoSetUp();

        [Test]
        public void Gets_the_current_browser_location()
        {
            DriverSpecs.Driver.Visit(SomeRandomStaticHelpers.TestSiteUrl("/"), DriverSpecs.Root);
            Assert.That(new Uri(SomeRandomStaticHelpers.TestSiteUrl("/")), Is.EqualTo(DriverSpecs.Driver.Location(DriverSpecs.Root)));

            DriverSpecs.Driver.Visit(SomeRandomStaticHelpers.TestSiteUrl("/auto_login"), DriverSpecs.Root);
            Assert.That(new Uri(SomeRandomStaticHelpers.TestSiteUrl("/auto_login")), Is.EqualTo(DriverSpecs.Driver.Location(DriverSpecs.Root)));
        }

        [Test]
        public void Gets_location_for_correct_window_scope()
        {
            DriverSpecs.Driver.Click(DriverSpecs.Link("Open pop up window"));
            var popUp = new BrowserWindow(DriverSpecs.DefaultSessionConfiguration, new WindowFinder(DriverSpecs.Driver, "Pop Up Window", DriverSpecs.Root, DriverSpecs.DefaultOptions), DriverSpecs.Driver, null, null, null, new ThrowsWhenMissingButNoDisambiguationStrategy());

            Assert.That(DriverSpecs.Driver.Location(popUp).AbsoluteUri, Does.Contain("/html/popup.htm"));
        }

        [Test]
        public void Not_just_when_set_by_visit()
        {
            DriverSpecs.Driver.Visit(SomeRandomStaticHelpers.TestSiteUrl("/auto_login"), DriverSpecs.Root);
            DriverSpecs.Driver.ExecuteScript("document.location.href = '" + SomeRandomStaticHelpers.TestSiteUrl("/resource/bdd") + "'", DriverSpecs.Root);

            // Seems like WebDriver is not waiting on JS, has exec been made asnyc?
            Thread.Sleep(500);

            Assert.That(new Uri(SomeRandomStaticHelpers.TestSiteUrl("/resource/bdd")), Is.EqualTo(DriverSpecs.Driver.Location(DriverSpecs.Root)));
        }
    }
}