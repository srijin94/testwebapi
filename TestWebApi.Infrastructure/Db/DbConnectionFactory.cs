using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace TestWebApi.Infrastructure.Db
{
    public interface IDbConnectionFactory
    {
        NpgsqlConnection CreateConnection(string databaseName);
    }
    public class DbConnectionFactory : IDbConnectionFactory
    {
        private readonly IConfiguration _config;

        public DbConnectionFactory(IConfiguration config)
        {
            _config = config;
        }

        public NpgsqlConnection CreateConnection(string databaseName)
        {
            var baseConn = _config.GetConnectionString("DefaultConnection");

            var builder = new NpgsqlConnectionStringBuilder(baseConn)
            {
                Database = databaseName
            };

            return new NpgsqlConnection(builder.ConnectionString);
        }
    }
}
