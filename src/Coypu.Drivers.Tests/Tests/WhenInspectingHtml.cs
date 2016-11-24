using System.Text.RegularExpressions;
using NUnit.Framework;

namespace Coypu.Drivers.Tests.Tests
{
    [TestFixture]
    internal class WhenInspectingHtml
    {
        [OneTimeSetUp]
        public void Given() => DriverSpecs.DoSetUp(@"html\table.htm");

        [Test]
        public void FindsElementOuterHtml()
        {
            var outerHtml = Normalise(DriverSpecs.Css("table").OuterHTML);
            Assert.That(outerHtml, Is.EqualTo("<table><tbody><tr><th>name</th><th>age</th></tr><tr><td>bob</td><td>12</td></tr><tr><td>jane</td><td>79</td></tr></tbody></table>"));
        }

        [Test]
        public void FindsElementInnerHtml()
        {
            var innerHtml = Normalise(DriverSpecs.Css("table").InnerHTML);
            Assert.That(innerHtml, Is.EqualTo("<tbody><tr><th>name</th><th>age</th></tr><tr><td>bob</td><td>12</td></tr><tr><td>jane</td><td>79</td></tr></tbody>"));
        }

        [Test]
        public void FindsWindowOuterHtml()
        {
            var outerHtml = Normalise(DriverSpecs.Driver.Window.OuterHTML);
            Assert.That(outerHtml, Is.EqualTo("<html><head><title>table</title></head><body><table><tbody><tr><th>name</th><th>age</th></tr><tr><td>bob</td><td>12</td></tr><tr><td>jane</td><td>79</td></tr></tbody></table></body></html>"));
        }

        [Test]
        public void FindsWindowInnerHtml()
        {
            var innerHtml = Normalise(DriverSpecs.Driver.Window.InnerHTML);
            Assert.That(innerHtml, Is.EqualTo("<head><title>table</title></head><body><table><tbody><tr><th>name</th><th>age</th></tr><tr><td>bob</td><td>12</td></tr><tr><td>jane</td><td>79</td></tr></tbody></table></body>"));
        }

        private static string Normalise(string innerHtml)
        {
            return new Regex(@"\s+<", RegexOptions.Multiline).Replace(innerHtml, "<").Replace(" webdriver=\"true\"","").ToLower().TrimEnd();
        }
    }
}