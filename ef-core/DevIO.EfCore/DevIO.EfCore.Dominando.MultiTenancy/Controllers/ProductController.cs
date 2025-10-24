using DevIO.EfCore.Dominando.MultiTenancy.EFCore;
using DevIO.EfCore.Dominando.MultiTenancy.Entities;
using Microsoft.AspNetCore.Mvc;

namespace DevIO.EfCore.Dominando.MultiTenancy.Controllers;

[ApiController]
[Route("{tenant}/[controller]")]
public class ProductController : ControllerBase
{
    private readonly ILogger<ProductController> _logger;
    private readonly MultiTenancyDbContext _context;

    public ProductController(ILogger<ProductController> logger, MultiTenancyDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    [HttpGet]
    public IEnumerable<Product> Get()
    {
        var products = _context.Products.ToList();
        return products;
    }
}