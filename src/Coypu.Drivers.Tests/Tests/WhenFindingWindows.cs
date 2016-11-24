using Coypu.Finders;
using NUnit.Framework;

namespace Coypu.Drivers.Tests.Tests
{
    [TestFixture]
    internal class WhenFindingWindows
    {
        private static class Helpers
        {
            public static void OpenPopup(IDriver driver, DriverScope scope)
            {
                driver.Click(FindPopUpLink(driver, scope));
            }

            public static void OpenPopup2(IDriver driver, DriverScope scope)
            {
                driver.Click(FindPopUp2Link(driver, scope));
            }

            public static IElement FindPopUpLink(IDriver driver, DriverScope scope)
            {
                return DriverHelpers.Link(driver, "Open pop up window", scope, Default.Options);
            }

            public static IElement FindPopUp2Link(IDriver driver, DriverScope scope)
            {
                return DriverHelpers.Link(driver, "Open pop up window 2", scope, Default.Options);
            }

            public static IElement FindPopUp(IDriver driver, DriverScope scope)
            {
                return FindWindow(driver, "Pop Up Window", scope);
            }

            public static IElement FindWindow(IDriver driver, string locator, DriverScope scope)
            {
                return DriverHelpers.Window(driver, locator, scope, Default.Options);
            }
        }

        private IDriver _driver;
        private DriverScope _scope;

        [SetUp]
        public void Given()
        {
            _driver = TestDriver.Instance();
            _scope = DriverHelpers.WindowScope(_driver);
        }

        [TearDown]
        public void Kill() => _driver.Dispose();

        [Test, Explicit("Occasionally fails on appveyor")]
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
            Assert.That(Helpers.FindPopUp(_driver, _scope).Text, Does.Contain("I am a pop up window"));

            Helpers.FindPopUpLink(_driver, _scope);
        }

        [Test, Explicit("Occasionally fails on appveyor")]
        public void Finds_by_substring_title()
        {
            Helpers.OpenPopup2(_driver, _scope);
            Assert.That(Helpers.FindPopUp(_driver, _scope).Text, Does.Contain("I am a pop up window 2"));
            Helpers.FindPopUp2Link(_driver, _scope);
        }

        [Test]
        public void Finds_by_exact_title_over_substring()
        {
            Helpers.OpenPopup(_driver, _scope);
            Helpers.OpenPopup2(_driver, _scope);
            Assert.That(Helpers.FindPopUp(_driver, _scope).Text, Does.Contain("I am a pop up window"));

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
            Assert.Throws<MissingWindowException>(() => Helpers.FindPopUp(_driver, _scope));
        }
    }
}