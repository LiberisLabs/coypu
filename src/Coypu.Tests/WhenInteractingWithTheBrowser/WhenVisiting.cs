﻿using System.Linq;
using NUnit.Framework;

namespace Coypu.Tests.WhenInteractingWithTheBrowser
{
    [TestFixture]
    public class WhenVisiting : BrowserInteractionTests
    {
        [Test]
        public void It_uses_a_fully_qualified_url_from_the_url_builder()
        {
            stubUrlBuilder.SetStubUrl("/some/resource", "http://blank.org");
            browserSession.Visit("/some/resource");
            Assert.That(driver.Visits.Single().Request, Is.EqualTo("http://blank.org"));
            Assert.That(driver.Visits.Single().Scope, Is.EqualTo(browserSession));
        }

        [Test]
        public void It_uses_the_current_scope()
        {
            stubUrlBuilder.SetStubUrl("/some/resource", "http://blank.org");
            popupScope.Visit("/some/resource");

            Assert.That(driver.Visits.Single().Request, Is.EqualTo("http://blank.org"));
            Assert.That(driver.Visits.Single().Scope, Is.EqualTo(popupScope));
        }
    }
}