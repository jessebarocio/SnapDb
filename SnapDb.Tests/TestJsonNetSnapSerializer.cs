using ApprovalTests;
using ApprovalTests.Reporters;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnapDb.Tests
{
    [TestFixture]
    [UseReporter(typeof(DiffReporter))]
    class TestJsonNetSnapSerializer
    {
        Person samplePerson = new Person()
        {
            PersonId = Guid.Parse("e71f68e0-34b4-4fa4-b375-26923af4e40d"),
            FirstName = "John",
            LastName = "Doe"
        };

        [Test]
        public void Serialize_SerializesObjectCorrectly()
        {
            var serializer = new JsonNetSnapSerializer();
            using (var stream = new MemoryStream())
            using (var streamReader = new StreamReader(stream))
            {
                serializer.Serialize(samplePerson, stream);
                var result = Encoding.Default.GetString(stream.ToArray());
                Approvals.Verify(result);
            }
        }
    }
}
