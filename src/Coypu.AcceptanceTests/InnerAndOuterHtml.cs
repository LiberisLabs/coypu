using System;
using System.Text.RegularExpressions;
using NUnit.Framework;

namespace Coypu.AcceptanceTests
{
    [TestFixture]
    public class InnerAndOuterHtml
    {
        private SessionConfiguration _sessionConfiguration;
        private BrowserSession _browser;

        [OneTimeSetUp]
        public void SetUpFixture()
        {
            _sessionConfiguration = new SessionConfiguration {Timeout = TimeSpan.FromMilliseconds(1000)};
            _browser = new BrowserSession(_sessionConfiguration);
            _browser.Visit(Helper.GetProjectFile(@"html\table.htm"));
        }

        [OneTimeTearDown]
        public void TearDownFixture()
        {
            _browser.Dispose();
        }

        [Test]
        public void GrabsTheOuterHtmlFromAnElement()
        {
            var outerHtml = Normalise(_browser.FindCss("table").OuterHTML);
            Assert.That(outerHtml, Is.EqualTo("<table><tbody><tr><th>name</th><th>age</th></tr><tr><td>bob</td><td>12</td></tr><tr><td>jane</td><td>79</td></tr></tbody></table>"));
        }

        [Test]
        public void GrabsTheInnerHtmlFromAnElement()
        {
            var innerHtml = Normalise(_browser.FindCss("table").InnerHTML);
            Assert.That(innerHtml, Is.EqualTo("<tbody><tr><th>name</th><th>age</th></tr><tr><td>bob</td><td>12</td></tr><tr><td>jane</td><td>79</td></tr></tbody>"));
        }

        private static string Normalise(string innerHtml)
        {
            return new Regex(@"\s+", RegexOptions.Multiline).Replace(innerHtml, "").ToLower();
        }
    }
}
