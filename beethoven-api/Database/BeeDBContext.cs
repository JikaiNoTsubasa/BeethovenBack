using beethoven_api.Database.DBModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace beethoven_api.Database;

public class BeeDBContext(DbContextOptions options) : DbContext(options)
{

    public DbSet<Entity> Entities { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Preferences> Preferences { get; set; }
    public DbSet<Team> Teams { get; set; }
    public DbSet<Message> Messages { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<Entity>().UseTptMappingStrategy().ToTable("Entities");

        builder.Entity<User>().HasIndex(u => u.Email).IsUnique();
        builder.Entity<User>().Property(u => u.Avatar).HasDefaultValue("avatar-default.png");

        builder.Entity<User>()
            .HasOne(u => u.Preferences)
            .WithOne(p => p.User)
            .HasForeignKey<Preferences>(p => p.UserId).OnDelete(DeleteBehavior.Cascade);

        builder.Entity<Team>()
            .HasMany(t => t.Members)
            .WithMany(u => u.Teams);

        builder.Entity<Project>()
            .HasMany(u => u.Phases)
            .WithOne(t => t.Project)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<Project>()
            .HasOne(u => u.CurrentPhase)
            .WithOne(t => t.Project);

        builder.Entity<Document>()
            .HasMany(d => d.Versions)
            .WithOne(v => v.Document)
            .OnDelete(DeleteBehavior.Cascade);
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
        optionsBuilder.UseNpgsql(connectionString);
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