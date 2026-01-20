using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TriviaGame.Application;
using TriviaGame.Application.Trivia;
using TriviaGame.DTOs.Trivia;

namespace TriviaGame.Infrastructure.Persistence.Repositories
{
    public sealed class TriviaRepository : ITriviaRepository
    {
        private readonly IDbConnectionFactory _connectionFactory;

        public TriviaRepository(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public int CrearPartida(int usuarioId, int categoriaId)
        {
            using var connection = _connectionFactory.Create();
            using var command = connection.CreateCommand();

            command.CommandText = "sp_Partida_Crear";
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(CreateParameter(command, "@UsuarioId", usuarioId));
            command.Parameters.Add(CreateParameter(command, "@CategoriaId", categoriaId));

            return Convert.ToInt32(command.ExecuteScalar());
        }

        public IEnumerable<QuestionResponseDTO> ObtenerPreguntasAleatorias(int categoriaId)
        {
            using var connection = _connectionFactory.Create();
            using var command = connection.CreateCommand();

            command.CommandText = "sp_Preguntas_ObtenerAleatoriasPorCategoria";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(CreateParameter(command, "@CategoriaId", categoriaId));

            using var reader = command.ExecuteReader();

            var preguntas = new Dictionary<int, QuestionResponseDTO>();

            while (reader.Read())
            {
                var preguntaId = reader.GetInt32(reader.GetOrdinal("IdPregunta"));

                if (!preguntas.TryGetValue(preguntaId, out var pregunta))
                {
                    pregunta = new QuestionResponseDTO
                    {
                        PreguntaId = preguntaId,
                        Pregunta = reader.GetString(reader.GetOrdinal("TextoPregunta"))
                    };

                    preguntas.Add(preguntaId, pregunta);
                }

                pregunta.Respuestas.Add(new AnswerResponseDTO
                {
                    RespuestaId = reader.GetInt32(reader.GetOrdinal("IdRespuesta")),
                    Respuesta = reader.GetString(reader.GetOrdinal("Respuesta"))
                });
            }

            return preguntas.Values;
        }


        public bool EsRespuestaCorrecta(int respuestaId)
        {
            using var connection = _connectionFactory.Create();
            using var command = connection.CreateCommand();

            command.CommandText = "sp_Respuesta_EsCorrecta";
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(CreateParameter(command, "@RespuestaId", respuestaId));

            return Convert.ToBoolean(command.ExecuteScalar());
        }

        public void GuardarDetallePartida(DetallePartidaDTO detalle)
        {
            using var connection = _connectionFactory.Create();
            using var command = connection.CreateCommand();

            command.CommandText = "sp_DetallePartida_Insertar";
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(CreateParameter(command, "@PartidaId", detalle.PartidaId));
            command.Parameters.Add(CreateParameter(command, "@PreguntaId", detalle.PreguntaId));
            command.Parameters.Add(CreateParameter(command, "@RespuestaId", detalle.RespuestaId));
            command.Parameters.Add(CreateParameter(command, "@EsCorrecta", detalle.EsCorrecta));

            command.ExecuteNonQuery();
        }

        public int FinalizarPartida(int partidaId)
        {
            using var connection = _connectionFactory.Create();
            using var command = connection.CreateCommand();

            command.CommandText = "sp_Partida_Finalizar";
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(CreateParameter(command, "@PartidaId", partidaId));

            return Convert.ToInt32(command.ExecuteScalar());
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
