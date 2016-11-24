using System.IO;
using NUnit.Framework;

namespace Coypu.Drivers.Tests.Tests
{
    [TestFixture]
    internal class WhenUploadingFiles
    {
        [OneTimeSetUp]
        public void Given() => DriverSpecs.VisitTestPage();

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

                var textField = DriverHelpers.Field(DriverSpecs.Driver, "forLabeledFileFieldId");
                DriverSpecs.Driver.Set(textField, fullPath);

                var findAgain = DriverHelpers.Field(DriverSpecs.Driver, "forLabeledFileFieldId");
                Assert.That(findAgain.Value, Does.EndWith(someLocalFile));
            }
            finally
            {
                File.Delete(someLocalFile);
            }
        }
    }
}
