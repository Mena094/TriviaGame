using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TriviaGame.Application.Auth;
using TriviaGame.DTOs.Auth;

namespace TriviaGame.Api.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly ITokenService _tokenService;

        public AuthController(
            IUserRepository userRepository,
            IPasswordHasher passwordHasher,
            ITokenService tokenService)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _tokenService = tokenService;
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterRequestDTO request)
        {
            if (string.IsNullOrWhiteSpace(request.Usuario) ||
                string.IsNullOrWhiteSpace(request.Password))
            {
                return BadRequest(new LoginResponseDTO
                {
                    Autenticado = false,
                    Mensaje = "Usuario y contraseña son obligatorios."
                });
            }

            if (_userRepository.UsuarioExiste(request.Usuario))
            {
                return Conflict(new LoginResponseDTO
                {
                    Autenticado = false,
                    Mensaje = "El usuario ya existe."
                });
            }

            var passwordHash = _passwordHasher.Hash(request.Password);

            var usuarioId = _userRepository.RegistrarUsuario(
                request.Usuario,
                passwordHash
            );

            return Created("", new LoginResponseDTO
            {
                UsuarioId = usuarioId,
                Usuario = request.Usuario,
                Autenticado = true,
                Mensaje = "Usuario registrado exitosamente."
            });
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequestDTO request)
        {
            var usuario = _userRepository.ObtenerUsuarioPorNombre(request.Usuario);

            var passwordValida = usuario != null &&
                                 !string.IsNullOrWhiteSpace(usuario.Nombre) &&
                                 _passwordHasher.Verify(request.Password, usuario.PasswordHash);

            if (!passwordValida)
            {
                return Unauthorized(new LoginResponseDTO
                {
                    Autenticado = false,
                    Mensaje = "Credenciales inválidas."
                });
            }

            string token = _tokenService.GenerateToken(usuario.IdUsuario, usuario.Nombre);

            return Ok(new LoginResponseDTO
            {
                UsuarioId = usuario.IdUsuario,
                Usuario = usuario.Nombre,
                Autenticado = true,
                Token = token,
                Mensaje = "Login exitoso"
            });
        }


    }
}
