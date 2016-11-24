using NUnit.Framework;

namespace Coypu.Drivers.Tests.Tests
{
    [TestFixture]
    internal class WhenAccessingAttributes
    {
        private Driver _driver;

        [OneTimeSetUp]
        public void Given() => _driver = TestDriver.Instance();

        [Test]
        public void Exposes_element_attributes()
        {
            var formWithAttributesToTest = DriverHelpers.Id(_driver, "attributeTestForm");
            Assert.That(formWithAttributesToTest["id"], Is.EqualTo("attributeTestForm"));
            Assert.That(formWithAttributesToTest["method"], Is.EqualTo("post"));
            Assert.That(formWithAttributesToTest["action"], Is.EqualTo("http://somesite.com/action.htm"));
            Assert.That(formWithAttributesToTest["target"], Is.EqualTo("_parent"));
        }
    }
}