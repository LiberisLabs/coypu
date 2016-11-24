using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net;
using System.Text.RegularExpressions;

namespace Coypu
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDriver : IDisposable
    {
        /// <summary>
        /// 
        /// </summary>
        object Native { get; }
        /// <summary>
        /// 
        /// </summary>
        bool Disposed { get; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cssSelector"></param>
        /// <param name="scope"></param>
        /// <param name="options"></param>
        /// <param name="textPattern"></param>
        /// <returns></returns>
        IEnumerable<IElement> FindAllCss(string cssSelector, IScope scope, Options options, Regex textPattern = null);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="xpath"></param>
        /// <param name="scope"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        IEnumerable<IElement> FindAllXPath(string xpath, IScope scope, Options options);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="locator"></param>
        /// <param name="scope"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        IEnumerable<IElement> FindWindows(string locator, IScope scope, Options options);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="locator"></param>
        /// <param name="scope"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        IEnumerable<IElement> FindFrames(string locator, IScope scope, Options options);
        /// <summary>
        /// 
        /// </summary>
        IElement Window { get; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="scope"></param>
        /// <returns></returns>
        Uri Location(IScope scope);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="scope"></param>
        /// <returns></returns>
        string Title(IScope scope);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="javascript"></param>
        /// <param name="scope"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        object ExecuteScript(string javascript, IScope scope, params object[] args);
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IEnumerable<Cookie> GetBrowserCookies();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="element"></param>
        void Click(IElement element);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="scope"></param>
        void AcceptModalDialog(IScope scope);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="scope"></param>
        void CancelModalDialog(IScope scope);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="withText"></param>
        /// <param name="scope"></param>
        /// <returns></returns>
        bool HasDialog(string withText, IScope scope);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="field"></param>
        void Choose(IElement field);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="field"></param>
        void Check(IElement field);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="field"></param>
        void Uncheck(IElement field);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        void Set(IElement element, string value);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="scope"></param>
        void Visit(string url, IScope scope);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="scope"></param>
        void GoBack(IScope scope);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="scope"></param>
        void GoForward(IScope scope);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="element"></param>
        void Hover(IElement element);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="scope"></param>
        void MaximiseWindow(IScope scope);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="scope"></param>
        void Refresh(IScope scope);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="size"></param>
        /// <param name="scope"></param>
        void ResizeTo(Size size, IScope scope);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="scope"></param>
        void SaveScreenshot(string fileName, IScope scope);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="element"></param>
        /// <param name="keys"></param>
        void SendKeys(IElement element, string keys);
    }
}