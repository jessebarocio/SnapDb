# SnapDb

SnapDb is a very simple framework that allows developers to persist and query small amounts (a few thousand records) of data 
easily. **This is accomplished by loading the entire datastore into memory.**

## RED ALERT

Yes, you read that right. SnapDb's entire datastore is loaded into memory when you instantiate it. I also said it was for 
**small amounts** of data. SnapDb isn't the right tool for every application but it does have valid use cases such as storing
basic user configuration or error logs in smaller applications where working with an entire DBMS or ORM tool doesn't make sense.

Carefully consider whether or not this is the right tool for your application. Speed and performance statistics will be 
published soon.

## Example

To perform CRUD operations on your SnapDb, use the generic `SnapRepository` class.

    var repository = new SnapRepository<Person>(dbFilePath);

    // Insert
    repository.Insert(new Person("John", "Doe"));
    repository.SaveChanges(); // SaveChanges writes the record to the store.

    // Get all records
    var allPeople = repository.Get();

    // Search for a records using linq
    Person doeFamilyMembers = repository.Get(p => p.LastName = "Doe");

    // Updating records is as easy as changing the object and calling SaveChanges()
    Person firstPerson = repository.Get().First();
    firstPerson.LastName = "Smith";
    repository.SaveChanges();

    // Delete records
    Person firstPerson = repository.Get().First();
    repository.Delete(firstPerson);
    repository.SaveChanges();


## Datastore Types

Right now SnapDb ships with only a File datastore. Records are serialized as json and stored in a file. Applications should not
use this datastore if multiple instances or threads will be writing to the store at the same time.

Additional datastores are coming soon. You can also create your own by implementing the `IDataStore` interface.