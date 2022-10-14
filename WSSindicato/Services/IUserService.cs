using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WSSindicato.Models;
using WSSindicato.Models.Request;
using WSSindicato.Models.Response;

namespace WSSindicato.Services
{
    public interface IUserService
    {
        TokenResponse Auth(AuthRequest model);
    }
}
