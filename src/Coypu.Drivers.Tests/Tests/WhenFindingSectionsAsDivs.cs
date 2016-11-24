using NUnit.Framework;

namespace Coypu.Drivers.Tests.Tests
{
    [TestFixture]
    internal class WhenFindingSectionsAsDivs
    {
        [OneTimeSetUp]
        public void Given() => DriverSpecs.DoSetUp();

        [Test]
        public void Finds_by_h1_text()
        {
            Assert.That("divSectionOne", Is.EqualTo(DriverSpecs.Section("Div Section One h1").Id));
            Assert.That("divSectionTwo", Is.EqualTo(DriverSpecs.Section("Div Section Two h1").Id));
        }

        [Test]
        public void Finds_by_h2_text()
        {
            Assert.That("divSectionOne", Is.EqualTo(DriverSpecs.Section("Div Section One h2").Id));
            Assert.That("divSectionTwo", Is.EqualTo(DriverSpecs.Section("Div Section Two h2").Id));
        }

        [Test]
        public void Finds_by_h3_text()
        {
            Assert.That("divSectionOne", Is.EqualTo(DriverSpecs.Section("Div Section One h3").Id));
            Assert.That("divSectionTwo", Is.EqualTo(DriverSpecs.Section("Div Section Two h3").Id));
        }

        [Test]
        public void Finds_by_h6_text()
        {
            Assert.That("divSectionOne", Is.EqualTo(DriverSpecs.Section("Div Section One h6").Id));
            Assert.That("divSectionTwo", Is.EqualTo(DriverSpecs.Section("Div Section Two h6").Id));
        }


        [Test]
        public void Finds_by_h2_text_within_child_link()
        {
            Assert.That("divSectionOneWithLink", Is.EqualTo(DriverSpecs.Section("Div Section One h2 with link").Id));
            Assert.That("divSectionTwoWithLink", Is.EqualTo(DriverSpecs.Section("Div Section Two h2 with link").Id));
        }


        [Test]
        public void Finds_by_div_by_id()
        {
            Assert.That(DriverSpecs.Section("Div Section One h1").Native, Is.EqualTo(DriverSpecs.Section("divSectionOne").Native));
            Assert.That(DriverSpecs.Section("Div Section Two h1").Native, Is.EqualTo(DriverSpecs.Section("divSectionTwo").Native));
        }
    }
}