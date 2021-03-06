﻿using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnapDb.Tests.Unit
{
    [TestFixture(Category = "Unit")]
    class TestSnapRepository
    {
        List<Person> sampleData;
        Mock<ISnapStore<Person>> snapStoreMock;
        SnapRepository<Person> repository;

        [SetUp]
        public void SetUp()
        {
            sampleData = new List<Person>()
            {
                new Person()
                {
                    PersonId = Guid.NewGuid(),
                    FirstName = "John",
                    LastName = "Doe"
                },
                new Person()
                {
                    PersonId = Guid.NewGuid(),
                    FirstName = "Jane",
                    LastName = "Doe"
                },
            };

            // Mock ISnapStore
            snapStoreMock = new Mock<ISnapStore<Person>>();
            snapStoreMock.Setup(s => s.LoadRecords()).Returns(sampleData);

            repository = new SnapRepository<Person>(snapStoreMock.Object);
        }

        [Test]
        public void Records_LoadFromSnapStore()
        {
            var result = repository.Records;

            Assert.AreEqual(sampleData.Count, result.Count());
            foreach (var item in result)
            {
                Assert.True(sampleData.Contains(item));
            }
        }

        [Test]
        public void Get_ReturnsAllRecordsIfNoFilterIsPassed()
        {
            var result = repository.Get();

            Assert.AreEqual(sampleData.Count, result.Count());
            foreach (var item in result)
            {
                Assert.True(sampleData.Contains(item));
            }
        }

        [Test]
        public void Get_ReturnsFilteredRecords()
        {
            var result = repository.Get(p => p.FirstName == "John");

            Assert.AreEqual(1, result.Count());
            Assert.AreEqual(sampleData.First().PersonId, result.First().PersonId);
        }

        [Test]
        public void Insert_AddsItemToRecords()
        {
            var newPerson = new Person()
            {
                PersonId = Guid.NewGuid(),
                FirstName = "John",
                LastName = "Doe"
            };

            repository.Insert(newPerson);

            Assert.Contains(newPerson, repository.Records);
        }

        [Test]
        public void Delete_RemovesItemFromRecords()
        {
            var itemToRemove = sampleData.First();
            Assert.True(repository.Records.Contains(itemToRemove));

            repository.Delete(itemToRemove);

            Assert.False(repository.Records.Contains(itemToRemove));
        }

        [Test]
        public void SaveChanges_SavesAllRecordsToSnapStore()
        {
            bool saveCalled = false;
            IEnumerable<Person> recordsToSave = null;
            snapStoreMock.Setup(s => s.SaveRecords(It.IsAny<IEnumerable<Person>>()))
                .Callback<IEnumerable<Person>>((people) =>
                {
                    saveCalled = true;
                    recordsToSave = people;
                });

            repository.SaveChanges();

            Assert.True(saveCalled);
            Assert.AreSame(repository.Records, recordsToSave);
        }
    }
}
