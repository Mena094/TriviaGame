using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriviaGame.DTOs.Trivia
{
    public  class QuestionResponseDTO
    {
        public int PreguntaId { get; init; }
        public string Pregunta { get; init; } = string.Empty;
        public List<AnswerResponseDTO> Respuestas { get; init; } = new();
    }

}
