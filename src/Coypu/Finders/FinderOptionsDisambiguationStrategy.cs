using System.Linq;

namespace Coypu.Finders
{
    internal class FinderOptionsDisambiguationStrategy : IDisambiguationStrategy
    {
        public Element ResolveQuery(ElementFinder elementFinder)
        {
            Element[] results;

            if (elementFinder.Options.TextPrecision == TextPrecision.PreferExact)
                results = PreferExect(elementFinder);
            else
                results = Find(elementFinder);

            if (elementFinder.Options.Match == Match.Single && results.Length > 1)
                throw new AmbiguousException(elementFinder.Options.BuildAmbiguousMessage(elementFinder.QueryDescription, results.Length));

            if (!results.Any())
                throw elementFinder.GetMissingException();

            return results.First();
        }

        private static Element[] PreferExect(ElementFinder elementFinder)
        {
            var results = Find(elementFinder, Options.Exact);
            if (results.Any() || !elementFinder.SupportsSubstringTextMatching)
                return results;

            return Find(elementFinder, Options.Substring);
        }

        private static Element[] Find(ElementFinder elementFinder, Options preferredOptions = null)
        {
            return elementFinder.Find((Options.Merge(preferredOptions, elementFinder.Options))).ToArray();
        }
    }
}