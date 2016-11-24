using System;

namespace Coypu.Actions
{
    /// <summary>
    /// 
    /// </summary>
    public class LambdaBrowserAction : BrowserAction
    {
        private readonly Action _action;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="action"></param>
        /// <param name="options"></param>
        public LambdaBrowserAction(Action action, Options options)
            : base(null, options)
        {
            _action = action;
        }

        /// <inheritdoc />
        public override void Act()
        {
            _action();
        }
    }
}