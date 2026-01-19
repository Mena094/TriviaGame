using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriviaGame.DTOs.Trivia
{
    public class StartTriviaRequestDTO
    {
        [Required]
        public int UsuarioId { get; set; }

        [Required]
        public int CategoriaId { get; set; }
    }
}
