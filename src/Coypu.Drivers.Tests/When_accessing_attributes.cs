using NUnit.Framework;

namespace Coypu.Drivers.Tests
{
    internal class When_accessing_attributes : DriverSpecs
    {
        [Test]
        public void Exposes_element_attributes()
        {
            var formWithAttributesToTest = Id("attributeTestForm", Root, DefaultOptions);
            Assert.That(formWithAttributesToTest["id"], Is.EqualTo("attributeTestForm"));
            Assert.That(formWithAttributesToTest["method"], Is.EqualTo("post"));
            Assert.That(formWithAttributesToTest["action"], Is.EqualTo("http://somesite.com/action.htm"));
            Assert.That(formWithAttributesToTest["target"], Is.EqualTo("_parent"));
        }
    }
}