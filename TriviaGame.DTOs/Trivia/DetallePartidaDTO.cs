using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriviaGame.DTOs.Trivia
{
    public class DetallePartidaDTO
    {
        public int PartidaId { get; init; }
        public int PreguntaId { get; init; }
        public int RespuestaId { get; init; }
        public bool EsCorrecta { get; init; }
    }
}
