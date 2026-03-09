using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using TestWebApi.Infrastructure.Data;

public class MainDbContextFactory : IDesignTimeDbContextFactory<MainDbContext>
{
    public MainDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<MainDbContext>();
        optionsBuilder.UseNpgsql(
            "Host=192.168.100.36;Port=5432;Database=PackmanDb;Username=postgres;Password=0000;Ssl Mode=Disable;"
        ).UseSnakeCaseNamingConvention();

        return new MainDbContext(optionsBuilder.Options);
    }
}
