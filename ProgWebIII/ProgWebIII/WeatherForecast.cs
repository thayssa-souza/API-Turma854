using System.ComponentModel.DataAnnotations;

namespace ProgWebIII
{
    public class WeatherForecast
    {
        [Required(ErrorMessage = "Data é obrigatoria")]
        public DateTime? Date { get; set; }
        
        [Range(-50, 50)]
        [Required]
        public int TemperatureC { get; set; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);


        [MaxLength(11, ErrorMessage = "Sumario deve conter até 11 caracteres")]
        public string? Summary { get; set; }
    }
}