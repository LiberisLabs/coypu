using Coypu.Finders;
using NUnit.Framework;

namespace Coypu.Drivers.Tests.Tests
{
    [TestFixture]
    internal class WhenFindingWindows
    {
        private static class Helpers
        {
            public static void OpenPopup(Driver driver)
            {
                driver.Click(FindPopUpLink(driver));
            }

            public static void OpenPopup2(Driver driver)
            {
                driver.Click(FindPopUp2Link(driver));
            }

            public static Element FindPopUpLink(Driver driver)
            {
                return DriverSpecs.Link(driver, "Open pop up window", DriverSpecs.Root, DriverSpecs.DefaultOptions);
            }

            public static Element FindPopUp2Link(Driver driver)
            {
                return DriverSpecs.Link(driver, "Open pop up window 2", DriverSpecs.Root, DriverSpecs.DefaultOptions);
            }

            public static Element FindPopUp(Driver driver)
            {
                return FindWindow(driver, "Pop Up Window");
            }

            public static Element FindWindow(Driver driver, string locator)
            {
                return DriverSpecs.Window(driver, locator, DriverSpecs.Root, DriverSpecs.DefaultOptions);
            }
        }

        private Driver _driver;

        [SetUp]
        public void Given() => _driver = DriverSpecs.Instance();

        [TearDown]
        public void Kill() => _driver.Dispose();

        [Test]
        public void Finds_by_name()
        {
            Helpers.OpenPopup(_driver);
            var window = DriverSpecs.Window("popUpWindowName", DriverSpecs.Root, DriverSpecs.DefaultOptions);

            Assert.That(window.Text, Does.Contain("I am a pop up window"));

            Helpers.FindPopUpLink(_driver);
        }

        [Test]
        public void Finds_by_title()
        {
            Helpers.OpenPopup(_driver);
            Assert.That(Helpers.FindPopUp(_driver).Text, Does.Contain("I am a pop up window"));

            Helpers.FindPopUpLink(_driver);
        }

        [Test]
        public void Finds_by_substring_title()
        {
            Helpers.OpenPopup2(_driver);
            Assert.That(Helpers.FindPopUp(_driver).Text, Does.Contain("I am a pop up window 2"));
            Helpers.FindPopUp2Link(_driver);
        }

        [Test]
        public void Finds_by_exact_title_over_substring()
        {
            Helpers.OpenPopup(_driver);
            Helpers.OpenPopup2(_driver);
            Assert.That(Helpers.FindPopUp(_driver).Text, Does.Contain("I am a pop up window"));

            Helpers.FindPopUpLink(_driver);
        }

        [Test]
        public void Finds_scoped_by_window()
        {
            Helpers.OpenPopup(_driver);

            var popUp = new BrowserWindow(DriverSpecs.DefaultSessionConfiguration, new WindowFinder(_driver, "Pop Up Window", DriverSpecs.Root, DriverSpecs.DefaultOptions),
                                          _driver, null, null, null, new ThrowsWhenMissingButNoDisambiguationStrategy());

            DriverSpecs.Id("popUpButtonId", popUp);

            Helpers.FindPopUpLink(_driver);
        }

        [Test]
        public void Errors_on_no_such_window()
        {
            Helpers.OpenPopup(_driver);
            Assert.Throws<MissingWindowException>(() => Helpers.FindWindow(_driver, "Not A Window"));
        }

        [Test]
        public void Errors_on_window_closed()
        {
            Helpers.OpenPopup(_driver);
            var popUp = new BrowserWindow(DriverSpecs.DefaultSessionConfiguration, new WindowFinder(_driver, "Pop Up Window", DriverSpecs.Root, DriverSpecs.DefaultOptions),
                                          _driver, null, null, null, new ThrowsWhenMissingButNoDisambiguationStrategy());

            _driver.ExecuteScript("self.close();", popUp);
            Assert.Throws<MissingWindowException>(() => Helpers.FindPopUp(_driver));
        }
    }
}