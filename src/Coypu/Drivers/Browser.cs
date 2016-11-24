using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Coypu.Drivers
{
    /// <summary>
    /// The browser that will be used by your chosen driver
    /// </summary>
    public class Browser
    {
        private Browser()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        public bool Javascript { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        public bool UppercaseTagNames { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public static Browser Firefox = new Browser {Javascript = true, UppercaseTagNames = true};
        /// <summary>
        /// 
        /// </summary>
        public static Browser InternetExplorer = new Browser {Javascript = true};
        /// <summary>
        /// 
        /// </summary>
        public static Browser Chrome = new Browser {Javascript = true};
        /// <summary>
        /// 
        /// </summary>
        public static Browser Safari = new Browser {Javascript = true};
        /// <summary>
        /// 
        /// </summary>
        public static Browser HtmlUnit = new Browser {Javascript = false};
        /// <summary>
        /// 
        /// </summary>
        public static Browser HtmlUnitWithJavaScript = new Browser {Javascript = true};
        /// <summary>
        /// 
        /// </summary>
        public static Browser PhantomJS = new Browser {Javascript = true};

        /// <summary>
        /// 
        /// </summary>
        /// <param name="browserName"></param>
        /// <returns></returns>
        /// <exception cref="NoSuchBrowserException"></exception>
        public static Browser Parse(string browserName)
        {
            var fieldInfo = BrowserFields().FirstOrDefault(f => f.Name.Equals(browserName.Replace(" ", ""), StringComparison.InvariantCultureIgnoreCase));
            if (fieldInfo == null)
                throw new NoSuchBrowserException(browserName);

            return (Browser) fieldInfo.GetValue(null);
        }

        private static IEnumerable<FieldInfo> BrowserFields()
        {
            return typeof(Browser).GetFields(BindingFlags.Public | BindingFlags.Static);
        }
    }
}