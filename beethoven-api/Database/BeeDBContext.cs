using beethoven_api.Database.DBModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace beethoven_api.Database;

public class BeeDBContext(DbContextOptions options) : DbContext(options)
{

    public DbSet<Entity> Entities { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<Entity>().UseTptMappingStrategy().ToTable("Entities");
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        base.ConfigureConventions(configurationBuilder);
        configurationBuilder.Properties<DateTime?>().HaveConversion<DateTimeUTCNullableConvert>();
        configurationBuilder.Properties<DateTime>().HaveConversion<DateTimeUTCConvert>();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var configBuilder = new ConfigurationBuilder()
            .AddJsonFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "appsettings.json"));
        
        IConfiguration Config = configBuilder.Build();
        string connectionString = Config.GetConnectionString("Default") ?? "";

        string centralConnectionString = Config.GetConnectionString("Default") ?? "";
        optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
    }

    public class DateTimeUTCNullableConvert: ValueConverter<DateTime?, DateTime?>
    {
        public DateTimeUTCNullableConvert(): base(
            d => d == null ? null : d.GetValueOrDefault().ToUniversalTime(),
            d => d == null ? null : DateTime.SpecifyKind(d.GetValueOrDefault(), DateTimeKind.Utc)
        )
        {

        }

    }

    public class DateTimeUTCConvert: ValueConverter<DateTime, DateTime>
    {
        public DateTimeUTCConvert(): base(
            d => d.ToUniversalTime(),
            d => DateTime.SpecifyKind(d, DateTimeKind.Utc)
        )
        {
            
        }

    }
}