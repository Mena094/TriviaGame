using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriviaGame.DTOs.Trivia
{
    public class AnswerResponseDTO
    {
        public int RespuestaId { get; set; }
        public string Texto { get; set; } = null!;
    }
}
