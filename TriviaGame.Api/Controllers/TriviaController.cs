using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using TriviaGame.Application.Trivia;
using TriviaGame.DTOs.Trivia;


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
                var claimId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                              ?? User.FindFirst("sub")?.Value;

                var claimName = User.FindFirst(ClaimTypes.Name)?.Value
                                ?? User.FindFirst("unique_name")?.Value;

                if (claimId == null || !int.TryParse(claimId, out int usuarioId))
                    return Unauthorized(new { mensaje = "Token inválido: no se encontró el ID del usuario" });

           

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
                bool esCorrecta = _triviaRepository.EsRespuestaCorrecta(detalle.RespuestaId);

                detalle.EsCorrecta = esCorrecta;

                // 3. Ahora sí, guardar en DetallePartida
                _triviaRepository.GuardarDetallePartida(detalle);

                return Ok(new { mensaje = "Respuesta procesada", correcta = esCorrecta });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = $"Error: {ex.Message}" });
            }
        }

        // POST: api/trivia/finalizar/{partidaId}
        [HttpPost("finalizar/{partidaId}")]
        public IActionResult FinalizarPartida(int partidaId)
        {
            try
            {
                decimal puntaje = _triviaRepository.FinalizarPartida(partidaId);

                var response = new FinalizarPartidaResponse
                {
                    Puntaje = puntaje,
                    Mensaje = "Partida finalizada."
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = $"Error al finalizar partida: {ex.Message}" });
            }
        }
    }
}
