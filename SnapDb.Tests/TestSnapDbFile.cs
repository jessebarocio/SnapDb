using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnapDb.Tests
{
    [TestFixture(Category = "Integration")]
    class TestSnapDbFile
    {
        string dbFilePath = @"..\..\..\TestData\SampleDb.json";
        string newDbFilePath = @"..\..\..\TestData\NewSampleDb.json";

        [TearDown]
        public void TearDown()
        {
            File.Delete(newDbFilePath);
        }

        [Test]
        public void OpenRead_ReturnsFileStream()
        {
            var dbFile = new SnapDbFile(dbFilePath);
            using (var stream = dbFile.OpenRead())
            {
                Assert.IsInstanceOf<FileStream>(stream);
                var fileStream = (FileStream)stream;
                Assert.True(fileStream.CanRead);
                Assert.False(fileStream.CanWrite);
            }
        }

        [Test]
        public void OpenRead_CreatesFileIfDoesntExist()
        {
            var dbFile = new SnapDbFile(newDbFilePath);
            Assert.False(File.Exists(newDbFilePath));
            using (var stream = dbFile.OpenRead())
            {
                Assert.True(File.Exists(newDbFilePath));
            }
        }

        [Test]
        public void OpenWrite_ReturnsFileStreamForWriting()
        {
            var dbFile = new SnapDbFile(dbFilePath);
            using (var stream = dbFile.OpenWrite())
            {
                Assert.IsInstanceOf<FileStream>(stream);
                var fileStream = (FileStream)stream;
                Assert.True(fileStream.CanRead);
                Assert.True(fileStream.CanWrite);
            }
        }
    }
}
