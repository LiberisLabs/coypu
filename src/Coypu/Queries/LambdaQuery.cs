using System;

namespace Coypu.Queries
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class LambdaQuery<T> : Query<T>
    {
        private readonly Func<T> _query;
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
        public object ExpectedResult { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        public LambdaQuery(Func<T> query)
        {
            _query = query;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <param name="options"></param>
        public LambdaQuery(Func<T> query, Options options) : this(query)
        {
            Options = options;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <param name="expectedResult"></param>
        /// <param name="options"></param>
        public LambdaQuery(Func<T> query, object expectedResult, Options options) : this(query, options)
        {
            ExpectedResult = expectedResult;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <param name="expectedResult"></param>
        /// <param name="scope"></param>
        /// <param name="options"></param>
        public LambdaQuery(Func<T> query, object expectedResult, DriverScope scope, Options options)
            : this(query, expectedResult, options)
        {
            Scope = scope;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public T Run()
        {
            return _query();
        }
    }
}