using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriviaGame.Domain
{
    [Table("DetallePartida")]
    public class DetallePartida
    {
        [Key]
        public int IdDetalle { get; set; }

        [Required]
        public int PartidaId { get; set; }

        [Required]
        public int PreguntaId { get; set; }

        [Required]
        public int RespuestaId { get; set; }

        [Required]
        public bool EsCorrecta { get; set; }

        [ForeignKey(nameof(PartidaId))]
        public Partida? Partida { get; set; }

        [ForeignKey(nameof(PreguntaId))]
        public Pregunta? Pregunta { get; set; }

        [ForeignKey(nameof(RespuestaId))]
        public Respuesta? Respuesta { get; set; }
    }

}
