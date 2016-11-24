using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;

namespace Coypu.Drivers.Selenium
{
    internal class FrameFinder
    {
        private readonly IWebDriver _selenium;
        private readonly ElementFinder _elementFinder;
        private readonly XPath _xPath;
        private readonly SeleniumWindowManager _seleniumWindowManager;

        public FrameFinder(IWebDriver selenium, ElementFinder elementFinder, XPath xPath, SeleniumWindowManager seleniumWindowManager)
        {
            _selenium = selenium;
            _elementFinder = elementFinder;
            _xPath = xPath;
            _seleniumWindowManager = seleniumWindowManager;
        }

        public IEnumerable<IWebElement> FindFrame(string locator, IScope scope, Options options)
        {
            var frames = AllElementsByTag(scope, "iframe", options)
                .Union(AllElementsByTag(scope, "frame", options));

            return WebElement(locator, frames, options);
        }

        private IEnumerable<IWebElement> AllElementsByTag(IScope scope, string tagNameToFind, Options options)
        {
            return _elementFinder.FindAll(By.TagName(tagNameToFind), scope, options);
        }

        private IEnumerable<IWebElement> WebElement(string locator, IEnumerable<IWebElement> webElements, Options options)
        {
            return webElements.Where(e => e.GetAttribute("id") == locator ||
                                          e.GetAttribute("name") == locator ||
                                          (options.TextPrecisionExact ? e.GetAttribute("title") == locator : e.GetAttribute("title").Contains(locator)) ||
                                          FrameContentsMatch(e, locator, options));
        }

        private bool FrameContentsMatch(IWebElement e, string locator, Options options)
        {
            var currentHandle = _selenium.CurrentWindowHandle;
            try
            {
                var frame = _seleniumWindowManager.SwitchToFrame(e);
                return frame.Title == locator || frame.FindElements(By.XPath(".//h1[" + _xPath.IsText(locator, options) + "]")).Any();
            }
            finally
            {
                _selenium.SwitchTo().Window(currentHandle);
            }
        }
    }
}