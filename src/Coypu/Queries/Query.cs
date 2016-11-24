namespace Coypu.Queries
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TReturn"></typeparam>
    public interface IQuery<out TReturn>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        TReturn Run();
        /// <summary>
        /// 
        /// </summary>
        object ExpectedResult { get; }
        /// <summary>
        /// 
        /// </summary>
        Options Options { get; }
        /// <summary>
        /// 
        /// </summary>
        DriverScope Scope { get; }
    }
}