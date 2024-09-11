using crud_litedb.Models;

namespace crud_litedb.Services;

public class UserInputService : IUserInputService
{
    public Person? FillPerson(int id = 0)
    {
        Console.Write("Type a age: ");
        var inputAge = Console.ReadLine();
        if (!int.TryParse(inputAge, out var age))
        {
            Console.WriteLine("Invalid Age");
            FlowPause();
            return null;
        }
        Console.Write("Type a name: ");
        var name = Console.ReadLine();

        return new Person
        {
            Name = name,
            Age = age,
            Id = id
        };
    }

    public int? GetId()
    {
        Console.Write("Type a Id: ");
        var inputId = Console.ReadLine();
        if (int.TryParse(inputId, out var id)) return id;
        Console.WriteLine("Invalid ID");
        FlowPause();
        return null;
    }
    
    public void FlowPause()
    {
        Console.WriteLine("Type enter to continue...");
        Console.ReadLine();
    }
}