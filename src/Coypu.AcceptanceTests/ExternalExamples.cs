using System;
using System.Text.RegularExpressions;
using Coypu.Drivers.Selenium;
using NUnit.Framework;
using OpenQA.Selenium;

namespace Coypu.AcceptanceTests
{
    [TestFixture]
    public class ExternalExamples
    {
        private SessionConfiguration _sessionConfiguration;
        private BrowserSession _browser;

        [SetUp]
        public void SetUp()
        {
            _sessionConfiguration = new SessionConfiguration
            {
                AppHost = "www.google.com",
                Driver = typeof(SeleniumWebDriver),
                Timeout = TimeSpan.FromSeconds(10)
            };

            _browser = new BrowserSession(_sessionConfiguration);
        }

        [TearDown]
        public void TearDown() => _browser.Dispose();

        [Test]
        public void AppHostContainsScheme()
        {
            _sessionConfiguration = new SessionConfiguration
            {
                AppHost = "https://www.google.co.uk/",
                Driver = typeof(SeleniumWebDriver)
            };

            using (var browser = new BrowserSession(_sessionConfiguration))
            {
                browser.Visit("/");
                Assert.That(browser.Location.ToString(), Is.EqualTo("https://www.google.co.uk/"));
            }
        }

        [Test, Explicit]
        public void Retries_Autotrader()
        {
            _browser.Visit("http://www.autotrader.co.uk/used-cars");

            _browser.FillIn("postcode").With("N1 1AA");

            _browser.FindField("make").Click();

            _browser.Select("citroen").From("make");
            _browser.Select("c4_grand_picasso").From("model");

            _browser.Select("National").From("radius");
            _browser.Select("diesel").From("fuel-type");
            _browser.Select("up_to_7_years_old").From("maximum-age");
            _browser.Select("up_to_60000_miles").From("maximum-mileage");

            _browser.FillIn("Add keyword").With("vtr");
        }


        [Test, Explicit]
        public void Visibility_NewTwitterLogin()
        {
            _browser.Visit("http://www.twitter.com");

            _browser.FillIn("session[username_or_email]").With("coyputester2");
            _browser.FillIn("session[password]").With("Nappybara");
        }

        [Test,
         Ignore("Make checkboxes on carbuzz are jumping around after you click each one. Re-enable when that is fixed")]
        public void FindingStuff_CarBuzz()
        {
            _browser.Visit("http://carbuzz.heroku.com/car_search");

            Console.WriteLine(_browser.FindSection("Make").Exists());
            Console.WriteLine(_browser.FindSection("Bake").Exists());

            _browser.Check("Audi");
            _browser.Check("BMW");
            _browser.Check("Mercedes");

            Assert.That(_browser.HasContentMatch(new Regex(@"\b83 car reviews found")));

            _browser.FindSection("Seats").Click();
            _browser.ClickButton("4");

            Assert.That(_browser.HasContentMatch(new Regex(@"\b28 car reviews found")));
        }

        [Test]
        public void HtmlUnitDriver()
        {
            _sessionConfiguration.AppHost = "www.google.com";
            _sessionConfiguration.Browser = Drivers.Browser.HtmlUnit;

            try
            {
                using (var htmlUnit = new BrowserSession(_sessionConfiguration))
                {
                    htmlUnit.Visit("/");
                }
                Assert.Fail("Expected an exception attempting to connect to HtmlUnit driver");
            }
            catch (WebDriverException e)
            {
                Assert.That(e.Message, Does.Contain("No connection could be made because the target machine actively refused it 127.0.0.1:4444"));
            }
        }
    }
}