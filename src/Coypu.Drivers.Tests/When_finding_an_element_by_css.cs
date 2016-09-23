using System.Text.RegularExpressions;
using NUnit.Framework;

namespace Coypu.Drivers.Tests
{
    internal class When_finding_an_element_by_css : DriverSpecs
    { 
            [Test]
            public void Finds_present_examples()
            {
                var shouldFind = "#inspectingContent p.css-test span";
                Assert.That(Css(shouldFind).Text, Is.EqualTo("This"));

                shouldFind = "ul#cssTest li:nth-child(3)";
                Assert.That(Css(shouldFind).Text, Is.EqualTo("Me! Pick me!"));
            }

            [Test]
            public void Finds_present_examples_by_text()
            {
                var shouldFind = "#inspectingContent p.css-test span";
                Assert.That(Css(shouldFind, new Regex("^This$")).Text, Is.EqualTo("This"));

                shouldFind = "ul#cssTest li:nth-child(3)";
                Assert.That(Css(shouldFind, new Regex("Pick me")).Text, Is.EqualTo("Me! Pick me!"));
            }

            [Test]
            public void Does_not_find_missing_examples()
            {
                const string shouldNotFind = "#inspectingContent p.css-missing-test";
                Assert.Throws<MissingHtmlException>(() => Css(shouldNotFind, Root), "Expected not to find something at: " + shouldNotFind);
            }

            [Test]
            public void Does_not_find_missing_examples_by_text()
            {
                const string shouldFind = "ul#cssTest li:nth-child(3)";
                var missingTextPattern = new Regex("Drop me");
                Assert.Throws<MissingHtmlException>(() => Css(shouldFind, missingTextPattern), string.Format("Expected not to find something at: {0} with text: ", shouldFind, missingTextPattern));
            }

            
            [Test]
            public void Only_finds_visible_elements()
            {
                const string shouldNotFind = "#inspectingContent p.css-test img.invisible";
                Assert.Throws<MissingHtmlException>(() => Css(shouldNotFind,Root), "Expected not to find something at: " + shouldNotFind);
            }
        }
    
}