using System.Text.RegularExpressions;
using NUnit.Framework;

namespace Coypu.Drivers.Tests.Tests
{
    [TestFixture]
    internal class WhenInspectingHtml
    {
        [OneTimeSetUp]
        public void Given() => DriverSpecs.VisitTestPage(@"html\table.htm");

        [Test]
        public void FindsElementOuterHtml()
        {
            var outerHtml = Normalise(DriverHelpers.Css(DriverSpecs.Driver, "table").OuterHTML);
            Assert.That("<table><tbody><tr><th>name</th><th>age</th></tr><tr><td>bob</td><td>12</td></tr><tr><td>jane</td><td>79</td></tr></tbody></table>", Is.EqualTo(outerHtml));
        }

        [Test]
        public void FindsElementInnerHtml()
        {
            var innerHtml = Normalise(DriverHelpers.Css(DriverSpecs.Driver, "table").InnerHTML);
            Assert.That("<tbody><tr><th>name</th><th>age</th></tr><tr><td>bob</td><td>12</td></tr><tr><td>jane</td><td>79</td></tr></tbody>", Is.EqualTo(innerHtml));
        }

        [Test]
        public void FindsWindowOuterHtml()
        {
            var outerHtml = Normalise(DriverSpecs.Driver.Window.OuterHTML);
            Assert.That("<html><head><title>table</title></head><body><table><tbody><tr><th>name</th><th>age</th></tr><tr><td>bob</td><td>12</td></tr><tr><td>jane</td><td>79</td></tr></tbody></table></body></html>", Is.EqualTo(outerHtml));
        }

        [Test]
        public void FindsWindowInnerHtml()
        {
            var innerHtml = Normalise(DriverSpecs.Driver.Window.InnerHTML);
            Assert.That("<head><title>table</title></head><body><table><tbody><tr><th>name</th><th>age</th></tr><tr><td>bob</td><td>12</td></tr><tr><td>jane</td><td>79</td></tr></tbody></table></body>", Is.EqualTo(innerHtml));
        }

        private static string Normalise(string innerHtml)
        {
            return new Regex(@"\s+<", RegexOptions.Multiline).Replace(innerHtml, "<").Replace(" webdriver=\"true\"","").ToLower().TrimEnd();
        }
    }
}