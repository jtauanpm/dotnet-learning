using crud_litedb.Models;
using LiteDB;

namespace crud_litedb.Repositories;

public interface IPersonRepository
{
    public void AddPerson(Person person);

    public Person? GetPersonById(int id);

    public IEnumerable<Person>? GetAllPerson();

    public bool UpdatePerson(Person person);

    public bool DeletePerson(int id);
}