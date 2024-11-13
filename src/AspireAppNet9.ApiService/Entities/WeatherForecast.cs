namespace AspireAppNet9.ApiService;

public record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
    public Guid Id { get; set; } = Guid.NewGuid();
}
