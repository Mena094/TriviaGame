using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TriviaGame.Application.Categoria;

namespace TriviaGame.Api.Controllers
{
    [ApiController]
    [Route("api/categorias")]
    [Authorize] 
    public class CategoriaController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoriaController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        [HttpGet("activas")]
        public IActionResult ObtenerCategoriasActivas()
        {
            try
            {
                var categorias = _categoryRepository.ObtenerCategoriasActivas();
                return Ok(categorias);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = $"Error al obtener categorias: {ex.Message}" });
            }
        }
    }
}
