using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TriviaGame.Application;
using TriviaGame.Application.DTOs;
using TriviaGame.Application.Auth;

namespace TriviaGame.Infrastructure.Persistence.Repositories
{
    public sealed class UserRepository : IUserRepository
    {
        private readonly IDbConnectionFactory _connectionFactory;

        public UserRepository(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public bool UsuarioExiste(string nombre)
        {
            using var connection = _connectionFactory.Create();
            using var command = connection.CreateCommand();

            command.CommandText = "sp_Usuario_Existe";
            command.CommandType = CommandType.StoredProcedure;

            var param = command.CreateParameter();
            param.ParameterName = "@Nombre";
            param.Value = nombre;
            command.Parameters.Add(param);

            var result = Convert.ToInt32(command.ExecuteScalar());
            return result > 0;
        }

        public int RegistrarUsuario(string nombre, string passwordHash)
        {
            using var connection = _connectionFactory.Create();
            using var command = connection.CreateCommand();

            command.CommandText = "sp_Usuario_Registrar";
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(CreateParameter(command, "@Nombre", nombre));
            command.Parameters.Add(CreateParameter(command, "@PasswordHash", passwordHash));

            return Convert.ToInt32(command.ExecuteScalar());
        }

        public UsuarioAuthDTO? ObtenerUsuarioPorNombre(string nombre)
        {
            using var connection = _connectionFactory.Create();
            using var command = connection.CreateCommand();

            command.CommandText = "sp_Usuario_ObtenerPorNombre";
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(CreateParameter(command, "@Nombre", nombre));

            using var reader = command.ExecuteReader();

            if (!reader.Read())
                return null;

            return new UsuarioAuthDTO
            {
                IdUsuario = reader.GetInt32(reader.GetOrdinal("IdUsuario")),
                Nombre = reader.GetString(reader.GetOrdinal("Nombre")),
                PasswordHash = reader.GetString(reader.GetOrdinal("PasswordHash")),
                Estado = reader.GetBoolean(reader.GetOrdinal("Estado"))
            };
        }

        private static IDbDataParameter CreateParameter(
            IDbCommand command,
            string name,
            object value)
        {
            var parameter = command.CreateParameter();
            parameter.ParameterName = name;
            parameter.Value = value;
            return parameter;
        }
    }
}
