using Coypu.Drivers.Tests.Sites;
using NUnit.Framework;

namespace Coypu.Drivers.Tests
{
    [SetUpFixture]
    public class Initialize
    {
        public static SelfishSite TestSite;

        [OneTimeSetUp]
        public void StartTestSite()
        {
            TestSite = new SelfishSite();
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            TestSite.Dispose();

            TestDriver.DisposeDriver();
        }
    }
}
