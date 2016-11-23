using Coypu.Finders;
using Coypu.Tests.WhenInteractingWithTheBrowser;
using Coypu.Timing;
using Coypu.WebRequests;

namespace Coypu.Tests.TestBuilders
{
    internal class TestSessionBuilder
    {
        internal static BrowserSession Build(SessionConfiguration sessionConfiguration,
                                             Driver driver,
                                             TimingStrategy timingStrategy,
                                             Waiter waiter,
                                             RestrictedResourceDownloader restrictedResourceDownloader,
                                             UrlBuilder urlBuilder,
                                             IDisambiguationStrategy disambiguationStrategy = null)
        {
            disambiguationStrategy = disambiguationStrategy ?? new FirstOrDefaultNoDisambiguationStrategy();

            return new BrowserSession(sessionConfiguration,
                                      new StubDriverFactory(driver),
                                      timingStrategy,
                                      waiter,
                                      urlBuilder,
                                      disambiguationStrategy,
                                      restrictedResourceDownloader);
        }
    }
}