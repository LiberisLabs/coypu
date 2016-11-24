using System;
using Coypu.Finders;
using NUnit.Framework;

namespace Coypu.Drivers.Tests.Tests
{
    [TestFixture]
    internal class WhenInteractingWithDialogs
    {
        private Driver _driver;

        [SetUp]
        public void Given() => _driver = DriverSpecs.Instance();

        [TearDown]
        public void Kill() => _driver.Dispose();

        [Test]
        public void Accepts_alerts()
        {
            _driver.Click(DriverHelpers.Link(_driver, "Trigger an alert"));
            Assert.That(_driver.HasDialog("You have triggered an alert and this is the text.", DriverSpecs.Root), Is.True);
            _driver.AcceptModalDialog(DriverSpecs.Root);
            Assert.That(_driver.HasDialog("You have triggered an alert and this is the text.", DriverSpecs.Root), Is.False);
        }

        [Test]
        public void Clears_dialog()
        {
            _driver.Click(DriverHelpers.Link(_driver, "Trigger a confirm"));
            Assert.That(_driver.HasDialog("You have triggered a confirm and this is the text.", DriverSpecs.Root), Is.True);
            _driver.AcceptModalDialog(DriverSpecs.Root);
            Assert.That(_driver.HasDialog("You have triggered a confirm and this is the text.", DriverSpecs.Root), Is.False);
        }

        [Test]
        public void Missing_dialog_throws_coypu_exception()
        {
            Assert.Throws<MissingDialogException>(() => _driver.AcceptModalDialog(DriverSpecs.Root));
            Assert.Throws<MissingDialogException>(() => _driver.CancelModalDialog(DriverSpecs.Root));
        }

        [Test]
        public void Returns_true()
        {
            _driver.Click(DriverHelpers.Link(_driver, "Trigger a confirm"));
            _driver.AcceptModalDialog(DriverSpecs.Root);
            Assert.That(DriverHelpers.Link(_driver, "Trigger a confirm - accepted", DriverSpecs.Root), Is.Not.Null);
        }


        [Test]
        public void Cancel_Clears_dialog()
        {
            _driver.Click(DriverHelpers.Link(_driver, "Trigger a confirm"));
            Assert.That(_driver.HasDialog("You have triggered a confirm and this is the text.", DriverSpecs.Root), Is.True);
            _driver.CancelModalDialog(DriverSpecs.Root);
            Assert.That(_driver.HasDialog("You have triggered a confirm and this is the text.", DriverSpecs.Root), Is.False);
        }

        [Test]
        public void Cancel_Returns_false()
        {
            _driver.Click(DriverHelpers.Link(_driver, "Trigger a confirm"));
            _driver.CancelModalDialog(DriverSpecs.Root);

            DriverHelpers.Link(_driver, "Trigger a confirm - cancelled");
        }

        // IE can't do this
        [Test]
        public void Finds_scope_first_for_alerts()
        {
            _driver.Click(DriverHelpers.Link(_driver, "Open pop up window"));
            var popUp = new BrowserWindow(Default.SessionConfiguration, new WindowFinder(_driver, "Pop Up Window", DriverSpecs.Root, Default.Options), _driver, null, null, null,
                                          new ThrowsWhenMissingButNoDisambiguationStrategy());
            Assert.That("Pop Up Window", Is.EqualTo(_driver.Title(popUp)));

            _driver.ExecuteScript("window.setTimeout(function() {document.getElementById('alertTriggerLink').click();},200);", DriverSpecs.Root);
            Assert.That("Pop Up Window", Is.EqualTo(_driver.Title(popUp)));

            System.Threading.Thread.Sleep(1000);
            _driver.AcceptModalDialog(DriverSpecs.Root);
            Assert.That(_driver.HasDialog("You have triggered a alert and this is the text.", DriverSpecs.Root), Is.False);
        }

        // IE can't do this
        [Test]
        public void Finds_scope_first_for_confirms()
        {
            _driver.Click(DriverHelpers.Link(_driver, "Open pop up window"));
            var popUp = new BrowserWindow(Default.SessionConfiguration, new WindowFinder(_driver, "Pop Up Window", DriverSpecs.Root, Default.Options), _driver, null, null, null,
                                          new ThrowsWhenMissingButNoDisambiguationStrategy());
            Assert.That("Pop Up Window", Is.EqualTo(_driver.Title(popUp)));

            _driver.ExecuteScript("window.setTimeout(function() {document.getElementById('confirmTriggerLink').click();},500);", DriverSpecs.Root);
            Assert.That("Pop Up Window", Is.EqualTo(_driver.Title(popUp)));
            try
            {
                _driver.ExecuteScript("self.close();", popUp);
            }
            catch (Exception)
            {
                // IE permissions
            }

            System.Threading.Thread.Sleep(500);
            _driver.CancelModalDialog(DriverSpecs.Root);
            Assert.That(_driver.HasDialog("You have triggered a confirm and this is the text.", DriverSpecs.Root), Is.False);
        }
    }
}