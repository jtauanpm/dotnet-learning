using crud_litedb.Models;
using LiteDB;

namespace crud_litedb.Repositories;

public class PersonRepository : IPersonRepository
{
    public void AddPerson(Person person)
    {
        var db = CreateDb();
        db.GetCollection<Person>().Insert(person);
        db.Commit();
    }

    public Person? GetPersonById(int id)
    {
        var db = CreateDb();
        return !db.CollectionExists("Person") ? 
            null : db.GetCollection<Person>().FindOne(p => p.Id == id);
    }

    public IEnumerable<Person>? GetAllPerson()
    {
        var db = CreateDb();
        return !db.CollectionExists("Person") ? null : db.GetCollection<Person>().FindAll();
    }

    public bool UpdatePerson(Person person)
    {
        var db = CreateDb();
        if (!db.CollectionExists("Person")) return false;
        var response = db.GetCollection<Person>().Update(person);
        db.Commit();
        return response;
    }

    public bool DeletePerson(int id)
    {
        var db = CreateDb();
        if (!db.CollectionExists("Person")) return false;
        var response = db.GetCollection<Person>().Delete(id);
        db.Commit();
        return response;
    }
    
    private static LiteDatabase CreateDb()
    {
        return new LiteDatabase("Data.db");
    }
}