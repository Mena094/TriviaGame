using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriviaGame.Application.DTOs
{
    public class UsuarioAuthDTO
    {
        public int IdUsuario { get; set; }
        public string Nombre { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public bool Estado { get; set; }
    }
}
