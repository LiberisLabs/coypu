using System;
using System.Reflection;

namespace Coypu
{
    /// <summary>
    /// 
    /// </summary>
    public class ActivatorDriverFactory : IDriverFactory
    {
        /// <summary>
        /// 
        /// </summary>
        public static int OpenDrivers { get; set; }

        /// <inheritdoc />
        public IDriver NewWebDriver(Type driverType, Drivers.Browser browser)
        {
            try
            {
                var driver = (IDriver) Activator.CreateInstance(driverType, browser);
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