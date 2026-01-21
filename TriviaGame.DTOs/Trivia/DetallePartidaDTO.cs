using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriviaGame.DTOs.Trivia
{
    public class DetallePartidaDTO
    {
        public int PartidaId { get; set; }
        public int PreguntaId { get; set; }
        public int RespuestaId { get; set; }
        public bool EsCorrecta { get; set; }
    }
}
