using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriviaGame.DTOs.Trivia
{
    public class QuestionResponseDTO
    {
        public int PreguntaId { get; set; }
        public string Pregunta { get; set; } = null!;
        public List<AnswerResponseDTO> Respuestas { get; set; } = new();
    }
}
