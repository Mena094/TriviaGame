using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriviaGame.Domain
{
    [Table("Respuestas")]
    public class Respuesta
    {
        [Key]
        public int IdRespuesta { get; set; }

        [Required]
        public int IdPregunta { get; set; }

        [Required]
        [StringLength(500)]
        public string RespuestaTexto { get; set; } = null!;

        [Required]
        public bool EsCorrecta { get; set; }

        [Required]
        public bool Estado { get; set; }

        [ForeignKey(nameof(IdPregunta))]
        public Pregunta? Pregunta { get; set; }
    }
}
