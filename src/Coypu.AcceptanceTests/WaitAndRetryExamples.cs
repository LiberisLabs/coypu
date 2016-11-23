﻿using System;
using Coypu.Drivers.Selenium;
using NUnit.Framework;

namespace Coypu.AcceptanceTests
{
    public class WaitAndRetryExamples
    {
        protected BrowserSession Browser;

        [OneTimeSetUp]
        public void SetUpFixture()
        {
            var configuration = new SessionConfiguration
            {
                Timeout = TimeSpan.FromMilliseconds(2000),
                Browser = Drivers.Browser.Firefox,
                Driver = typeof(SeleniumWebDriver)
            };

            Browser = new BrowserSession(configuration);
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            Browser.Dispose();
        }

        [SetUp]
        public void SetUp()
        {
            ReloadTestPageWithDelay();
        }

        protected void ApplyAsyncDelay()
        {
            // Hide the HTML then bring back after a short delay to test robustness
            Browser.ExecuteScript("window.holdIt = window.document.body.innerHTML;");
            Browser.ExecuteScript("window.document.body.innerHTML = '';");
            Browser.ExecuteScript("setTimeout(function() {document.body.innerHTML = window.holdIt},250)");
        }

        protected void ReloadTestPage()
        {
            Browser.Visit(Helper.GetProjectFile("html\\InteractionTestsPage.htm"));
        }

        protected void ReloadTestPageWithDelay()
        {
            ReloadTestPage();
            ApplyAsyncDelay();
        }
    }
}