using NUnit.Framework;

namespace Coypu.Drivers.Tests.Tests
{
    [TestFixture]
    internal class WhenAccessingAttributes
    {
        [OneTimeSetUp]
        public void Given() => DriverSpecs.VisitTestPage();

        [Test]
        public void Exposes_element_attributes()
        {
            var formWithAttributesToTest = DriverHelpers.Id(DriverSpecs.Driver, "attributeTestForm");
            Assert.That(formWithAttributesToTest["id"], Is.EqualTo("attributeTestForm"));
            Assert.That(formWithAttributesToTest["method"], Is.EqualTo("post"));
            Assert.That(formWithAttributesToTest["action"], Is.EqualTo("http://somesite.com/action.htm"));
            Assert.That(formWithAttributesToTest["target"], Is.EqualTo("_parent"));
        }
    }
}