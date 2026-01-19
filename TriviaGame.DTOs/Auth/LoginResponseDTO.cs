using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriviaGame.DTOs.Auth
{
    public class LoginResponseDTO
    {
        public int UsuarioId { get; set; }
        public string Usuario { get; set; } = null!;
        public bool Autenticado { get; set; }
        public string? Mensaje { get; set; }
    }
}
