using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using System.Data;
using TriviaGame.Application;

namespace TriviaGame.Infrastructure.Persistence.DbContext
{
    public sealed class SqlConnectionFactory : IDbConnectionFactory
    {
        private readonly string _connectionString;

        public SqlConnectionFactory(IConfiguration configuration)
        {
            _connectionString =
                configuration.GetConnectionString("sql")
                ?? throw new InvalidOperationException("Cadena de conexion no encontrada.");
        }

        public IDbConnection Create()
        {
            var connection = new SqlConnection(_connectionString);
            connection.Open();
            return connection;
        }
    }
}
