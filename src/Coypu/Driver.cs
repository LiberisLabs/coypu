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
    public interface Driver : IDisposable
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
        IEnumerable<Element> FindAllCss(string cssSelector, Scope scope, Options options, Regex textPattern = null);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="xpath"></param>
        /// <param name="scope"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        IEnumerable<Element> FindAllXPath(string xpath, Scope scope, Options options);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="locator"></param>
        /// <param name="scope"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        IEnumerable<Element> FindWindows(string locator, Scope scope, Options options);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="locator"></param>
        /// <param name="scope"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        IEnumerable<Element> FindFrames(string locator, Scope scope, Options options);
        /// <summary>
        /// 
        /// </summary>
        Element Window { get; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="scope"></param>
        /// <returns></returns>
        Uri Location(Scope scope);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="scope"></param>
        /// <returns></returns>
        string Title(Scope scope);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="javascript"></param>
        /// <param name="scope"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        object ExecuteScript(string javascript, Scope scope, params object[] args);
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IEnumerable<Cookie> GetBrowserCookies();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="element"></param>
        void Click(Element element);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="scope"></param>
        void AcceptModalDialog(Scope scope);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="scope"></param>
        void CancelModalDialog(Scope scope);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="withText"></param>
        /// <param name="scope"></param>
        /// <returns></returns>
        bool HasDialog(string withText, Scope scope);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="field"></param>
        void Choose(Element field);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="field"></param>
        void Check(Element field);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="field"></param>
        void Uncheck(Element field);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        void Set(Element element, string value);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="scope"></param>
        void Visit(string url, Scope scope);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="scope"></param>
        void GoBack(Scope scope);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="scope"></param>
        void GoForward(Scope scope);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="element"></param>
        void Hover(Element element);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="scope"></param>
        void MaximiseWindow(Scope scope);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="scope"></param>
        void Refresh(Scope scope);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="size"></param>
        /// <param name="scope"></param>
        void ResizeTo(Size size, Scope scope);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="scope"></param>
        void SaveScreenshot(string fileName, Scope scope);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="element"></param>
        /// <param name="keys"></param>
        void SendKeys(Element element, string keys);
    }
}