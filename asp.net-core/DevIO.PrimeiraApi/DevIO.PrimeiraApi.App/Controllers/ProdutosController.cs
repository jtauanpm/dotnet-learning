using DevIO.PrimeiraApi.App.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DevIO.PrimeiraApi.App.Controllers;

[ApiController]
[Route("api/v1/produtos")]
public class ProdutosController : ControllerBase
{
    private readonly ApiDbContext _context;

    public ProdutosController(ApiDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Produto>>> GetProdutos()
    {
        return await _context.Produtos.ToListAsync();
    }
    
    [HttpGet("{id:int}")]
    public async Task<ActionResult<Produto>> GetProduto(int id)
    {
        var produto = await _context.Produtos.FindAsync(id);
        if (produto != null) return produto;
        return produto != null ? produto: NotFound();
    }
    
    [HttpPost]
    public async Task<ActionResult<Produto>> PostProduto(Produto produto)
    {
        if (!ModelState.IsValid)
        {
            // Retorna os erros simplificados
            // return BadRequest(ModelState);
            
            // Retorna o erro padrão, maneira mais correta de acordo com RFC
            return ValidationProblem(ModelState);
            
            // Personalizar mensagem de erro
            // return ValidationProblem(new ValidationProblemDetails
            // {
            //     Title = "Um ou mais erros de validação ocorretam!",
            // });
        }
        
        _context.Produtos.Add(produto);
        await _context.SaveChangesAsync();
        
        return CreatedAtAction(nameof(GetProduto), new { id = produto.Id }, produto);
    }
    
    [HttpPut("{id:int}")]
    public async Task<ActionResult<Produto>> PutProduto(int id, Produto produto)
    {
        if (id != produto.Id) return BadRequest();
        
        if(!ModelState.IsValid) return ValidationProblem(ModelState);
        
        _context.Produtos.Update(produto);
        await _context.SaveChangesAsync();
        
        return NoContent();
    }
    
    [HttpDelete("{id:int}")]
    public async Task<ActionResult<Produto>> DeleteProduto(int id)
    {
        var produto = await _context.Produtos.FindAsync(id);
        
        _context.Produtos.Remove(produto);
        await _context.SaveChangesAsync();
        
        return NoContent();
    }
}