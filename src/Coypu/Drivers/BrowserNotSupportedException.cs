using System;

namespace Coypu.Drivers
{
    /// <summary>
    /// Thrown when your chosen browser is not supported by your chosen driver
    /// </summary>
    public class BrowserNotSupportedException : Exception
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="browser"></param>
        /// <param name="driverType"></param>
        public BrowserNotSupportedException(Browser browser, Type driverType)
            : this(browser, driverType, null)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="browser"></param>
        /// <param name="driverType"></param>
        /// <param name="inner"></param>
        public BrowserNotSupportedException(Browser browser, Type driverType, Exception inner)
            : base($"{browser} is not supported by {driverType.Name}", inner)
        {
        }
    }
}