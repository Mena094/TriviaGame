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
        [Required]
        [StringLength(50)]
        public string Usuario { get; set; } = null!;

        [Required]
        public string Password { get; set; } = null!;
    }
}
