using NUnit.Framework;

namespace Coypu.Drivers.Tests.Tests
{
    [TestFixture]
    internal class WhenFindingSectionsAsDivs
    {
        [OneTimeSetUp]
        public void Given() => DriverSpecs.VisitTestPage();

        [Test]
        public void Finds_by_h1_text()
        {
            Assert.That(DriverHelpers.Section(DriverSpecs.Driver, "Div Section One h1").Id, Is.EqualTo("divSectionOne"));
            Assert.That(DriverHelpers.Section(DriverSpecs.Driver, "Div Section Two h1").Id, Is.EqualTo("divSectionTwo"));
        }

        [Test]
        public void Finds_by_h2_text()
        {
            Assert.That(DriverHelpers.Section(DriverSpecs.Driver, "Div Section One h2").Id, Is.EqualTo("divSectionOne"));
            Assert.That(DriverHelpers.Section(DriverSpecs.Driver, "Div Section Two h2").Id, Is.EqualTo("divSectionTwo"));
        }

        [Test]
        public void Finds_by_h3_text()
        {
            Assert.That(DriverHelpers.Section(DriverSpecs.Driver, "Div Section One h3").Id, Is.EqualTo("divSectionOne"));
            Assert.That(DriverHelpers.Section(DriverSpecs.Driver, "Div Section Two h3").Id, Is.EqualTo("divSectionTwo"));
        }

        [Test]
        public void Finds_by_h6_text()
        {
            Assert.That(DriverHelpers.Section(DriverSpecs.Driver, "Div Section One h6").Id, Is.EqualTo("divSectionOne"));
            Assert.That(DriverHelpers.Section(DriverSpecs.Driver, "Div Section Two h6").Id, Is.EqualTo("divSectionTwo"));
        }


        [Test]
        public void Finds_by_h2_text_within_child_link()
        {
            Assert.That(DriverHelpers.Section(DriverSpecs.Driver, "Div Section One h2 with link").Id, Is.EqualTo("divSectionOneWithLink"));
            Assert.That(DriverHelpers.Section(DriverSpecs.Driver, "Div Section Two h2 with link").Id, Is.EqualTo("divSectionTwoWithLink"));
        }


        [Test]
        public void Finds_by_div_by_id()
        {
            Assert.That(DriverHelpers.Section(DriverSpecs.Driver, "divSectionOne").Native, Is.EqualTo(DriverHelpers.Section(DriverSpecs.Driver, "Div Section One h1").Native));
            Assert.That(DriverHelpers.Section(DriverSpecs.Driver, "divSectionTwo").Native, Is.EqualTo(DriverHelpers.Section(DriverSpecs.Driver, "Div Section Two h1").Native));
        }
    }
}