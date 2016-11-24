using System.Drawing;
using System.IO;
using NUnit.Framework;

namespace Coypu.Drivers.Tests.Tests
{
    [TestFixture]
    internal class WhenSavingScreenshots
    {
        private Driver _driver;
        private DriverScope _scope;

        [OneTimeSetUp]
        public void Given()
        {
            _driver = TestDriver.Instance(@"html\test-card.jpg");
            _scope = DriverHelpers.WindowScope(_driver);
        }

        [Test]
        public void SavesAScreenshot()
        {
            _driver.ResizeTo(new Size(800, 600), _scope);

            const string saveAs = "expect-saved-here.jpg";
            try
            {
                _driver.SaveScreenshot(saveAs, _scope);
                Assert.That(File.Exists(saveAs), "Expected screenshot saved to " + new FileInfo(saveAs).FullName);
                using (var saved = Image.FromFile(saveAs))
                {
                    var docWidth = (long)_driver.ExecuteScript("return window.document.body.clientWidth;", _scope);
                    var docHeight = (long)_driver.ExecuteScript("return window.document.body.clientHeight;", _scope);
                    Assert.That(saved.PhysicalDimension.Width, Is.InRange(docWidth - 10, docWidth));
                    Assert.That(saved.PhysicalDimension.Height, Is.InRange(docHeight - 75, docHeight));
                }
            }
            finally
            {
                if (File.Exists(saveAs))
                    File.Delete(saveAs);
            }
        }
    }
}