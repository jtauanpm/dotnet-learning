using crud_litedb.Models;
using crud_litedb.Repositories;

namespace crud_litedb.Services;

public class PersonService : IPersonService
{
    private readonly IPersonRepository _personRepository;
    private readonly IUserInputService _userInputService;
    
    public PersonService(IPersonRepository personRepository, IUserInputService userInputService)
    {
        _personRepository = personRepository;
        _userInputService = userInputService;
    }

    public void AddPerson()
    {
        var person = _userInputService.FillPerson();
        if (person != null) _personRepository.AddPerson(person);
    }

    public void GetPersonById()
    {
        var id = _userInputService.GetId();
        if (id is null) return;
        var person = _personRepository.GetPersonById(id.Value);
        Console.WriteLine(person is not null ? person.ToString() : $"No person found with ID {id}");
        _userInputService.FlowPause();
    }

    public void GetAllPerson()
    {
        var persons = (_personRepository.GetAllPerson() ?? Array.Empty<Person>()).ToList();
        if (persons.Count == 0)
        {
            Console.WriteLine("No person found");
            _userInputService.FlowPause();
            return;
        }
        foreach (var person in persons.ToList())
        {
            Console.WriteLine($"{person}\n");
        }
        _userInputService.FlowPause();
    }

    public void UpdatePerson()
    {
        var id = _userInputService.GetId();
        if (id == null) return;
        var person = _personRepository.GetPersonById(id.Value);
        if (person is null)
        {
            Console.WriteLine($"No person found with ID {id}");
            _userInputService.FlowPause();
            return;
        }
        
        person = _userInputService.FillPerson(id.Value);
        if (person == null) return;
        var response = _personRepository.UpdatePerson(person); //TODO: Throw Exception
        if (response) return;
        Console.WriteLine($"Error when trying to update person with id {id}");
        _userInputService.FlowPause();
    }

    public void DeletePerson()
    {
        var id = _userInputService.GetId();
        if (id is null) return;
        if (_personRepository.DeletePerson(id.Value)) return;
        Console.WriteLine($"Error when trying to delete person with id {id}");
        _userInputService.FlowPause();
    }
}