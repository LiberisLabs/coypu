using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using NUnit.Framework;

namespace Coypu.AcceptanceTests
{
    [TestFixture]
    public class Screenshots : WaitAndRetryExamples
    {
        [Test]
        public void SavesToSpecifiedLocation()
        {
            Browser.Visit(Helper.GetProjectFile("html\\test-card.jpg"));
            Browser.ResizeTo(800, 600);

            SavesToSpecifiedLocation(Browser);
        }

        [Test]
        public void CapturesCorrectWindow()
        {
            Browser.ClickLink("Open pop up window");
            var popUp = Browser.FindWindow("Pop Up Window");
            popUp.Visit(Helper.GetProjectFile("html\\test-card.jpg"));
            popUp.ResizeTo(800, 600);

            // Do something in the main window
            Browser.FindCss("body").Click();

            SavesToSpecifiedLocation(popUp);
        }

        private static void SavesToSpecifiedLocation(BrowserWindow browserWindow)
        {
            const string fileName = "screenshot-test-card.jpg";
            try
            {
                browserWindow.SaveScreenshot(fileName, ImageFormat.Jpeg);

                Assert.That(File.Exists(fileName), "Expected screenshot saved to " + new FileInfo(fileName).FullName);
                using (var saved = Image.FromFile("screenshot-test-card.jpg"))
                {
                    var docWidth = float.Parse(browserWindow.ExecuteScript("return window.document.body.clientWidth;").ToString());
                    var docHeight = float.Parse(browserWindow.ExecuteScript("return window.document.body.clientHeight;").ToString());
                    Assert.That(saved.PhysicalDimension, Is.EqualTo(new SizeF(docWidth, docHeight)));
                }
            }
            finally
            {
                if (File.Exists(fileName))
                    File.Delete(fileName);
            }
        }
    }
}