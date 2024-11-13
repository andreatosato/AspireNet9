using AspireAppNet9.ApiService;
using AspireAppNet9.ApiService.Entities;

var builder = WebApplication.CreateBuilder(args);
builder.AddServiceDefaults();
builder.AddSqlServerDbContext<WeatherContext>("weatherDb");
builder.Services.AddProblemDetails();
builder.Services.AddOpenApi();

var app = builder.Build();

await (app.Services.CreateAsyncScope().ServiceProvider).GetService<WeatherContext>()!.Database.EnsureCreatedAsync();
var conn = app.Configuration.GetValue<string>("ConnectionStrings:weatherDb");
// Configure the HTTP request pipeline.
app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

string[] summaries = ["Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"];

app.MapGet("/weatherforecast", async (WeatherContext db) =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();

    await db.Forecasts.AddRangeAsync(forecast);
    await db.SaveChangesAsync();
    return forecast;
})
.WithName("GetWeatherForecast");

app.MapDefaultEndpoints();

app.Run();

