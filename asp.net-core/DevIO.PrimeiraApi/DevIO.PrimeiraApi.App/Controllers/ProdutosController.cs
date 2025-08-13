using DevIO.PrimeiraApi.App.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DevIO.PrimeiraApi.App.Controllers;

[Authorize]
[ApiController]
[Route("api/v1/produtos")]
public class ProdutosController : ControllerBase
{
    private readonly ApiDbContext _context;

    public ProdutosController(ApiDbContext context)
    {
        _context = context;
    }

    [AllowAnonymous]
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
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary), StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType]
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
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<Produto>> PutProduto(int id, Produto produto)
    {
        if (id != produto.Id) return BadRequest();
        
        if(!ModelState.IsValid) return ValidationProblem(ModelState);

        _context.Entry(produto).State = EntityState.Modified;
        
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException e)
        {
            if (await _context.Produtos.FirstOrDefaultAsync(p => p.Id == produto.Id) is null)
            {
                return NotFound();
            }

            throw;
        }
        
        return NoContent();
    }
    
    [Authorize(Roles = "Admin")]
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<Produto>> DeleteProduto(int id)
    {
        var produto = await _context.Produtos.FindAsync(id);
        
        if (produto == null) return NotFound();
        
        _context.Produtos.Remove(produto);
        await _context.SaveChangesAsync();
        
        return NoContent();
    }
}