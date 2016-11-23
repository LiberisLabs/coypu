using System;
using NUnit.Framework;
using Coypu.Queries;

namespace Coypu.AcceptanceTests
{
    [TestFixture]
    public class States
    {
        private SessionConfiguration _sessionConfiguration;
        private BrowserSession _browser;

        [OneTimeSetUp]
        public void SetUpFixture()
        {
            _sessionConfiguration = new SessionConfiguration {Timeout = TimeSpan.FromMilliseconds(1000)};
            _browser = new BrowserSession(_sessionConfiguration);
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            _browser.Dispose();
        }

        [SetUp]
        public void SetUp()
        {
            
            ReloadTestPage();
        }

        private void ShowStateAsync(string id, int delayMilliseconds)
        {
            _browser.ExecuteScript($"setTimeout(function() {{document.getElementById('{id}').style.display = 'block'}},{delayMilliseconds})");
        }

        private void ReloadTestPage()
        {
            _browser.Visit(Helper.GetProjectFile(@"html\states.htm"));
        }

        [Test]
        public void Page_reaches_first_of_three_possible_states()
        {
            ShowStateAsync("state1", 500);

            var state1 = new State(() => _browser.HasContent("State one reached"));
            var state2 = new State(() => _browser.HasContent("State two reached"));
            var state3 = new State(() => _browser.HasContent("State three reached"));

            var foundState = _browser.FindState(state1, state2, state3);

            Assert.That(foundState, Is.SameAs(state1));
        }

        [Test]
        public void Page_reaches_second_of_three_possible_states()
        {
            ShowStateAsync("state2", 500);

            var state1 = new State(new LambdaQuery<bool>(() => _browser.HasContent("State one reached")));
            var state2 = new State(new LambdaQuery<bool>(() => _browser.HasContent("State two reached")));
            var state3 = new State(new LambdaQuery<bool>(() => _browser.HasContent("State three reached")));

            var foundState = _browser.FindState(state1, state2, state3);

            Assert.That(foundState, Is.SameAs(state2));
        }


        [Test]
        public void Page_reaches_third_of_three_possible_states()
        {
            ShowStateAsync("state3", 500);

            var state1 = new State(new LambdaQuery<bool>(() => _browser.HasContent("State one reached")));
            var state2 = new State(new LambdaQuery<bool>(() => _browser.HasContent("State two reached")));
            var state3 = new State(new LambdaQuery<bool>(() => _browser.HasContent("State three reached")));

            var foundState = _browser.FindState(state1, state2, state3);

            Assert.That(foundState, Is.SameAs(state3));
        }

        [Test]
        public void Page_reaches_none_of_three_possible_states()
        {
            var state1 = new State(new LambdaQuery<bool>(() => _browser.HasContent("State one reached")));
            var state2 = new State(new LambdaQuery<bool>(() => _browser.HasContent("State two reached")));
            var state3 = new State(new LambdaQuery<bool>(() => _browser.HasContent("State three reached")));

            Assert.Throws<MissingHtmlException>(() => _browser.FindState(state1, state2, state3));
        }
    }
}