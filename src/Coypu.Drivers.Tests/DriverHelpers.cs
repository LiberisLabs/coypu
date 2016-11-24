using System.Text.RegularExpressions;
using Coypu.Finders;

namespace Coypu.Drivers.Tests
{
    internal static class DriverHelpers
    {
        public static DriverScope WindowScope(IDriver driver)
        {
            return new BrowserWindow(Default.SessionConfiguration,
                                     new DocumentElementFinder(driver, Default.SessionConfiguration),
                                     null,
                                     new ImmediateSingleExecutionFakeTimingStrategy(),
                                     null,
                                     null,
                                     new ThrowsWhenMissingButNoDisambiguationStrategy());
        }

        public static IElement Button(IDriver driver, string locator, DriverScope scope = null, Options options = null)
        {
            return FindSingle(new ButtonFinder(driver, locator, scope ?? WindowScope(driver), options ?? Default.Options));
        }

        public static IElement Link(IDriver driver, string locator, DriverScope scope = null, Options options = null)
        {
            return FindSingle(new LinkFinder(driver, locator, scope ?? WindowScope(driver), options ?? Default.Options));
        }

        public static IElement Field(IDriver driver, string locator, DriverScope scope = null, Options options = null)
        {
            return FindSingle(new FieldFinder(driver, locator, scope ?? WindowScope(driver), options ?? Default.Options));
        }

        public static IElement Window(IDriver driver, string locator, DriverScope scope = null, Options options = null)
        {
            return FindSingle(new WindowFinder(driver, locator, scope ?? WindowScope(driver), options ?? Default.Options));
        }

        public static IElement Frame(IDriver driver, string locator, DriverScope scope = null, Options options = null)
        {
            return FindSingle(new FrameFinder(driver, locator, scope ?? WindowScope(driver), options ?? Default.Options));
        }

        public static IElement Id(IDriver driver, string locator, DriverScope scope = null, Options options = null)
        {
            return FindSingle(new IdFinder(driver, locator, scope ?? WindowScope(driver), options ?? Default.Options));
        }

        public static IElement XPath(IDriver driver, string locator)
        {
            return FindSingle(new XPathFinder(driver, locator, WindowScope(driver), Default.Options));
        }

        public static IElement Css(IDriver driver, string locator)
        {
            return FindSingle(new CssFinder(driver, locator, WindowScope(driver), Default.Options));
        }

        public static IElement Css(IDriver driver, string locator, Regex text, DriverScope scope = null, Options options = null)
        {
            return FindSingle(new CssFinder(driver, locator, scope ?? WindowScope(driver), options ?? Default.Options, text));
        }

        public static IElement Section(IDriver driver, string locator)
        {
            return FindSingle(new SectionFinder(driver, locator, WindowScope(driver), Default.Options));
        }

        public static IElement Fieldset(IDriver driver, string locator)
        {
            return FindSingle(new FieldsetFinder(driver, locator, WindowScope(driver), Default.Options));
        }

        public static IElement FindSingle(ElementFinder finder)
        {
            return new ThrowsWhenMissingButNoDisambiguationStrategy().ResolveQuery(finder);
        }
    }
}