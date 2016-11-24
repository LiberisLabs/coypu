using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;

namespace Coypu.Tests.TestDoubles
{
    public class StubDriver : IDriver
    {
        public StubDriver()
        {
        }

        public StubDriver(Drivers.Browser browser)
        {
        }

        public void Dispose()
        {
        }

        public void Click(IElement element)
        {
        }

        public void Visit(string url, IScope scope)
        {
        }

        public void Set(IElement element, string value)
        {
        }

        public object Native
        {
            get { return "Native driver on stub driver"; }
        }

        public bool HasContent(string text, IScope scope)
        {
            return false;
        }

        public bool HasContentMatch(Regex pattern, IScope scope)
        {
            return false;
        }

        public bool HasDialog(string withText, IScope scope)
        {
            return false;
        }

        public IEnumerable<IElement> FindAllCss(string cssSelector, IScope scope, Options options, Regex textPattern = null)
        {
            return Enumerable.Empty<IElement>();
        }


        public IEnumerable<IElement> FindAllXPath(string xpath, IScope scope, Options options)
        {
            return Enumerable.Empty<IElement>();
        }

        public void Check(IElement field)
        {
        }

        public void Uncheck(IElement field)
        {
        }

        public void Choose(IElement field)
        {
        }

        public bool Disposed
        {
            get { return false; }
        }

        Uri IDriver.Location(IScope scope)
        {
            return null;
        }

        public string Title(IScope scope)
        {
            return null;
        }

        public IElement Window
        {
            get { return null; }
        }

        public void AcceptModalDialog(IScope scope)
        {
        }

        public void CancelModalDialog(IScope scope)
        {
        }

        public void SetScope(IElement findScope)
        {
        }

        public void ClearScope()
        {
        }

        public object ExecuteScript(string javascript, IScope scope, params object[] args)
        {
            return null;
        }

        public IElement FindIFrame(string locator, IScope scope)
        {
            return null;
        }

        public void Hover(IElement element)
        {
        }

        public IEnumerable<Cookie> GetBrowserCookies()
        {
            return new List<Cookie>();
        }

        public IEnumerable<IElement> FindWindows(string locator, IScope scope, Options options)
        {
            return Enumerable.Empty<IElement>();
        }

        public IEnumerable<IElement> FindFrames(string locator, IScope scope, Options options)
        {
            return Enumerable.Empty<IElement>();
        }

        public void SendKeys(IElement element, string keys)
        {
        }

        public void MaximiseWindow(IScope scope)
        {
        }

        public void Refresh(IScope scope)
        {
        }

        public void ResizeTo(Size size, IScope Scope)
        {
        }

        public void SaveScreenshot(string fileName, IScope scope)
        {
        }

        public void GoBack(IScope scope)
        {
        }

        public void GoForward(IScope scope)
        {
        }

        public void SetBrowserCookies(Cookie cookie)
        {
        }
    }
}