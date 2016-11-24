using System.Text.RegularExpressions;
using NUnit.Framework;

namespace Coypu.Drivers.Tests.Tests
{
    [TestFixture]
    internal class WhenInspectingHtml
    {
        private Driver _driver;

        [OneTimeSetUp]
        public void Given() => _driver = TestDriver.Instance(@"html\table.htm");

        [Test]
        public void FindsElementOuterHtml()
        {
            var outerHtml = Normalise(DriverHelpers.Css(_driver, "table").OuterHTML);
            Assert.That("<table><tbody><tr><th>name</th><th>age</th></tr><tr><td>bob</td><td>12</td></tr><tr><td>jane</td><td>79</td></tr></tbody></table>", Is.EqualTo(outerHtml));
        }

        [Test]
        public void FindsElementInnerHtml()
        {
            var innerHtml = Normalise(DriverHelpers.Css(_driver, "table").InnerHTML);
            Assert.That("<tbody><tr><th>name</th><th>age</th></tr><tr><td>bob</td><td>12</td></tr><tr><td>jane</td><td>79</td></tr></tbody>", Is.EqualTo(innerHtml));
        }

        [Test]
        public void FindsWindowOuterHtml()
        {
            var outerHtml = Normalise(_driver.Window.OuterHTML);
            Assert.That("<html><head><title>table</title></head><body><table><tbody><tr><th>name</th><th>age</th></tr><tr><td>bob</td><td>12</td></tr><tr><td>jane</td><td>79</td></tr></tbody></table></body></html>", Is.EqualTo(outerHtml));
        }

        [Test]
        public void FindsWindowInnerHtml()
        {
            var innerHtml = Normalise(_driver.Window.InnerHTML);
            Assert.That("<head><title>table</title></head><body><table><tbody><tr><th>name</th><th>age</th></tr><tr><td>bob</td><td>12</td></tr><tr><td>jane</td><td>79</td></tr></tbody></table></body>", Is.EqualTo(innerHtml));
        }

        private static string Normalise(string innerHtml)
        {
            return new Regex(@"\s+<", RegexOptions.Multiline).Replace(innerHtml, "<").Replace(" webdriver=\"true\"","").ToLower().TrimEnd();
        }
    }
}