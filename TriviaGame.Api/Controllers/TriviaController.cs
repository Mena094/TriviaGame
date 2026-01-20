using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TriviaGame.Application.Trivia;
using TriviaGame.DTOs.Trivia;
using System.IdentityModel.Tokens.Jwt;


namespace TriviaGame.Api.Controllers
{
    [ApiController]
    [Route("api/trivia")]
    [Authorize] 
    public class TriviaController : ControllerBase
    {
        private readonly ITriviaRepository _triviaRepository;

        public TriviaController(ITriviaRepository triviaRepository)
        {
            _triviaRepository = triviaRepository;
        }

        // POST: api/trivia/crear-partida
        [HttpPost("crear-partida")]
        public IActionResult CrearPartida([FromBody] StartTriviaRequestDTO request)
        {
            try
            {
                var claimId = User.FindFirst("userId")?.Value;
                var claimName = User.FindFirst("userName")?.Value;

                if (claimId == null || !int.TryParse(claimId, out int usuarioId))
                    return Unauthorized(new { mensaje = "Token inválido: no se encontro el Id del usuario" });


                // Creat partida para el usuario autenticado
                var partidaId = _triviaRepository.CrearPartida(usuarioId, request.CategoriaId);

                return Ok(new { PartidaId = partidaId, mensaje = "Partida creada correctamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = $"Error al crear partida: {ex.Message}" });
            }
        }

        // GET: api/trivia/preguntas/{categoriaId}
        [HttpGet("preguntas/{categoriaId}")]
        public IActionResult ObtenerPreguntasAleatorias(int categoriaId)
        {
            try
            {
                var preguntas = _triviaRepository.ObtenerPreguntasAleatorias(categoriaId);
                return Ok(preguntas);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = $"Error al obtener preguntas: {ex.Message}" });
            }
        }

        // POST: api/trivia/responder
        [HttpPost("responder")]
        public IActionResult GuardarDetalle([FromBody] DetallePartidaDTO detalle)
        {
            try
            {
                _triviaRepository.GuardarDetallePartida(detalle);
                return Ok(new { mensaje = "Respuesta guardada correctamente." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = $"Error al guardar detalle: {ex.Message}" });
            }
        }

        // POST: api/trivia/finalizar/{partidaId}
        [HttpPost("finalizar/{partidaId}")]
        public IActionResult FinalizarPartida(int partidaId)
        {
            try
            {
                var puntaje = _triviaRepository.FinalizarPartida(partidaId);
                return Ok(new { Puntaje = puntaje, mensaje = "Partida finalizada." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = $"Error al finalizar partida: {ex.Message}" });
            }
        }
    }
}
