using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TriviaGame.Application;
using TriviaGame.Application.Categoria;
using TriviaGame.DTOs.Categorias;

namespace TriviaGame.Infrastructure.Persistence.Repositories
{
    public sealed class CategoryRepository : ICategoryRepository
    {
        private readonly IDbConnectionFactory _connectionFactory;

        public CategoryRepository(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public IEnumerable<CategoriaResponseDTO> ObtenerCategoriasActivas()
        {
            using var connection = _connectionFactory.Create();
            using var command = connection.CreateCommand();

            command.CommandText = "sp_Categorias_ListarActivas";
            command.CommandType = CommandType.StoredProcedure;

            using var reader = command.ExecuteReader();

            var categorias = new List<CategoriaResponseDTO>();

            while (reader.Read())
            {
                categorias.Add(new CategoriaResponseDTO
                {
                    IdCategoria = reader.GetInt32(0),
                    Nombre = reader.GetString(1),
                    Descripcion = reader.GetString(2)
                });
            }

            return categorias;
        }
    }
}
