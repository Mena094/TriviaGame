using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriviaGame.DTOs.Trivia
{
    public class UsuarioEstadisticasDTO
    {
        public int PartidasJugadas { get; set; }
        public int TotalPuntosAcumulados { get; set; }
        public int MejorPuntaje { get; set; }
    }
}
