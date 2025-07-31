using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace beethoven_api.Database;

public class BeeDBContextFactory : IDesignTimeDbContextFactory<BeeDBContext>
{
    public BeeDBContext CreateDbContext(string[] args)
    {
        var configBuilder = new ConfigurationBuilder()
            .AddJsonFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "appsettings.json"));
        
        IConfiguration Config = configBuilder.Build();
        var optionsBuilder = new DbContextOptionsBuilder<BeeDBContext>();

        string centralConnectionString = Config.GetConnectionString("Default") ?? "";

        optionsBuilder.UseNpgsql(centralConnectionString);

        return new BeeDBContext(optionsBuilder.Options);
    }
}