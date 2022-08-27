using Microsoft.AspNetCore.Mvc;

namespace ProgWebIII.Controllers
{
    [ApiController]
    [Route("[controller]")]
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
        [HttpGet]
        public List<WeatherForecast> Consultar(int index)
        {
            return tempos;
        }

        //https://localhost:7030/WeatherForecast POST
        [HttpPost]
        public WeatherForecast Inserir(WeatherForecast tempo)
        {
            tempos.Add(tempo);
            return tempo;
        }

        //https://localhost:7030/WeatherForecast PUT
        [HttpPut]
        public WeatherForecast Atualizar(int index, WeatherForecast tempo)
        {
            tempos[index] = tempo;
            return tempos[index];
        }

        //https://localhost:7030/WeatherForecast DELETE
        [HttpDelete]
        public List<WeatherForecast> Deletar(int index)
        {
            tempos.RemoveAt(index);
            return tempos;
        }
    }
}