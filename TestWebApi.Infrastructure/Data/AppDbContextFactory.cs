using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using TestWebApi.Infrastructure.Data;

public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        optionsBuilder.UseNpgsql(
            "Host=192.168.100.36;Port=5432;Database=EcommerceDb;Username=postgres;Password=0000;Ssl Mode=Disable;"
        ).UseSnakeCaseNamingConvention();

        return new AppDbContext(optionsBuilder.Options);
    }
}