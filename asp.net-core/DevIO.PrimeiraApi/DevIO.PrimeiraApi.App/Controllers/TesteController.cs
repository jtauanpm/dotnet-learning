using DevIO.PrimeiraApi.App.Entities;
using Microsoft.AspNetCore.Mvc;

namespace DevIO.PrimeiraApi.App.Controllers;

[ApiController]
[Route("demo")]
public class TesteController : ControllerBase
{
    [HttpGet]
    [ProducesResponseType<Produto>(statusCode: StatusCodes.Status200OK)]
    public IActionResult Get()
    {
        return Ok(new Produto {Id = 1, Nome = "Teste"});
    }
     
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
    [ProducesResponseType(statusCode: StatusCodes.Status204NoContent)]
    [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest)]
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