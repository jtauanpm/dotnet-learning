using DevIO.PrimeiraApi.App.Entities;
using Microsoft.AspNetCore.Mvc;

namespace DevIO.PrimeiraApi.App.Controllers;

[ApiController]
[Route("api/demo")]
// [Route("[controller]")] utiliza nome da classe como recurso, ex: Teste
public class TesteController : ControllerBase
{
    
    [HttpGet]
    // [Route("")] também pode ser usado para definir rotas em métodos
    [ProducesResponseType<Produto>(statusCode: StatusCodes.Status200OK)]
    public IActionResult Get()
    {
        return Ok(new Produto {Id = 1, Nome = "Teste"});
    }
    
    // Constraints para parâmetros de rota: 
    // https://learn.microsoft.com/en-us/aspnet/core/fundamentals/routing?view=aspnetcore-9.0#route-constraints
    // Parâmetros de rota
    [HttpGet("{id:int}")]
    [ProducesResponseType<Produto>(statusCode: StatusCodes.Status200OK)]
    [ProducesResponseType(statusCode: StatusCodes.Status404NotFound)]
    public IActionResult Get(int id)
    {
        return Ok(new Produto {Id = 1, Nome = "Teste"});
    }
     
    [HttpPost]
    [ProducesResponseType<Produto>(statusCode: StatusCodes.Status201Created)]
    [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest)]
    public IActionResult Post([FromBody] Produto produto)
    {
        return CreatedAtAction("Get", new { produto.Id }, produto);
    }

    [HttpPut("{id:int}")]
    [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Put))]
    public IActionResult Put(int id, [FromBody] Produto produto)
    {
        if(id != produto.Id) return BadRequest();

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(statusCode: StatusCodes.Status204NoContent)]
    [ProducesResponseType(statusCode: StatusCodes.Status404NotFound)]
    public IActionResult Delete(int id)
    {
        return NoContent();
    }
}