using System;
using System.Reflection;

namespace Coypu
{
    /// <summary>
    /// 
    /// </summary>
    public class ActivatorDriverFactory : DriverFactory
    {
        /// <summary>
        /// 
        /// </summary>
        public static int OpenDrivers { get; set; }

        /// <inheritdoc />
        public Driver NewWebDriver(Type driverType, Drivers.Browser browser)
        {
            try
            {
                var driver = (Driver) Activator.CreateInstance(driverType, browser);
                OpenDrivers++;
                return driver;
            }
            catch (TargetInvocationException e)
            {
                throw e.InnerException;
            }
        }
    }
}