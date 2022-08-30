using Microsoft.AspNetCore.Mvc;

namespace ProgWebIII.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Consumes("application/json")]
    [Produces("application/json")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        public List<WeatherForecast> tempos { get; set; }

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
            tempos = Enumerable.Range(1, 5).Select(diasAMais => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(diasAMais),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToList();
        }

        //https://localhost:7030/WeatherForecast GET
        [HttpGet("/tempos/consultar")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        //public ActionResult<List<WeatherForecast>> Consultar([FromQuery] int index, string index2)
        public ActionResult<List<WeatherForecast>> Consultar()
        {
            return Ok(tempos);
        }

        //https://localhost:7030/WeatherForecast GET
        [HttpGet("/tempo/{index}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<WeatherForecast> Consultar2(int index)
        {
            if (index > 4)
            {
                return NotFound();
            }
            return Ok(tempos[index]);
        }

        //https://localhost:7030/WeatherForecast POST
        [HttpPost("/tempo/cadastrar")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<WeatherForecast> Inserir(WeatherForecast tempo)
        {
            tempos.Add(tempo);
            return StatusCode(201, tempo);
        }

        //https://localhost:7030/WeatherForecast PUT
        [HttpPut("/tempo/{index}/atualizar")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Atualizar(int index, WeatherForecast tempo)
        {
            if (index >= tempos.Count || index < 0)
            {
                return NotFound();
            }

            tempos[index] = tempo;
            return NoContent();
        }

        //https://localhost:7030/WeatherForecast DELETE
        [HttpDelete("/tempo/{index}/deletar")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult Deletar(int index)
        {
            if (index >= tempos.Count || index < 0)
            {
                return NotFound();
            }

            tempos.RemoveAt(index);
            return NoContent();
        }
    }
}