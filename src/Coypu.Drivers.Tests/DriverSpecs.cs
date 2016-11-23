using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Coypu.Actions;
using Coypu.Drivers.Tests.Sites;
using Coypu.Finders;
using Coypu.Queries;
using Coypu.Timing;
using NUnit.Framework;

[SetUpFixture]
public class AssemblyTearDown
{
    public static SelfishSite TestSite;

    [SetUp]
    public void StartTestSite()
    {
        TestSite = new SelfishSite();
    }

    [TearDown]
    public void TearDown()
    {
        TestSite.Dispose();

        Coypu.Drivers.Tests.DriverSpecs.DisposeDriver();
    }
}

namespace Coypu.Drivers.Tests
{
    public class TestDriver : IDisposable
    {
        public class ThrowsWhenMissingButNoDisambiguationStrategy : DisambiguationStrategy
        {
            public Element ResolveQuery(ElementFinder elementFinder)
            {
                var all = elementFinder.Find(elementFinder.Options).ToArray();
                if (!all.Any())
                    throw elementFinder.GetMissingException();

                return all.First();
            }
        }

        public class ImmediateSingleExecutionFakeTimingStrategy : TimingStrategy
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

        private Driver _driver;
        private readonly Type _driverType = typeof(Selenium.SeleniumWebDriver);
        private DriverScope _root;
        private readonly Browser _browser = Browser.Chrome;

        public static TestDriver Create()
        {
            var d = new TestDriver();
            d.EnsureDriver();
            return d;
        }

        internal void EnsureDriver()
        {
            if (_driver != null && !_driver.Disposed)
            {
                if (_driverType == _driver.GetType())
                    return;

                _driver.Dispose();
            }

            _driver = (Driver) Activator.CreateInstance(_driverType, _browser);
            _root = null;
        }

        public Driver Driver
        {
            get
            {
                EnsureDriver();
                return _driver;
            }
        }

        public void Dispose()
        {
            if (_driver != null && !_driver.Disposed)
            {
                _driver.Dispose();
            }
        }

        protected DriverScope Root
        {
            get
            {
                var sessionConfiguration = new SessionConfiguration
                {
                    Browser = _browser,
                    Driver = _driverType,
                    TextPrecision = TextPrecision.Exact
                };

                return _root ?? (_root = new BrowserWindow(sessionConfiguration,
                                                           new DocumentElementFinder(Driver, sessionConfiguration),
                                                           null,
                                                           new ImmediateSingleExecutionFakeTimingStrategy(),
                                                           null,
                                                           null,
                                                           new ThrowsWhenMissingButNoDisambiguationStrategy()));
            }
        }

        protected readonly Options DefaultOptions = new Options();

        protected readonly DisambiguationStrategy DisambiguationStrategy = new DriverSpecs.ThrowsWhenMissingButNoDisambiguationStrategy();

        protected Element FindSingle(ElementFinder finder)
        {
            return DisambiguationStrategy.ResolveQuery(finder);
        }

        protected Element Frame(string locator, DriverScope scope = null, Options options = null)
        {
            return FindSingle(new FrameFinder(Driver, locator, scope ?? Root, options ?? DefaultOptions));
        }

        protected Element Button(string locator, DriverScope scope = null, Options options = null)
        {
            return FindSingle(new ButtonFinder(Driver, locator, scope ?? Root, options ?? DefaultOptions));
        }

        protected Element Link(string locator, DriverScope scope = null, Options options = null)
        {
            return FindSingle(new LinkFinder(Driver, locator, scope ?? Root, options ?? DefaultOptions));
        }

        protected Element Id(string locator, DriverScope scope = null, Options options = null)
        {
            return FindSingle(new IdFinder(Driver, locator, scope ?? Root, options ?? DefaultOptions));
        }

        protected Element Field(string locator, DriverScope scope = null, Options options = null)
        {
            return FindSingle(new FieldFinder(Driver, locator, scope ?? Root, options ?? DefaultOptions));
        }

        protected Element XPath(string locator, DriverScope scope = null, Options options = null)
        {
            return FindSingle(new XPathFinder(Driver, locator, scope ?? Root, options ?? DefaultOptions));
        }

        protected Element XPath(string locator, Regex text, DriverScope scope = null, Options options = null)
        {
            return FindSingle(new XPathFinder(Driver, locator, scope ?? Root, options ?? DefaultOptions, text));
        }

        protected Element XPath(string locator, string text, DriverScope scope = null, Options options = null)
        {
            return FindSingle(new XPathFinder(Driver, locator, scope ?? Root, options ?? DefaultOptions, text));
        }

        protected Element Css(string locator, DriverScope scope = null, Options options = null)
        {
            return FindSingle(new CssFinder(Driver, locator, scope ?? Root, options ?? DefaultOptions));
        }

        protected Element Css(string locator, Regex text, DriverScope scope = null, Options options = null)
        {
            return FindSingle(new CssFinder(Driver, locator, scope ?? Root, options ?? DefaultOptions, text));
        }

        protected Element Css(string locator, string text, DriverScope scope = null, Options options = null)
        {
            return FindSingle(new CssFinder(Driver, locator, scope ?? Root, options ?? DefaultOptions, text));
        }

        protected Element Section(string locator, DriverScope scope = null, Options options = null)
        {
            return FindSingle(new SectionFinder(Driver, locator, scope ?? Root, options ?? DefaultOptions));
        }

        protected Element Fieldset(string locator, DriverScope scope = null, Options options = null)
        {
            return FindSingle(new FieldsetFinder(Driver, locator, scope ?? Root, options ?? DefaultOptions));
        }

        protected Element Window(string locator, DriverScope scope = null, Options options = null)
        {
            return FindSingle(new WindowFinder(Driver, locator, scope ?? Root, options ?? DefaultOptions));
        }
    }

    public class DriverSpecs
    {
        public class ThrowsWhenMissingButNoDisambiguationStrategy : DisambiguationStrategy
        {
            public Element ResolveQuery(ElementFinder elementFinder)
            {
                var all = elementFinder.Find(elementFinder.Options).ToArray();
                if (!all.Any())
                    throw elementFinder.GetMissingException();

                return all.First();
            }
        }

        public class ImmediateSingleExecutionFakeTimingStrategy : TimingStrategy
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

        protected string TestSiteUrl(string path)
        {
            return new Uri(AssemblyTearDown.TestSite.BaseUri, path).AbsoluteUri;
        }

        private const string INTERACTION_TESTS_PAGE = @"html\InteractionTestsPage.htm";
        private static DriverScope root;
        private static Driver driver;

        private static readonly Browser browser = Browser.Chrome;
        private static readonly Type driverType = typeof(Selenium.SeleniumWebDriver);

//        private static readonly Browser browser = Browser.InternetExplorer;
//        private static readonly Type driverType = typeof (Watin.WatiNDriver);

        [SetUp]
        public virtual void SetUp()
        {
            Driver.Visit(GetTestHTMLPathLocation(), Root);
        }

        protected string GetTestHTMLPathLocation()
        {
            return TestHtmlPathLocation(TestPage);
        }

        protected static string TestHtmlPathLocation(string testPage)
        {
            var file = new FileInfo(Path.Combine(@"..\..\", testPage)).FullName;
            return "file:///" + file.Replace('\\', '/');
        }

        protected virtual string TestPage => INTERACTION_TESTS_PAGE;

        protected static DriverScope Root => root ?? (root = new BrowserWindow(
            DefaultSessionConfiguration,
            new DocumentElementFinder(Driver, DefaultSessionConfiguration),
            null,
            new ImmediateSingleExecutionFakeTimingStrategy(),
            null,
            null,
            new ThrowsWhenMissingButNoDisambiguationStrategy()));

        protected static readonly Options DefaultOptions = new Options();

        protected static readonly SessionConfiguration DefaultSessionConfiguration = new SessionConfiguration
        {
            Browser = browser,
            Driver = driverType,
            TextPrecision = TextPrecision.Exact
        };

        private static void EnsureDriver()
        {
            if (driver != null && !driver.Disposed)
            {
                if (driverType == driver.GetType())
                    return;

                driver.Dispose();
            }

            driver = (Driver) Activator.CreateInstance(driverType, browser);
            root = null;
        }

        public static Driver Driver
        {
            get
            {
                EnsureDriver();
                return driver;
            }
        }

        public static void DisposeDriver()
        {
            if (driver != null && !driver.Disposed)
            {
                driver.Dispose();
            }
        }

        protected static readonly DisambiguationStrategy DisambiguationStrategy = new ThrowsWhenMissingButNoDisambiguationStrategy();

        protected static Element FindSingle(ElementFinder finder)
        {
            return DisambiguationStrategy.ResolveQuery(finder);
        }

        protected static Element Frame(string locator, DriverScope scope = null, Options options = null)
        {
            return FindSingle(new FrameFinder(Driver, locator, scope ?? Root, options ?? DefaultOptions));
        }

        protected static Element Button(string locator, DriverScope scope = null, Options options = null)
        {
            return FindSingle(new ButtonFinder(Driver, locator, scope ?? Root, options ?? DefaultOptions));
        }

        protected static Element Link(string locator, DriverScope scope = null, Options options = null)
        {
            return FindSingle(new LinkFinder(Driver, locator, scope ?? Root, options ?? DefaultOptions));
        }

        protected static Element Id(string locator, DriverScope scope = null, Options options = null)
        {
            return FindSingle(new IdFinder(Driver, locator, scope ?? Root, options ?? DefaultOptions));
        }

        protected static Element Field(string locator, DriverScope scope = null, Options options = null)
        {
            return FindSingle(new FieldFinder(Driver, locator, scope ?? Root, options ?? DefaultOptions));
        }

        protected static Element XPath(string locator, DriverScope scope = null, Options options = null)
        {
            return FindSingle(new XPathFinder(Driver, locator, scope ?? Root, options ?? DefaultOptions));
        }

        protected static Element XPath(string locator, Regex text, DriverScope scope = null, Options options = null)
        {
            return FindSingle(new XPathFinder(Driver, locator, scope ?? Root, options ?? DefaultOptions, text));
        }

        protected static Element XPath(string locator, string text, DriverScope scope = null, Options options = null)
        {
            return FindSingle(new XPathFinder(Driver, locator, scope ?? Root, options ?? DefaultOptions, text));
        }

        protected static Element Css(string locator, DriverScope scope = null, Options options = null)
        {
            return FindSingle(new CssFinder(Driver, locator, scope ?? Root, options ?? DefaultOptions));
        }

        protected static Element Css(string locator, Regex text, DriverScope scope = null, Options options = null)
        {
            return FindSingle(new CssFinder(Driver, locator, scope ?? Root, options ?? DefaultOptions, text));
        }

        protected static Element Css(string locator, string text, DriverScope scope = null, Options options = null)
        {
            return FindSingle(new CssFinder(Driver, locator, scope ?? Root, options ?? DefaultOptions, text));
        }

        protected static Element Section(string locator, DriverScope scope = null, Options options = null)
        {
            return FindSingle(new SectionFinder(Driver, locator, scope ?? Root, options ?? DefaultOptions));
        }

        protected static Element Fieldset(string locator, DriverScope scope = null, Options options = null)
        {
            return FindSingle(new FieldsetFinder(Driver, locator, scope ?? Root, options ?? DefaultOptions));
        }

        protected static Element Window(string locator, DriverScope scope = null, Options options = null)
        {
            return FindSingle(new WindowFinder(Driver, locator, scope ?? Root, options ?? DefaultOptions));
        }
    }
}