using Microsoft.EntityFrameworkCore;

namespace AspireAppNet9.ApiService.Entities;

public class WeatherContext : DbContext
{
    public WeatherContext(DbContextOptions op) : base(op)
    {
        
    }

    
    public DbSet<WeatherForecast> Forecasts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<WeatherForecast>().HasKey(x => x.Id);
    }
}
