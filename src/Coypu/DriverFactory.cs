using System;

namespace Coypu
{
    /// <summary>
    /// 
    /// </summary>
    public interface DriverFactory
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="driverType"></param>
        /// <param name="browser"></param>
        /// <returns></returns>
        Driver NewWebDriver(Type driverType, Drivers.Browser browser);
    }
}