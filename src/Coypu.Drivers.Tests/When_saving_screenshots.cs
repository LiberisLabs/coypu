﻿using System.Drawing;
using System.IO;
using NUnit.Framework;

namespace Coypu.Drivers.Tests
{
    internal class WhenSavingScreenshots : DriverSpecs
    {
        [Test]
        public void SavesAScreenshot()
        {
            Scope driverScope = Root;
            Driver.Visit(TestHtmlPathLocation("html\\test-card.jpg"), driverScope);
            Driver.ResizeTo(new Size(800, 600), driverScope);

            const string saveAs = "expect-saved-here.jpg";
            try
            {
                Driver.SaveScreenshot(saveAs, driverScope);
                Assert.That(File.Exists(saveAs), "Expected screenshot saved to " + new FileInfo(saveAs).FullName);
                using (var saved = Image.FromFile(saveAs))
                {
                    var docWidth = (long) Driver.ExecuteScript("return window.document.body.clientWidth;", driverScope);
                    var docHeight = (long) Driver.ExecuteScript("return window.document.body.clientHeight;", driverScope);
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