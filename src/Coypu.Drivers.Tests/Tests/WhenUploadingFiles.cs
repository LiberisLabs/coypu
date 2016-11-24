using System.IO;
using NUnit.Framework;

namespace Coypu.Drivers.Tests.Tests
{
    [TestFixture]
    internal class WhenUploadingFiles
    {
        private IDriver _driver;

        [OneTimeSetUp]
        public void Given() => _driver = TestDriver.Instance();

        [Test]
        public void Sets_the_path_to_be_uploaded()
        {
            const string someLocalFile = @"local.file";
            try
            {
                var directoryInfo = new DirectoryInfo(".");
                var fullPath = Path.Combine(directoryInfo.FullName, someLocalFile);
                using (File.Create(fullPath))
                {
                }

                var textField = DriverHelpers.Field(_driver, "forLabeledFileFieldId");
                _driver.Set(textField, fullPath);

                var findAgain = DriverHelpers.Field(_driver, "forLabeledFileFieldId");
                Assert.That(findAgain.Value, Does.EndWith(someLocalFile));
            }
            finally
            {
                File.Delete(someLocalFile);
            }
        }
    }
}
