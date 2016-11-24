using System;
using Coypu.Queries;

namespace Coypu
{
    ///<summary>
    /// A possible state for the current page
    ///</summary>
    public class State
    {
        private readonly IQuery<bool> _condition;

        ///<summary>
        /// Describe a possible state for the page with a condition to identify this state.
        ///</summary>
        ///<param name="condition">How to identify this state</param>
        public State(IQuery<bool> condition)
        {
            _condition = condition;
        }

        ///<summary>
        /// Describe a possible state for the page with a condition to identify this state.
        ///</summary>
        ///<param name="condition">How to identify this state</param>
        public State(Func<bool> condition)
        {
            _condition = new LambdaQuery<bool>(condition, true, new Options {Timeout = TimeSpan.Zero});
        }

        internal bool ConditionWasMet { get; private set; }

        internal bool CheckCondition()
        {
            return ConditionWasMet = _condition.Run();
        }
    }
}