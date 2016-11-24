namespace Coypu.Actions
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class DriverAction : BrowserAction
    {
        /// <summary>
        /// 
        /// </summary>
        protected IDriver Driver { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="scope"></param>
        /// <param name="options"></param>
        protected DriverAction(IDriver driver, DriverScope scope, Options options)
            : base(scope, options)
        {
            Driver = driver;
        }
    }
}