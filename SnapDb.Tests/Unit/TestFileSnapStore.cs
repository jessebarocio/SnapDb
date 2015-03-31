using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnapDb.Tests.Unit
{
    [TestFixture(Category = "Unit")]
    class TestFileSnapStore
    {
        Mock<ISnapDbFile> dbFileMock;
        Mock<ISnapSerializer> serializerMock;
        FileSnapStore fileStore;

        [SetUp]
        public void SetUp()
        {
            dbFileMock = new Mock<ISnapDbFile>();
            serializerMock = new Mock<ISnapSerializer>();

            fileStore = new FileSnapStore(dbFileMock.Object, serializerMock.Object);
        }

        [Test]
        public void LoadRecords_OpensAndDeserializesFile()
        {
            using (var stream = new MemoryStream())
            {
                bool openReadWasCalled = false;
                Stream streamPassedToDeserializer = null;
                dbFileMock.Setup(s => s.OpenRead()).Returns(stream)
                    .Callback(() =>
                    {
                        openReadWasCalled = true;
                    });
                serializerMock.Setup(s => s.Deserialize<IEnumerable<Person>>(It.IsAny<Stream>()))
                    .Callback<Stream>((inputStream) =>
                    {
                        streamPassedToDeserializer = inputStream;
                    });

                var result = fileStore.LoadRecords<Person>();

                Assert.True(openReadWasCalled);
                Assert.AreSame(stream, streamPassedToDeserializer);
            }
        }

        [Test]
        public void LoadRecords_OpensAndSerializesToFile()
        {
            using (var stream = new MemoryStream())
            {
                bool openWriteWasCalled = false;
                object objectPassedToSerializer = null;
                Stream streamPassedToSerializer = null;
                dbFileMock.Setup(s => s.OpenWrite()).Returns(stream)
                    .Callback(() =>
                    {
                        openWriteWasCalled = true;
                    });
                serializerMock.Setup(s => s.Serialize(It.IsAny<object>(), It.IsAny<Stream>()))
                    .Callback<object, Stream>((obj, outputStream) =>
                    {
                        objectPassedToSerializer = obj;
                        streamPassedToSerializer = outputStream;
                    });

                fileStore.SaveRecords(new List<Person>());

                Assert.True(openWriteWasCalled);
                Assert.AreSame(stream, streamPassedToSerializer);
            }
        }
    }
}
