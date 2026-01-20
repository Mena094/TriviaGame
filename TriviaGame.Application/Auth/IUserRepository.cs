using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TriviaGame.Application.DTOs;

namespace TriviaGame.Application.Auth
{
    public interface IUserRepository
    {
        bool UsuarioExiste(string nombre);
        int RegistrarUsuario(string nombre, string passwordHash);
        UsuarioAuthDTO? ObtenerUsuarioPorNombre(string nombre);
    }
}
