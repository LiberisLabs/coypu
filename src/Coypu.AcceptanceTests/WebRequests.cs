using System;
using System.IO;
using System.Text;
using Coypu.AcceptanceTests.Sites;
using NUnit.Framework;

namespace Coypu.AcceptanceTests
{
    [TestFixture]
    public class WebRequests
    {
        private SelfishSite _site;
        private BrowserSession _browser;

        [SetUp]
        public void SetUp()
        {
            _site = new SelfishSite();

            var configuration = new SessionConfiguration
            {
                Timeout = TimeSpan.FromMilliseconds(1000),
                Port = _site.BaseUri.Port
            };

            _browser = new BrowserSession(configuration);
            _browser.Visit("/");
        }

        [TearDown]
        public void TearDown()
        {
            _browser.Dispose();
            _site.Dispose();
        }

        [Test]
        public void It_saves_a_resource_from_the_web()
        {
            var saveAs = TempFileName();
            var expectedResource = Encoding.Default.GetBytes("bdd");

            _browser.SaveWebResource("/resource/bdd", saveAs);

            Assert.That(File.ReadAllBytes(saveAs), Is.EqualTo(expectedResource));
        }

        [Test]
        public void It_saves_a_restricted_file_from_a_site_you_are_logged_into()
        {
            var saveAs = TempFileName();
            const string expectedResource = "bdd";

            _browser.SaveWebResource("/restricted_resource/bdd", saveAs);
            Assert.That(File.ReadAllBytes(saveAs), Is.Not.EqualTo(expectedResource));

            _browser.Visit("/auto_login");

            _browser.SaveWebResource("/restricted_resource/bdd", saveAs);
            Assert.That(File.ReadAllText(saveAs), Is.EqualTo(expectedResource));
        }

        private static string TempFileName()
        {
            var saveAs = Path.GetTempFileName();
            if (File.Exists(saveAs))
            {
                File.Delete(saveAs);
            }
            return saveAs;
        }
    }
}