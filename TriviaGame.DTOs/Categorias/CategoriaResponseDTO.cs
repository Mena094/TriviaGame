using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriviaGame.DTOs.Categorias
{
    public class CategoriaResponseDTO
    {
        public int IdCategoria { get; set; }
        public string Nombre { get; set; } = null!;
        public string? Descripcion { get; set; }
    }
}
