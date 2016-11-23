﻿namespace Coypu.Queries
{
    internal class HasValueQuery : ElementScopeQuery<bool>
    {
        private readonly string text;

        public override object ExpectedResult
        {
            get { return true; }
        }

        internal HasValueQuery(DriverScope scope, string text, Options options)
            : base(scope, options)
        {
            this.text = text;
        }

        public override bool Run()
        {
            return DriverScope.FindElement().Value == text;
        }
    }
}