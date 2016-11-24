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
            Assert.That("attributeTestForm", Is.EqualTo(formWithAttributesToTest["id"]));
            Assert.That("post", Is.EqualTo(formWithAttributesToTest["method"]));
            Assert.That("http://somesite.com/action.htm", Is.EqualTo(formWithAttributesToTest["action"]));
            Assert.That("_parent", Is.EqualTo(formWithAttributesToTest["target"]));
        }
    }
}