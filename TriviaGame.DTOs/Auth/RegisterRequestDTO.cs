using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriviaGame.DTOs.Auth
{
    public class RegisterRequestDTO
    {
        [Required(ErrorMessage = "El usuario es obligatorio")]
        [StringLength(50, ErrorMessage = "El usuario no puede exceder 50 caracteres")]
        public string Usuario { get; set; } = null!;

        [Required(ErrorMessage = "La contraseña es obligatoria")]
        [MinLength(6, ErrorMessage = "La contraseña debe tener al menos 6 caracteres")]
        public string Password { get; set; } = null!;
    }
}
