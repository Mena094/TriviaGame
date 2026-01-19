using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriviaGame.DTOs.Trivia
{
    public class GameResultResponseDto
    {
        public int PartidaId { get; set; }
        public int Puntaje { get; set; }
        public List<GameQuestionResultDto> Detalle { get; set; } = new();
    }
        
    public class GameQuestionResultDto
    {
        public int PreguntaId { get; set; }
        public bool EsCorrecta { get; set; }
    }
}
