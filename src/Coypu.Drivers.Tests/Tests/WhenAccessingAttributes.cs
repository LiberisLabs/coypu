using NUnit.Framework;

namespace Coypu.Drivers.Tests.Tests
{
    [TestFixture]
    internal class WhenAccessingAttributes
    {
        [OneTimeSetUp]
        public void Given() => DriverSpecs.DoSetUp();

        [Test]
        public void Exposes_element_attributes()
        {
            var formWithAttributesToTest = DriverSpecs.Id("attributeTestForm", DriverSpecs.Root, DriverSpecs.DefaultOptions);
            Assert.That(formWithAttributesToTest["id"], Is.EqualTo("attributeTestForm"));
            Assert.That(formWithAttributesToTest["method"], Is.EqualTo("post"));
            Assert.That(formWithAttributesToTest["action"], Is.EqualTo("http://somesite.com/action.htm"));
            Assert.That(formWithAttributesToTest["target"], Is.EqualTo("_parent"));
        }
    }
}