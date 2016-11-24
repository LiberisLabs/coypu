using System.Text.RegularExpressions;
using Coypu.Finders;

namespace Coypu.Drivers.Tests
{
    internal static class DriverHelpers
    {
        public static DriverScope WindowScope(Driver driver)
        {
            return new BrowserWindow(Default.SessionConfiguration,
                                     new DocumentElementFinder(driver, Default.SessionConfiguration),
                                     null,
                                     new ImmediateSingleExecutionFakeTimingStrategy(),
                                     null,
                                     null,
                                     new ThrowsWhenMissingButNoDisambiguationStrategy());
        }

        public static Element Button(Driver driver, string locator, DriverScope scope = null, Options options = null)
        {
            return FindSingle(new ButtonFinder(driver, locator, scope ?? WindowScope(driver), options ?? Default.Options));
        }

        public static Element Link(Driver driver, string locator, DriverScope scope = null, Options options = null)
        {
            return FindSingle(new LinkFinder(driver, locator, scope ?? WindowScope(driver), options ?? Default.Options));
        }

        public static Element Field(Driver driver, string locator, DriverScope scope = null, Options options = null)
        {
            return FindSingle(new FieldFinder(driver, locator, scope ?? WindowScope(driver), options ?? Default.Options));
        }

        public static Element Window(Driver driver, string locator, DriverScope scope = null, Options options = null)
        {
            return FindSingle(new WindowFinder(driver, locator, scope ?? WindowScope(driver), options ?? Default.Options));
        }

        public static Element Frame(Driver driver, string locator, DriverScope scope = null, Options options = null)
        {
            return FindSingle(new FrameFinder(driver, locator, scope ?? WindowScope(driver), options ?? Default.Options));
        }

        public static Element Id(Driver driver, string locator, DriverScope scope = null, Options options = null)
        {
            return FindSingle(new IdFinder(driver, locator, scope ?? WindowScope(driver), options ?? Default.Options));
        }

        public static Element XPath(Driver driver, string locator)
        {
            return FindSingle(new XPathFinder(driver, locator, WindowScope(driver), Default.Options));
        }

        public static Element Css(Driver driver, string locator)
        {
            return FindSingle(new CssFinder(driver, locator, WindowScope(driver), Default.Options));
        }

        public static Element Css(Driver driver, string locator, Regex text, DriverScope scope = null, Options options = null)
        {
            return FindSingle(new CssFinder(driver, locator, scope ?? WindowScope(driver), options ?? Default.Options, text));
        }

        public static Element Section(Driver driver, string locator)
        {
            return FindSingle(new SectionFinder(driver, locator, WindowScope(driver), Default.Options));
        }

        public static Element Fieldset(Driver driver, string locator)
        {
            return FindSingle(new FieldsetFinder(driver, locator, WindowScope(driver), Default.Options));
        }

        public static Element FindSingle(ElementFinder finder)
        {
            return new ThrowsWhenMissingButNoDisambiguationStrategy().ResolveQuery(finder);
        }
    }
}