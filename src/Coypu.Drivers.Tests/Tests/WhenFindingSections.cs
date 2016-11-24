using NUnit.Framework;

namespace Coypu.Drivers.Tests.Tests
{
    [TestFixture]
    internal class WhenFindingSections
    {
        private Driver _driver;

        [OneTimeSetUp]
        public void Given() => _driver = TestDriver.Instance();

        [Test]
        public void Finds_by_h1_text()
        {
            Assert.That(DriverHelpers.Section(_driver, "Section One h1").Id, Is.EqualTo("sectionOne"));
            Assert.That(DriverHelpers.Section(_driver, "Section Two h1").Id, Is.EqualTo("sectionTwo"));
        }

        [Test]
        public void Finds_by_h2_text()
        {
            Assert.That(DriverHelpers.Section(_driver, "Section One h2").Id, Is.EqualTo("sectionOne"));
            Assert.That(DriverHelpers.Section(_driver, "Section Two h2").Id, Is.EqualTo("sectionTwo"));
        }

        [Test]
        public void Finds_by_h3_text()
        {
            Assert.That(DriverHelpers.Section(_driver, "Section One h3").Id, Is.EqualTo("sectionOne"));
            Assert.That(DriverHelpers.Section(_driver, "Section Two h3").Id, Is.EqualTo("sectionTwo"));
        }

        [Test]
        public void Finds_by_h6_text()
        {
            Assert.That(DriverHelpers.Section(_driver, "Section One h6").Id, Is.EqualTo("sectionOne"));
            Assert.That(DriverHelpers.Section(_driver, "Section Two h6").Id, Is.EqualTo("sectionTwo"));
        }

        [Test]
        public void Finds_section_by_id()
        {
            Assert.That(DriverHelpers.Section(_driver, "sectionOne").Id, Is.EqualTo("sectionOne"));
            Assert.That(DriverHelpers.Section(_driver, "sectionTwo").Id, Is.EqualTo("sectionTwo"));
        }


        [Test]
        public void Only_finds_div_and_section()
        {
            Assert.Throws<MissingHtmlException>(() => DriverHelpers.Section(_driver, "scope1TextInputFieldId"));
            Assert.Throws<MissingHtmlException>(() => DriverHelpers.Section(_driver, "fieldsetScope2"));
        }
    }
}