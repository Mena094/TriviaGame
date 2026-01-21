using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TriviaGame.DTOs.Trivia;

namespace TriviaGame.Application.Trivia
{

    public interface ITriviaRepository
    {
        int CrearPartida(int usuarioId, int categoriaId);

        IEnumerable<QuestionResponseDTO> ObtenerPreguntasAleatorias(int categoriaId);

        bool EsRespuestaCorrecta(int respuestaId);

        void GuardarDetallePartida(DetallePartidaDTO detalle);

        decimal FinalizarPartida(int partidaId);
    }

}
