using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnapDb.Tests
{
    [TestFixture]
    class TestSnapRepository
    {
        class Person
        {
            public Guid PersonId { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
        }

        [Test]
        public void Records_LoadFromSnapStore()
        {
            var people = new List<Person>();
            var snapStoreMock = new Mock<ISnapStore>();
            snapStoreMock.Setup(s => s.LoadRecords<Person>()).Returns(people);

            var repo = new SnapRepository<Person>(snapStoreMock.Object);

            Assert.AreSame(people, repo.Records);
        }
    }
}
