using Coypu.Finders;
using NUnit.Framework;

namespace Coypu.Drivers.Tests.Tests
{
    [TestFixture]
    internal class WhenFindingWindows
    {
        private static class Helpers
        {
            public static void OpenPopup(Driver driver, DriverScope scope)
            {
                driver.Click(FindPopUpLink(driver, scope));
            }

            public static void OpenPopup2(Driver driver, DriverScope scope)
            {
                driver.Click(FindPopUp2Link(driver, scope));
            }

            public static Element FindPopUpLink(Driver driver, DriverScope scope)
            {
                return DriverHelpers.Link(driver, "Open pop up window", scope, Default.Options);
            }

            public static Element FindPopUp2Link(Driver driver, DriverScope scope)
            {
                return DriverHelpers.Link(driver, "Open pop up window 2", scope, Default.Options);
            }

            public static Element FindPopUp(Driver driver)
            {
                return FindWindow(driver, "Pop Up Window", DriverHelpers.WindowScope(driver));
            }

            public static Element FindWindow(Driver driver, string locator, DriverScope scope)
            {
                return DriverHelpers.Window(driver, locator, scope, Default.Options);
            }
        }

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
        public void Finds_by_name()
        {
            Helpers.OpenPopup(_driver, _scope);
            var window = DriverHelpers.Window(_driver, "popUpWindowName", _scope, Default.Options);

            Assert.That(window.Text, Does.Contain("I am a pop up window"));

            Helpers.FindPopUpLink(_driver, _scope);
        }

        [Test]
        public void Finds_by_title()
        {
            Helpers.OpenPopup(_driver, _scope);
            Assert.That(Helpers.FindPopUp(_driver).Text, Does.Contain("I am a pop up window"));

            Helpers.FindPopUpLink(_driver, _scope);
        }

        [Test]
        public void Finds_by_substring_title()
        {
            Helpers.OpenPopup2(_driver, _scope);
            Assert.That(Helpers.FindPopUp(_driver).Text, Does.Contain("I am a pop up window 2"));
            Helpers.FindPopUp2Link(_driver, _scope);
        }

        [Test]
        public void Finds_by_exact_title_over_substring()
        {
            Helpers.OpenPopup(_driver, _scope);
            Helpers.OpenPopup2(_driver, _scope);
            Assert.That(Helpers.FindPopUp(_driver).Text, Does.Contain("I am a pop up window"));

            Helpers.FindPopUpLink(_driver, _scope);
        }

        [Test]
        public void Finds_scoped_by_window()
        {
            Helpers.OpenPopup(_driver, _scope);

            var popUp = new BrowserWindow(Default.SessionConfiguration, new WindowFinder(_driver, "Pop Up Window", _scope, Default.Options),
                                          _driver, null, null, null, new ThrowsWhenMissingButNoDisambiguationStrategy());

            DriverHelpers.Id(_driver, "popUpButtonId", popUp);

            Helpers.FindPopUpLink(_driver, _scope);
        }

        [Test]
        public void Errors_on_no_such_window()
        {
            Helpers.OpenPopup(_driver, _scope);
            Assert.Throws<MissingWindowException>(() => Helpers.FindWindow(_driver, "Not A Window", _scope));
        }

        [Test]
        public void Errors_on_window_closed()
        {
            Helpers.OpenPopup(_driver, _scope);
            var popUp = new BrowserWindow(Default.SessionConfiguration, new WindowFinder(_driver, "Pop Up Window", _scope, Default.Options),
                                          _driver, null, null, null, new ThrowsWhenMissingButNoDisambiguationStrategy());

            _driver.ExecuteScript("self.close();", popUp);
            Assert.Throws<MissingWindowException>(() => Helpers.FindPopUp(_driver));
        }
    }
}