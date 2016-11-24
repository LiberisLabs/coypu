using System;

namespace Coypu
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDriverFactory
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="driverType"></param>
        /// <param name="browser"></param>
        /// <returns></returns>
        IDriver NewWebDriver(Type driverType, Drivers.Browser browser);
    }
}