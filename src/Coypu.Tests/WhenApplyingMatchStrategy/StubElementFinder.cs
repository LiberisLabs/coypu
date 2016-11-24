using System.Collections.Generic;
using Coypu.Finders;

namespace Coypu.Tests.WhenApplyingMatchStrategy
{
    public class StubElementFinder : ElementFinder
    {
        public IDictionary<Options, IEnumerable<IElement>> StubbedFindResults = new Dictionary<Options, IEnumerable<IElement>>();

        public StubElementFinder(Options options, bool supportsSubstringTextMatching = true, string queryDescription = null) : base(null, null, null, options)
        {
            SupportsSubstringTextMatching = supportsSubstringTextMatching;
            QueryDescription = queryDescription;
        }

        public override bool SupportsSubstringTextMatching { get; }

        internal override IEnumerable<IElement> Find(Options o)
        {
            return StubbedFindResults[o];
        }

        internal override string QueryDescription { get; }
    }
}