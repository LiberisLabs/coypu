using System;
using System.Linq;
using NUnit.Framework;

namespace Coypu.Drivers.Tests.Tests
{
    [TestFixture]
    internal class WhenGettingCookies
    {
        private Driver _driver;
        private DriverScope _scope;

        [OneTimeSetUp]
        public void Given()
        {
            _driver = TestDriver.Instance();
            _scope = DriverHelpers.WindowScope(_driver);
        }

        [SetUp]
        public void SetUpCookies()
        {
            _driver.Visit(SomeRandomStaticHelpers.TestSiteUrl("/resource/cookie_test"), _scope);
            _driver.ExecuteScript("document.cookie = 'cookie1=; expires=Fri, 27 Jul 2001 02:47:11 UTC; '", _scope);
            _driver.ExecuteScript("document.cookie = 'cookie1=; expires=Fri, 27 Jul 2001 02:47:11 UTC;  path=/resource'", _scope);
            _driver.ExecuteScript("document.cookie = 'cookie2=; expires=Fri, 27 Jul 2001 02:47:11 UTC; '", _scope);
            _driver.Visit(SomeRandomStaticHelpers.TestSiteUrl("/resource/cookie_test"), _scope);
        }

        [Test]
        public void Gets_all_the_session_cookies()
        {
            _driver.ExecuteScript("document.cookie = 'cookie1=value1; '", _scope);
            _driver.ExecuteScript("document.cookie = 'cookie2=value2; '", _scope);

            var cookies = _driver.GetBrowserCookies().ToArray();

            Assert.That("value1", Is.EqualTo(cookies.First(c => c.Name == "cookie1").Value));
            Assert.That("value2", Is.EqualTo(cookies.First(c => c.Name == "cookie2").Value));
        }

        [Test]
        public void Gets_all_the_persistent_cookies()
        {
            var expires = DateTime.UtcNow.AddDays(2);

            _driver.ExecuteScript($"document.cookie = 'cookie1=value11; expires={expires:R} '", _scope);
            _driver.ExecuteScript($"document.cookie = 'cookie2=value22; expires={expires:R} '", _scope);


            var cookies = _driver.GetBrowserCookies().ToArray();

            Assert.That("value11", Is.EqualTo(cookies.First(c => c.Name == "cookie1").Value));
            Assert.That("value22", Is.EqualTo(cookies.First(c => c.Name == "cookie2").Value));
        }

        // Internet Explorer fails this test - cookie information with path isn't available,
        // unless it's a persistent cookie that's been retrieved from the cache (and even then
        // the path value seems to be wrong?)
        [Test]
        public void Gets_the_cookie_path()
        {
            _driver.ExecuteScript("document.cookie = 'cookie1=value1; path=/resource'", _scope);

            var cookies = _driver.GetBrowserCookies().ToArray();

            Assert.That("/resource", Is.EqualTo(cookies.First(c => c.Name == "cookie1").Path));
        }
    }
}