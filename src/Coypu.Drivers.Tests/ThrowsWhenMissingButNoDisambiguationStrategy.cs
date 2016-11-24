using System.Linq;
using Coypu.Finders;

namespace Coypu.Drivers.Tests
{
    internal class ThrowsWhenMissingButNoDisambiguationStrategy : IDisambiguationStrategy
    {
        public Element ResolveQuery(ElementFinder elementFinder)
        {
            var all = elementFinder.Find(elementFinder.Options).ToArray();
            if (!all.Any())
                throw elementFinder.GetMissingException();

            return all.First();
        }
    }
}