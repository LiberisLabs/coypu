using System.Linq;
using Coypu.Finders;

namespace Coypu.Tests.TestBuilders
{
    public class FirstOrDefaultNoDisambiguationStrategy : IDisambiguationStrategy
    {
        public IElement ResolveQuery(ElementFinder elementFinder)
        {
            return elementFinder.Find(elementFinder.Options).FirstOrDefault();
        }
    }

    public class ThrowsWhenMissingButNoDisambiguationStrategy : IDisambiguationStrategy
    {
        public IElement ResolveQuery(ElementFinder elementFinder)
        {
            var all = elementFinder.Find(elementFinder.Options).ToArray();
            if (!all.Any())
                throw elementFinder.GetMissingException();

            return all.First();
        }
    }
}