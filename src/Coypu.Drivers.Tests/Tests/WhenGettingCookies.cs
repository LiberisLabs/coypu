using System;
using System.Linq;
using NUnit.Framework;

namespace Coypu.Drivers.Tests.Tests
{
    [TestFixture]
    internal class WhenGettingCookies
    {
        [OneTimeSetUp]
        public void Given() => DriverSpecs.VisitTestPage();

        [SetUp]
        public void SetUpCookies()
        {
            DriverSpecs.Driver.Visit(SomeRandomStaticHelpers.TestSiteUrl("/resource/cookie_test"), DriverSpecs.Root);
            DriverSpecs.Driver.ExecuteScript("document.cookie = 'cookie1=; expires=Fri, 27 Jul 2001 02:47:11 UTC; '", DriverSpecs.Root);
            DriverSpecs.Driver.ExecuteScript("document.cookie = 'cookie1=; expires=Fri, 27 Jul 2001 02:47:11 UTC;  path=/resource'", DriverSpecs.Root);
            DriverSpecs.Driver.ExecuteScript("document.cookie = 'cookie2=; expires=Fri, 27 Jul 2001 02:47:11 UTC; '", DriverSpecs.Root);
            DriverSpecs.Driver.Visit(SomeRandomStaticHelpers.TestSiteUrl("/resource/cookie_test"), DriverSpecs.Root);
        }

        [Test]
        public void Gets_all_the_session_cookies()
        {
            DriverSpecs.Driver.ExecuteScript("document.cookie = 'cookie1=value1; '", DriverSpecs.Root);
            DriverSpecs.Driver.ExecuteScript("document.cookie = 'cookie2=value2; '", DriverSpecs.Root);

            var cookies = DriverSpecs.Driver.GetBrowserCookies().ToArray();

            Assert.That("value1", Is.EqualTo(cookies.First(c => c.Name == "cookie1").Value));
            Assert.That("value2", Is.EqualTo(cookies.First(c => c.Name == "cookie2").Value));
        }

        [Test]
        public void Gets_all_the_persistent_cookies()
        {
            var expires = DateTime.UtcNow.AddDays(2);

            DriverSpecs.Driver.ExecuteScript($"document.cookie = 'cookie1=value11; expires={expires:R} '", DriverSpecs.Root);
            DriverSpecs.Driver.ExecuteScript($"document.cookie = 'cookie2=value22; expires={expires:R} '", DriverSpecs.Root);


            var cookies = DriverSpecs.Driver.GetBrowserCookies().ToArray();

            Assert.That("value11", Is.EqualTo(cookies.First(c => c.Name == "cookie1").Value));
            Assert.That("value22", Is.EqualTo(cookies.First(c => c.Name == "cookie2").Value));
        }

        // Internet Explorer fails this test - cookie information with path isn't available,
        // unless it's a persistent cookie that's been retrieved from the cache (and even then
        // the path value seems to be wrong?)
        [Test]
        public void Gets_the_cookie_path()
        {
            DriverSpecs.Driver.ExecuteScript("document.cookie = 'cookie1=value1; path=/resource'", DriverSpecs.Root);

            var cookies = DriverSpecs.Driver.GetBrowserCookies().ToArray();

            Assert.That("/resource", Is.EqualTo(cookies.First(c => c.Name == "cookie1").Path));
        }
    }
}