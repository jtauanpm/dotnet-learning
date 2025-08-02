using Microsoft.AspNetCore.Mvc;

namespace DevIO.PrimeiraApi.App.Controllers;

[ApiController]
[Route("api/minha-controller")]
// [Route("[controller]")] utiliza nome da classe como recurso, ex: WeatherForecast
public class WeatherForecastController : ControllerBase
{
    // [Route("")] Pode ser usado para definir rotas em métodos
    [HttpGet("teste")]
    public IActionResult Get()
    {
        return Ok();
    }
    
    // Constraints para parâmetros de rota: 
    // https://learn.microsoft.com/en-us/aspnet/core/fundamentals/routing?view=aspnetcore-9.0#route-constraints
    // Parâmetros de rota
    [HttpGet("{id:int}/dado/{id2:int}")]
    public IActionResult Get(int id, int id2)
    {
        return Ok();
    }
}