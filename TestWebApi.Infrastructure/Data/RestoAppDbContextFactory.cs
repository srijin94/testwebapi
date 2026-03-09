using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace TestWebApi.Infrastructure.Data
{
    public interface IDbContextFactory
    {
        AppDbContext CreateDbContext(string databaseName);
    }

    public class RestoAppDbContextFactory : IDbContextFactory
    {
        private readonly IConfiguration _configuration;
        public RestoAppDbContextFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public AppDbContext CreateDbContext(string databaseName)
        {
            var host = _configuration["DB_HOST"] ?? "localhost";
            var port = int.TryParse(_configuration["DB_PORT"], out var p) ? p : 5432;
            var user = _configuration["DB_USER"] ?? "postgres";
            var password = _configuration["DB_PASSWORD"] ?? "";

            // Build connection string dynamically
            var builder = new NpgsqlConnectionStringBuilder
            {
                Host = host,
                Port = port,
                Username = user,
                Password = password,
                Database = databaseName,
                SslMode = SslMode.Disable
            };

            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseNpgsql(builder.ConnectionString)
                .UseSnakeCaseNamingConvention()
                .Options;

            return new AppDbContext(options);
        }
    }
}
