using Coypu.Queries;

namespace Coypu.Actions
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class BrowserAction : IQuery<object>
    {
        /// <summary>
        /// 
        /// </summary>
        public Options Options { get; }
        /// <summary>
        /// 
        /// </summary>
        public DriverScope Scope { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="scope"></param>
        /// <param name="options"></param>
        protected BrowserAction(DriverScope scope, Options options)
        {
            Options = options;
            Scope = scope;
        }

        /// <summary>
        /// 
        /// </summary>
        public abstract void Act();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public object Run()
        {
            Act();
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        public object ExpectedResult => null;
    }
}