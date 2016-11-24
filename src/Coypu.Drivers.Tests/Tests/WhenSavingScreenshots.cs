using System.Drawing;
using System.IO;
using NUnit.Framework;

namespace Coypu.Drivers.Tests.Tests
{
    [TestFixture]
    internal class WhenSavingScreenshots
    {
        [OneTimeSetUp]
        public void Given() => DriverSpecs.VisitTestPage(@"html\test-card.jpg");

        [Test]
        public void SavesAScreenshot()
        {
            DriverSpecs.Driver.ResizeTo(new Size(800, 600), DriverSpecs.Root);

            const string saveAs = "expect-saved-here.jpg";
            try
            {
                DriverSpecs.Driver.SaveScreenshot(saveAs, DriverSpecs.Root);
                Assert.That(File.Exists(saveAs), "Expected screenshot saved to " + new FileInfo(saveAs).FullName);
                using (var saved = Image.FromFile(saveAs))
                {
                    var docWidth = (long)DriverSpecs.Driver.ExecuteScript("return window.document.body.clientWidth;", DriverSpecs.Root);
                    var docHeight = (long)DriverSpecs.Driver.ExecuteScript("return window.document.body.clientHeight;", DriverSpecs.Root);
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