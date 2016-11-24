using System;

namespace Coypu.Queries
{
    /// <summary>
    /// 
    /// </summary>
    public class LambdaPredicateQuery : PredicateQuery
    {
        private readonly Func<bool> _query;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <param name="options"></param>
        public LambdaPredicateQuery(Func<bool> query, Options options = null) : base(options)
        {
            _query = query;
        }

        /// <inheritdoc />
        public override bool Predicate()
        {
            return _query();
        }
    }
}