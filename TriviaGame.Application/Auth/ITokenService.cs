using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriviaGame.Application.Auth
{
    public interface ITokenService
    {
        string GenerateToken(int userId, string userName);
    }
}
