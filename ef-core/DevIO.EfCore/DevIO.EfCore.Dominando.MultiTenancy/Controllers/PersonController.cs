using DevIO.EfCore.Dominando.MultiTenancy.EFCore;
using DevIO.EfCore.Dominando.MultiTenancy.Entities;
using Microsoft.AspNetCore.Mvc;

namespace DevIO.EfCore.Dominando.MultiTenancy.Controllers;

[ApiController]
[Route("[controller]")]
public class PersonController : ControllerBase
{
    private readonly ILogger<PersonController> _logger;
    private readonly MultiTenancyDbContext _context;

    public PersonController(ILogger<PersonController> logger, MultiTenancyDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    [HttpGet]
    public IEnumerable<Person> Get()
    {
        var people = _context.People.ToList();
        
        return people;
    }
}