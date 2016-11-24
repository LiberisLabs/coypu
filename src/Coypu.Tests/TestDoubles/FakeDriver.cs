using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using Coypu.Drivers;

namespace Coypu.Tests.TestDoubles
{
    public class FakeDriver : IDriver
    {
        public readonly IList<IElement> ClickedElements = new List<IElement>();
        public readonly IList<IElement> HoveredElements = new List<IElement>();
        public readonly IList<IElement> CheckedElements = new List<IElement>();
        public readonly IList<IElement> UncheckedElements = new List<IElement>();
        public readonly IList<IElement> ChosenElements = new List<IElement>();
        public readonly IList<ScopedRequest<string>> Visits = new List<ScopedRequest<string>>();
        public readonly IDictionary<IElement, SetFieldParams> SetFields = new Dictionary<IElement, SetFieldParams>();
        public readonly IDictionary<IElement, string> SentKeys = new Dictionary<IElement, string>();
        private readonly IList<ScopedStubResult> stubbedAllCssResults = new List<ScopedStubResult>();
        private readonly IList<ScopedStubResult> stubbedAllXPathResults = new List<ScopedStubResult>();
        private readonly IList<ScopedStubResult> stubbedExecuteScriptResults = new List<ScopedStubResult>();
        private readonly IList<ScopedStubResult> stubbedFrames = new List<ScopedStubResult>();
        private readonly IList<ScopedStubResult> stubbedHasDialogResults = new List<ScopedStubResult>();
        private readonly IList<ScopedStubResult> stubbedWindows = new List<ScopedStubResult>();
        private readonly IList<ScopedStubResult> stubbedLocations = new List<ScopedStubResult>();
        private readonly IList<ScopedStubResult> stubbedTitles = new List<ScopedStubResult>();
        private IElement stubbedCurrentWindow;
        public readonly IList<FindXPathParams> FindXPathRequests = new List<FindXPathParams>();
        public readonly IList<IScope> MaximiseWindowCalls = new List<IScope>();
        public readonly IList<IScope> RefreshCalls = new List<IScope>();
        public readonly IList<ScopedRequest<Size>> ResizeToCalls = new List<ScopedRequest<Size>>();
        public readonly IList<IScope> GoBackCalls = new List<IScope>();
        public readonly IList<IScope> GoForwardCalls = new List<IScope>();
        public readonly IList<ScopedRequest<string>> SaveScreenshotCalls = new List<ScopedRequest<string>>();

        private IList<Cookie> stubbedCookies;

        public List<IScope> ModalDialogsAccepted = new List<IScope>();
        public List<IScope> ModalDialogsCancelled = new List<IScope>();

        public FakeDriver()
        {
        }

        public FakeDriver(Drivers.Browser browser)
        {
            Browser = browser;
        }

        public class SaveScreenshotParams
        {
            public string SaveAs;
            public ImageFormat ImageFormat;
        }

        class ScopedStubResult
        {
            public string Locator;
            public object Result;
            public IScope Scope;
            public Regex TextPattern;
            public Options Options;
        }

        public class ScopedRequest<T>
        {
            public T Request;
            public IScope Scope;
        }

        public Drivers.Browser Browser { get; private set; }

        private T Find<T>(IEnumerable<ScopedStubResult> stubbed, string locator, IScope scope, Options options = null, Regex textPattern = null)
        {
            var stubResult = stubbed
                .FirstOrDefault(
                    r =>
                        r.Locator == locator && (r.Scope == scope || scope.Now() == r.Scope.Now()) && r.TextPattern == textPattern && options == r.Options);

            if (stubResult == null)
                throw new MissingHtmlException("No stubbed result found for: " + locator);

            return (T) stubResult.Result;
        }

        private IEnumerable<T> FindAll<T>(IEnumerable<ScopedStubResult> stubbed, object locator, IScope scope, Options options = null, Regex textPattern = null)
        {
            var stubResult = stubbed.FirstOrDefault(r => r.Locator == locator && r.Scope == scope && r.TextPattern == textPattern && options == r.Options);
            if (stubResult == null)
                return Enumerable.Empty<T>();

            return (IEnumerable<T>) stubResult.Result;
        }

        public void Click(IElement element)
        {
            ClickedElements.Add(element);
        }

        public void Hover(IElement element)
        {
            HoveredElements.Add(element);
        }

        public IEnumerable<Cookie> GetBrowserCookies()
        {
            return stubbedCookies;
        }

        public void SetBrowserCookies(Cookie cookie)
        {
        }

        public void Visit(string url, IScope scope)
        {
            Visits.Add(new ScopedRequest<string> {Request = url, Scope = scope});
        }

        public void StubDialog(string text, bool result, IScope scope)
        {
            stubbedHasDialogResults.Add(new ScopedStubResult {Locator = text, Scope = scope, Result = result});
        }

        public void StubAllCss(string cssSelector, IEnumerable<IElement> result, IScope scope, Options options)
        {
            stubbedAllCssResults.Add(new ScopedStubResult {Locator = cssSelector, Scope = scope, Result = result, Options = options});
        }

        public void StubAllXPath(string xpath, IEnumerable<IElement> result, IScope scope, Options options)
        {
            stubbedAllXPathResults.Add(new ScopedStubResult {Locator = xpath, Scope = scope, Result = result, Options = options});
        }

        public void Dispose()
        {
            Disposed = true;
        }

        public bool Disposed { get; private set; }

        public Uri Location(IScope scope)
        {
            return Find<Uri>(stubbedLocations, null, scope);
        }

        public string Title(IScope scope)
        {
            return Find<String>(stubbedTitles, null, scope);
        }

        public IElement Window
        {
            get { return stubbedCurrentWindow; }
        }

        public void AcceptModalDialog(IScope scope)
        {
            ModalDialogsAccepted.Add(scope);
        }

        public void CancelModalDialog(IScope scope)
        {
            ModalDialogsCancelled.Add(scope);
        }

        public object ExecuteScript(string javascript, IScope scope, params object[] args)
        {
            return Find<string>(stubbedExecuteScriptResults, javascript, scope);
        }

        public IEnumerable<IElement> FindFrames(string locator, IScope scope, Options options)
        {
            return Find<IEnumerable<IElement>>(stubbedFrames, locator, scope, options);
        }

        public void SendKeys(IElement element, string keys)
        {
            SentKeys.Add(element, keys);
        }

        public void MaximiseWindow(IScope scope)
        {
            MaximiseWindowCalls.Add(scope);
        }

        public void Refresh(IScope scope)
        {
            RefreshCalls.Add(scope);
        }

        public void ResizeTo(Size size, IScope Scope)
        {
            ResizeToCalls.Add(new ScopedRequest<Size> {Request = size, Scope = Scope});
        }

        public void SaveScreenshot(string fileName, IScope scope)
        {
            SaveScreenshotCalls.Add(new ScopedRequest<string>
            {
                Request = fileName,
                Scope = scope
            });
        }

        public void GoBack(IScope scope)
        {
            GoBackCalls.Add(scope);
        }

        public void GoForward(IScope scope)
        {
            GoForwardCalls.Add(scope);
        }

        public void Set(IElement element, string value)
        {
            SetFields.Add(element, new SetFieldParams {Value = value});
        }

        public object Native
        {
            get { return "Native driver on fake driver"; }
        }

        public bool HasDialog(string withText, IScope scope)
        {
            return Find<bool>(stubbedHasDialogResults, withText, scope);
        }

        private static bool RegexEqual(Regex a, Regex b)
        {
            return (a.ToString() == b.ToString() && a.Options == b.Options);
        }

        public IEnumerable<IElement> FindAllCss(string cssSelector, IScope scope, Options options, Regex textPattern = null)
        {
            return Find<IEnumerable<IElement>>(stubbedAllCssResults, cssSelector, scope, options, textPattern);
        }

        public IEnumerable<IElement> FindAllXPath(string xpath, IScope scope, Options options)
        {
            FindXPathRequests.Add(new FindXPathParams {XPath = xpath, Scope = scope, Options = options});
            return Find<IEnumerable<IElement>>(stubbedAllXPathResults, xpath, scope, options);
        }

        public void Check(IElement field)
        {
            CheckedElements.Add(field);
        }

        public void Uncheck(IElement field)
        {
            UncheckedElements.Add(field);
        }

        public void Choose(IElement field)
        {
            ChosenElements.Add(field);
        }

        public void StubExecuteScript(string script, string scriptReturnValue, IScope scope)
        {
            stubbedExecuteScriptResults.Add(new ScopedStubResult {Locator = script, Scope = scope, Result = scriptReturnValue});
        }

        public void StubFrames(string locator, IElement frame, IScope scope, Options options)
        {
            stubbedFrames.Add(new ScopedStubResult {Locator = locator, Scope = scope, Result = frame, Options = options});
        }

        public void StubId(string id, IElement element, IScope scope, SessionConfiguration options)
        {
            StubAllXPath(new Html(options.Browser.UppercaseTagNames).Id(id, options), new[] {element}, scope, options);
        }

        public void StubCookies(List<Cookie> cookies)
        {
            stubbedCookies = cookies;
        }

        public void StubLocation(Uri location, IScope scope)
        {
            stubbedLocations.Add(new ScopedStubResult {Result = location, Scope = scope});
        }

        public void StubTitle(String title, IScope scope)
        {
            stubbedTitles.Add(new ScopedStubResult {Result = title, Scope = scope});
        }

        public void StubWindow(string locator, IElement window, IScope scope, Options options)
        {
            stubbedWindows.Add(new ScopedStubResult {Locator = locator, Scope = scope, Result = window, Options = options});
        }

        public void StubCurrentWindow(IElement window)
        {
            stubbedCurrentWindow = window;
        }

        public IEnumerable<IElement> FindWindows(string locator, IScope scope, Options options)
        {
            return Find<IEnumerable<IElement>>(stubbedWindows, locator, scope, options);
        }
    }

    public class SetFieldParams
    {
        public string Value { get; set; }
    }

    public class FindXPathParams
    {
        public string XPath { get; set; }
        public Regex TextPattern { get; set; }
        public Options Options { get; set; }
        public IScope Scope { get; set; }
    }
}