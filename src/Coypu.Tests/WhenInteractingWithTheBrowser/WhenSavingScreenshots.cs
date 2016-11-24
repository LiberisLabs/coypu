﻿using System.Drawing.Imaging;
using System.Linq;
using NUnit.Framework;

namespace Coypu.Tests.WhenInteractingWithTheBrowser
{
    [TestFixture]
    public class WhenSavingScreenshots : BrowserInteractionTests
    {
        [Test]
        public void TakeScreenshot_acts_immediately_on_driver()
        {
            TakeScreenshot_acts_immediately_on_driver(popupScope);
        }

        [Test]
        public void TakeScreenshot_uses_current_window_scope()
        {
            TakeScreenshot_acts_immediately_on_driver(popupScope);
        }

        private void TakeScreenshot_acts_immediately_on_driver(BrowserWindow mainWindow)
        {
            mainWindow.SaveScreenshot("save-me-here.png", ImageFormat.Png);

            var saveScreenshotCall = driver.SaveScreenshotCalls.Single();

            Assert.That(saveScreenshotCall.Request, Is.EqualTo("save-me-here.png"));

            Assert.That(saveScreenshotCall.Scope, Is.EqualTo(mainWindow));
        }
    }
}