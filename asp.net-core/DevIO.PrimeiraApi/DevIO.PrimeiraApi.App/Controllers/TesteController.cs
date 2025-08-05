using DevIO.PrimeiraApi.App.Entities;
using Microsoft.AspNetCore.Mvc;

namespace DevIO.PrimeiraApi.App.Controllers;

[ApiController]
[Route("demo")]
public class TesteController : ControllerBase
{
     [HttpGet]
     public IActionResult Get()
     {
          return Ok(new Produto {Id = 1, Nome = "Teste"});
     }
     
     [HttpGet("{id:int}")]
     public IActionResult Get(int id)
     {
          return Ok(new Produto {Id = 1, Nome = "Teste"});
     }
     
     [HttpPost]
     public IActionResult Post([FromBody] Produto produto)
     {
          return CreatedAtAction("Get", new { Id = produto.Id }, produto);
     }

     [HttpPut("{id:int}")]
     public IActionResult Put(int id, [FromBody] Produto produto)
     {
          if(id != produto.Id) return BadRequest();

          return NoContent();
     }

     [HttpDelete("{id:int}")]
     public IActionResult Delete(int id)
     {
          return NoContent();
     }
}