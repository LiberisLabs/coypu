using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using OpenQA.Selenium;
using Cookie = System.Net.Cookie;

namespace Coypu.Drivers.Selenium
{
    /// <summary>
    /// 
    /// </summary>
    public class SeleniumWebDriver : IDriver
    {
        /// <inheritdoc />
        public bool Disposed { get; private set; }
        private IWebDriver _webDriver;
        private readonly ElementFinder _elementFinder;
        private readonly FrameFinder _frameFinder;
        private readonly TextMatcher _textMatcher;
        private readonly Dialogs _dialogs;
        private readonly MouseControl _mouseControl;
        private readonly Browser _browser;
        private readonly WindowHandleFinder _windowHandleFinder;
        private readonly SeleniumWindowManager _seleniumWindowManager;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="browser"></param>
        public SeleniumWebDriver(Browser browser)
            : this(new DriverFactory().NewWebDriver(browser), browser)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="webDriver"></param>
        /// <param name="browser"></param>
        protected SeleniumWebDriver(IWebDriver webDriver, Browser browser)
        {
            _webDriver = webDriver;
            _browser = browser;
            var xPath = new XPath(browser.UppercaseTagNames);
            _elementFinder = new ElementFinder();
            _textMatcher = new TextMatcher();
            _dialogs = new Dialogs(_webDriver);
            _mouseControl = new MouseControl(_webDriver);
            _seleniumWindowManager = new SeleniumWindowManager(_webDriver);
            _frameFinder = new FrameFinder(_webDriver, _elementFinder, xPath, _seleniumWindowManager);
            _windowHandleFinder = new WindowHandleFinder(_webDriver, _seleniumWindowManager);
        }

        /// <inheritdoc />
        public Uri Location(IScope scope)
        {
            _elementFinder.SeleniumScope(scope);
            return new Uri(_webDriver.Url);
        }

        /// <inheritdoc />
        public string Title(IScope scope)
        {
            _elementFinder.SeleniumScope(scope);
            return _webDriver.Title;
        }

        /// <inheritdoc />
        public IElement Window => new SeleniumWindow(_webDriver, _webDriver.CurrentWindowHandle, _seleniumWindowManager);

        /// <summary>
        /// 
        /// </summary>
        protected bool NoJavascript => !_browser.Javascript;

        private IJavaScriptExecutor JavaScriptExecutor => _webDriver as IJavaScriptExecutor;

        /// <inheritdoc />
        public object Native => _webDriver;

        /// <inheritdoc />
        public IEnumerable<IElement> FindFrames(string locator, IScope scope, Options options)
        {
            return _frameFinder.FindFrame(locator, scope, options).Select(BuildElement);
        }

        private Func<IWebElement, bool> ValidateTextPattern(Options options, Regex textPattern)
        {
            var textMatches = (textPattern == null)
                ? (Func<IWebElement, bool>) null
                : e => _textMatcher.TextMatches(e, textPattern);

            if (textPattern != null && options.ConsiderInvisibleElements)
                throw new NotSupportedException("Cannot inspect the text of invisible elements.");
            return textMatches;
        }

        /// <inheritdoc />
        public IEnumerable<IElement> FindAllCss(string cssSelector, IScope scope, Options options, Regex textPattern = null)
        {
            return FindAll(By.CssSelector(cssSelector), scope, options, ValidateTextPattern(options, textPattern)).Select(BuildElement);
        }

        /// <inheritdoc />
        public IEnumerable<IElement> FindAllXPath(string xpath, IScope scope, Options options)
        {
            return FindAll(By.XPath(xpath), scope, options).Select(BuildElement);
        }

        private IEnumerable<IWebElement> FindAll(By by, IScope scope, Options options, Func<IWebElement, bool> predicate = null)
        {
            return _elementFinder.FindAll(by, scope, options, predicate);
        }

        private IElement BuildElement(IWebElement element)
        {
            return new[] {"iframe", "frame"}.Contains(element.TagName.ToLower())
                ? new SeleniumFrame(element, _webDriver, _seleniumWindowManager)
                : new SeleniumElement(element, _webDriver);
        }

        /// <inheritdoc />
        public bool HasDialog(string withText, IScope scope)
        {
            _elementFinder.SeleniumScope(scope);
            return _dialogs.HasDialog(withText);
        }

        /// <inheritdoc />
        public void Visit(string url, IScope scope)
        {
            _elementFinder.SeleniumScope(scope);
            _webDriver.Navigate().GoToUrl(url);
        }

        /// <inheritdoc />
        public void Click(IElement element)
        {
            SeleniumElement(element).Click();
        }

        /// <inheritdoc />
        public void Hover(IElement element)
        {
            _mouseControl.Hover(element);
        }

        /// <inheritdoc />
        public void SendKeys(IElement element, string keys)
        {
            SeleniumElement(element).SendKeys(keys);
        }

        /// <inheritdoc />
        public void MaximiseWindow(IScope scope)
        {
            _elementFinder.SeleniumScope(scope);
            _webDriver.Manage().Window.Maximize();
        }

        /// <inheritdoc />
        public void Refresh(IScope scope)
        {
            _elementFinder.SeleniumScope(scope);
            _webDriver.Navigate().Refresh();
        }

        /// <inheritdoc />
        public void ResizeTo(Size size, IScope scope)
        {
            _elementFinder.SeleniumScope(scope);
            _webDriver.Manage().Window.Size = size;
        }

        /// <inheritdoc />
        public void SaveScreenshot(string fileName, IScope scope)
        {
            _elementFinder.SeleniumScope(scope);
            var format = ImageFormatParser.GetImageFormat(fileName);

            var screenshot = ((ITakesScreenshot) _webDriver).GetScreenshot();
            screenshot.SaveAsFile(fileName, format);
        }

        /// <inheritdoc />
        public void GoBack(IScope scope)
        {
            _elementFinder.SeleniumScope(scope);
            _webDriver.Navigate().Back();
        }

        /// <inheritdoc />
        public void GoForward(IScope scope)
        {
            _elementFinder.SeleniumScope(scope);
            _webDriver.Navigate().Forward();
        }

        /// <inheritdoc />
        public IEnumerable<Cookie> GetBrowserCookies()
        {
            return _webDriver.Manage().Cookies.AllCookies.Select(c => new Cookie(c.Name, c.Value, c.Path, c.Domain));
        }

        /// <inheritdoc />
        public IEnumerable<IElement> FindWindows(string titleOrName, IScope scope, Options options)
        {
            _elementFinder.SeleniumScope(scope);
            return _windowHandleFinder.FindWindowHandles(titleOrName, options).Select(h => new SeleniumWindow(_webDriver, h, _seleniumWindowManager));
        }

        /// <inheritdoc />
        public void Set(IElement element, string value)
        {
            try
            {
                SeleniumElement(element).Clear();
            }
            catch (InvalidElementStateException)
            {
            } // Non user-editable elements (file inputs) - chrome/IE
            catch (InvalidOperationException)
            {
            } // Non user-editable elements (file inputs) - firefox
            SendKeys(element, value);
        }

        /// <inheritdoc />
        public void AcceptModalDialog(IScope scope)
        {
            _elementFinder.SeleniumScope(scope);
            _dialogs.AcceptModalDialog();
        }

        /// <inheritdoc />
        public void CancelModalDialog(IScope scope)
        {
            _elementFinder.SeleniumScope(scope);
            _dialogs.CancelModalDialog();
        }

        /// <inheritdoc />
        public void Check(IElement field)
        {
            var seleniumElement = SeleniumElement(field);

            if (!seleniumElement.Selected)
                seleniumElement.Click();
        }

        /// <inheritdoc />
        public void Uncheck(IElement field)
        {
            var seleniumElement = SeleniumElement(field);

            if (seleniumElement.Selected)
                seleniumElement.Click();
        }

        /// <inheritdoc />
        public void Choose(IElement field)
        {
            SeleniumElement(field).Click();
        }

        /// <inheritdoc />
        public object ExecuteScript(string javascript, IScope scope, params object[] args)
        {
            if (NoJavascript)
                throw new NotSupportedException("Javascript is not supported by " + _browser);

            _elementFinder.SeleniumScope(scope);
            return JavaScriptExecutor.ExecuteScript(javascript, ConvertScriptArgs(args));
        }

        private static object[] ConvertScriptArgs(object[] args)
        {
            for (var i = 0; i < args.Length; ++i)
            {
                var argAsElement = args[i] as IElement;
                if (argAsElement != null)
                    args[i] = argAsElement.Native;
            }

            return args;
        }

        private static IWebElement SeleniumElement(IElement element)
        {
            return (IWebElement) element.Native;
        }

        /// <inheritdoc />
        public void Dispose()
        {
            if (_webDriver == null)
                return;

            AcceptAnyAlert();

            _webDriver.Quit();
            _webDriver = null;
            Disposed = true;
        }

        private void AcceptAnyAlert()
        {
            try
            {
                _seleniumWindowManager.SwitchToWindow(_webDriver.WindowHandles[0]);
                if (_dialogs.HasAnyDialog())
                    _webDriver.SwitchTo().Alert().Accept();
            }
            catch (WebDriverException)
            {
            }
            catch (KeyNotFoundException)
            {
            } // Chrome
            catch (InvalidOperationException)
            {
            }
            catch (IndexOutOfRangeException)
            {
            } // No window handles
        }
    }
}