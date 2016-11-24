using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Coypu.Actions;
using Coypu.Finders;
using Coypu.Queries;
using Coypu.Timing;
using NUnit.Framework;


namespace Coypu.Drivers.Tests
{
    public static class SomeRandomStaticHelpers
    {
        public static string TestHtmlPathLocation(string testPage)
        {
            var file = new FileInfo(Path.Combine(@"..\..\", testPage)).FullName;
            return "file:///" + file.Replace('\\', '/');
        }

        public static string TestSiteUrl(string path)
        {
            return new Uri(Initialize.TestSite.BaseUri, path).AbsoluteUri;
        }
    }

    public class DriverSpecs
    {
        private class ImmediateSingleExecutionFakeTimingStrategy : TimingStrategy
        {
            public T Synchronise<T>(Query<T> query)
            {
                return query.Run();
            }

            public void TryUntil(BrowserAction tryThis, PredicateQuery until, Options options)
            {
                tryThis.Act();
            }

            public bool ZeroTimeout { get; set; }

            public void SetOverrideTimeout(TimeSpan timeout)
            {
            }

            public void ClearOverrideTimeout()
            {
            }
        }

        private static DriverScope _root;
        private static Driver _driver;

        [SetUp]
        public virtual void SetUp()
        {
            DoSetUp();
        }

        public static void DoSetUp(string testPage = @"html\InteractionTestsPage.htm")
        {
            Driver.Visit(SomeRandomStaticHelpers.TestHtmlPathLocation(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, testPage)), Root);
        }

        public static DriverScope Root => _root ?? (_root = new BrowserWindow(
            DefaultSessionConfiguration,
            new DocumentElementFinder(Driver, DefaultSessionConfiguration),
            null,
            new ImmediateSingleExecutionFakeTimingStrategy(),
            null,
            null,
            new ThrowsWhenMissingButNoDisambiguationStrategy()));

        public static readonly Options DefaultOptions = new Options();

        public static readonly SessionConfiguration DefaultSessionConfiguration = new SessionConfiguration
        {
            Browser = Browser.Chrome,
            Driver = typeof(Selenium.SeleniumWebDriver),
            TextPrecision = TextPrecision.Exact
        };

        private static Driver EnsureDriver()
        {
            var driverType = typeof(Selenium.SeleniumWebDriver);
            if (_driver != null && !_driver.Disposed)
            {
                if (driverType == _driver.GetType())
                    return _driver;

                _driver.Dispose();
            }

            _driver = (Driver) Activator.CreateInstance(driverType, Browser.Chrome);
            _root = null;

            return _driver;
        }

        public static Driver Driver => EnsureDriver();

        public static Driver Instance()
        {
            var driver = EnsureDriver();
            DoSetUp();
            return driver;
        }

        public static void DisposeDriver()
        {
            if (_driver != null && !_driver.Disposed)
            {
                _driver.Dispose();
            }
        }

        public static Element FindSingle(ElementFinder finder)
        {
            return new ThrowsWhenMissingButNoDisambiguationStrategy().ResolveQuery(finder);
        }

        public static Element Frame(string locator, DriverScope scope = null, Options options = null)
        {
            return FindSingle(new FrameFinder(Driver, locator, scope ?? Root, options ?? DefaultOptions));
        }

        public static Element Button(string locator, DriverScope scope = null, Options options = null)
        {
            return Button(Driver, locator, scope, options);
        }

        public static Element Button(Driver driver, string locator, DriverScope scope = null, Options options = null)
        {
            return FindSingle(new ButtonFinder(driver, locator, scope ?? Root, options ?? DefaultOptions));
        }

        public static Element Link(string locator, DriverScope scope = null, Options options = null)
        {
            return Link(Driver, locator, scope, options);
        }

        public static Element Link(Driver driver, string locator, DriverScope scope = null, Options options = null)
        {
            return FindSingle(new LinkFinder(driver, locator, scope ?? Root, options ?? DefaultOptions));
        }

        public static Element Id(string locator, DriverScope scope = null, Options options = null)
        {
            return FindSingle(new IdFinder(Driver, locator, scope ?? Root, options ?? DefaultOptions));
        }

        public static Element Field(string locator, DriverScope scope = null, Options options = null)
        {
            return Field(Driver, locator, scope, options);
        }

        public static Element Field(Driver driver, string locator, DriverScope scope = null, Options options = null)
        {
            return FindSingle(new FieldFinder(driver, locator, scope ?? Root, options ?? DefaultOptions));
        }

        public static Element XPath(string locator, DriverScope scope = null, Options options = null)
        {
            return FindSingle(new XPathFinder(Driver, locator, scope ?? Root, options ?? DefaultOptions));
        }

        public static Element Css(string locator, DriverScope scope = null, Options options = null)
        {
            return FindSingle(new CssFinder(Driver, locator, scope ?? Root, options ?? DefaultOptions));
        }

        public static Element Css(string locator, Regex text, DriverScope scope = null, Options options = null)
        {
            return FindSingle(new CssFinder(Driver, locator, scope ?? Root, options ?? DefaultOptions, text));
        }

        public static Element Section(string locator, DriverScope scope = null, Options options = null)
        {
            return FindSingle(new SectionFinder(Driver, locator, scope ?? Root, options ?? DefaultOptions));
        }

        public static Element Fieldset(string locator, DriverScope scope = null, Options options = null)
        {
            return FindSingle(new FieldsetFinder(Driver, locator, scope ?? Root, options ?? DefaultOptions));
        }

        public static Element Window(string locator, DriverScope scope = null, Options options = null)
        {
            return Window(Driver, locator, scope, options);
        }

        public static Element Window(Driver driver, string locator, DriverScope scope = null, Options options = null)
        {
            return FindSingle(new WindowFinder(driver, locator, scope ?? Root, options ?? DefaultOptions));
        }
    }

    public class ThrowsWhenMissingButNoDisambiguationStrategy : IDisambiguationStrategy
    {
        public Element ResolveQuery(ElementFinder elementFinder)
        {
            var all = elementFinder.Find(elementFinder.Options).ToArray();
            if (!all.Any())
                throw elementFinder.GetMissingException();

            return all.First();
        }
    }
}