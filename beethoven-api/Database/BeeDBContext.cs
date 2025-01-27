using beethoven_api.Database.DBModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace beethoven_api.Database;

public class BeeDBContext(DbContextOptions options) : DbContext(options)
{

    public DbSet<Entity> Entities { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Ticket> Tickets { get; set; }
    public DbSet<TicketStatus> TicketStatuses { get; set; }
    public DbSet<SLA> SLAs { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Customer> Customers { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<Entity>().UseTptMappingStrategy().ToTable("Entities");

        builder.Entity<User>().HasIndex(u => u.Email).IsUnique();
        builder.Entity<User>().Property(u => u.Avatar).HasDefaultValue("avatar-default.png");

        builder.Entity<Product>()
            .HasOne(p => p.SLA)
            .WithMany(p => p.Products)
            .HasForeignKey(p => p.SLAId);

        builder.Entity<Ticket>()
            .HasOne(t => t.Status)
            .WithMany(p => p.Tickets)
            .HasForeignKey(p => p.StatusId);
        
        builder.Entity<Ticket>()
            .HasOne(t => t.AssignedTo)
            .WithMany(p => p.AssignedTickets)
            .HasForeignKey(p => p.AssignedToId);
        
        builder.Entity<Ticket>()
            .HasOne(t => t.ReviewedBy)
            .WithMany(p => p.ReviewedTickets)
            .HasForeignKey(p => p.ReviewedById);

        builder.Entity<Customer>()
            .HasMany(c=>c.Products)
            .WithOne(p=>p.Customer)
            .HasForeignKey(p=>p.CustomerId);

        builder.Entity<Product>()
            .HasMany(c=>c.Tickets)
            .WithOne(p=>p.Product)
            .HasForeignKey(p=>p.ProductId);

        builder.Entity<User>()
            .HasOne(u=>u.Preferences)
            .WithOne(p=>p.User)
            .HasForeignKey<Preferences>(p=>p.UserId).OnDelete(DeleteBehavior.Cascade);
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