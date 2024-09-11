using crud_litedb.Repositories;
using crud_litedb.Services;
using Microsoft.Extensions.DependencyInjection;

var serviceCollection = new ServiceCollection();
ConfigureServices(serviceCollection);

var serviceProvider = serviceCollection.BuildServiceProvider();
var personService = serviceProvider.GetService<IPersonService>();

Menu();

void ConfigureServices(IServiceCollection service)
{
    service
        .AddScoped<IPersonRepository, PersonRepository>()
        .AddScoped<IPersonService, PersonService>()
        .AddScoped<IUserInputService, UserInputService>();
}

void Menu()
{
    while (true)
    {
        Console.Clear();
        Console.Write("1 - AddPerson\n" +
                      "2 - GetPerson\n" +
                      "3 - GetAllPerson\n" +
                      "4 - UpdatePerson\n" +
                      "5 - DeletePerson\n" +
                      "0 - Exit\n" +
                      "Type a number: ");
        var choice = Console.ReadLine();
        Console.Clear();
        if(choice == "0") Environment.Exit(0);
        switch (choice)
        {
            case "1":
                personService!.AddPerson();
                break;
            case "2":
                personService!.GetPersonById();
                break;
            case "3":
                personService!.GetAllPerson();
                break;
            case "4":
                personService!.UpdatePerson();
                break;
            case "5":
                personService!.DeletePerson();
                break;
            default:
                Console.Clear();
                Console.WriteLine("Invalid choice. Type enter.");
                Console.ReadLine();
                break;
        }
    }

}
