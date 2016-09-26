﻿using NUnit.Framework;

namespace Coypu.Drivers.Tests
{
    internal class When_finding_an_element_by_xpath : DriverSpecs
    {
        [Test]
        public void Finds_present_examples()
        {
            var shouldFind = "//*[@id = 'inspectingContent']//p[@class='css-test']/span";
            Assert.That(XPath(shouldFind).Text, Is.EqualTo("This"));

            shouldFind = "//ul[@id='cssTest']/li[3]";
            Assert.That(XPath(shouldFind).Text, Is.EqualTo("Me! Pick me!"));
        }

        [Test]
        public void Does_not_find_missing_examples()
        {
            const string shouldNotFind = "//*[@id = 'inspectingContent']//p[@class='css-missing-test']";
            Assert.Throws<MissingHtmlException>(() => XPath(shouldNotFind),
                                                "Expected not to find something at: " + shouldNotFind);
        }

        [Test]
        public void Only_finds_visible_elements()
        {
            const string shouldNotFind = "//*[@id = 'inspectingContent']//p[@class='css-test']/img";
            Assert.Throws<MissingHtmlException>(() => XPath(shouldNotFind),
                                                "Expected not to find something at: " + shouldNotFind);
        }
    }
}