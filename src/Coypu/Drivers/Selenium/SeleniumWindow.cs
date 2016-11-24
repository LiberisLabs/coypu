using System;
using OpenQA.Selenium;

namespace Coypu.Drivers.Selenium
{
    internal class SeleniumWindow : IElement
    {
        private readonly IWebDriver _webDriver;
        private readonly string _windowHandle;
        private readonly SeleniumWindowManager _seleniumWindowManager;

        public SeleniumWindow(IWebDriver webDriver, string windowHandle, SeleniumWindowManager seleniumWindowManager)
        {
            _webDriver = webDriver;
            _windowHandle = windowHandle;
            _seleniumWindowManager = seleniumWindowManager;
        }

        public string Id
        {
            get { throw new NotSupportedException(); }
        }

        public string Text => ((ISearchContext) Native).FindElement(By.CssSelector("body")).Text;

        public string InnerHtml => ((ISearchContext) Native).FindElement(By.XPath("./*")).GetAttribute("innerHTML");

        public string Title => _webDriver.Title;

        public bool Disabled
        {
            get { throw new NotSupportedException(); }
        }

        public string OuterHtml => ((ISearchContext) Native).FindElement(By.XPath("./*")).GetAttribute("outerHTML");

        public string Value
        {
            get { throw new NotSupportedException(); }
        }

        public string Name
        {
            get { throw new NotSupportedException(); }
        }

        public string SelectedOption
        {
            get { throw new NotSupportedException(); }
        }

        public bool Selected
        {
            get { throw new NotSupportedException(); }
        }

        public object Native
        {
            get
            {
                SwitchTo(_windowHandle);
                return _webDriver;
            }
        }

        private void SwitchTo(string windowName)
        {
            _seleniumWindowManager.SwitchToWindow(windowName);
        }

        public string this[string attributeName]
        {
            get { throw new NotSupportedException(); }
        }
    }
}