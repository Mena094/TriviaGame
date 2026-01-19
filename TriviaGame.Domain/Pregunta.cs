using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriviaGame.Domain
{
    [Table("Preguntas")]
    public class Pregunta
    {
        [Key]
        public int IdPregunta { get; set; }

        [Required]
        public int CategoriaId { get; set; }

        [Required]
        [StringLength(500)]
        public string TextoPregunta { get; set; } = null!;

        [Required]
        public bool Estado { get; set; }

        [ForeignKey(nameof(CategoriaId))]
        public Categoria? Categoria { get; set; }
    }
}
