using NUnit.Framework;

namespace Coypu.Tests.WhenInteractingWithTheBrowser
{
    [TestFixture]
    public class WhenInspectingModalDialogs : WhenInspecting
    {
        [Test]
        public void HasDialog_should_wait_for_robustly_Positive_example()
        {
            Queries_robustly(true, browserSession.HasDialog, driver.StubDialog);
        }

        [Test]
        public void HasDialog_should_wait_for_robustly_Negative_example()
        {
            Queries_robustly(false, browserSession.HasDialog, driver.StubDialog);
        }

        [Test]
        public void HasNoDialog_should_wait_for_robustly_Positive_example()
        {
            Queries_robustly_reversing_result(true, browserSession.HasNoDialog, driver.StubDialog);
        }

        [Test]
        public void HasNoDialog_should_wait_for_robustly_Negative_example()
        {
            Queries_robustly_reversing_result(false, browserSession.HasNoDialog, driver.StubDialog);
        }
    }
}