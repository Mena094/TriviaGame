using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriviaGame.DTOs.Auth
{
    public class LoginRequestDTO
    {
        [Required(ErrorMessage = "El usuario es obligatorio")]
        [StringLength(50)]
        public string Usuario { get; set; } = null!;

        [Required(ErrorMessage = "La contraseña es obligatoria")]

        [StringLength(100, MinimumLength = 6, ErrorMessage = "La contraseña debe tener al menos 6 caracteres")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;
    }
}
