namespace Coypu.Queries
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class PredicateQuery : Query<bool>
    {
        /// <summary>
        /// 
        /// </summary>
        public Options Options { get; }
        /// <summary>
        /// 
        /// </summary>
        public DriverScope Scope { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        protected PredicateQuery(Options options)
        {
            Options = options;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public abstract bool Predicate();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool Run() => Predicate();

        /// <summary>
        /// 
        /// </summary>
        public object ExpectedResult => true;
    }
}