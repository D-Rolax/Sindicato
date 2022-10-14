using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WSSindicato.Models;
using WSSindicato.Models.Common;
using WSSindicato.Models.Request;
using WSSindicato.Models.Response;
using WSSindicato.Tools;

namespace WSSindicato.Services
{
    public class UserService : IUserService
    {
        private readonly AppSettings _appSettings;
        private readonly SindicatoContext _db;

        public UserService(IOptions<AppSettings> appSettings,SindicatoContext db)
        {
            _appSettings = appSettings.Value;
            _db = db;
        }
        public TokenResponse Auth(AuthRequest model)
        {
        TokenResponse userResponse = new TokenResponse();
            string spassword = Encrypt.GetSHA256(model.Password);

            var usuario = _db.Usuario.Where(d => d.Email == model.Email && d.Password == spassword).FirstOrDefault();

            if (usuario == null) return null;
            userResponse.Email = usuario.Email;
            userResponse.Token = GetToken(usuario);
            return userResponse;
        }

        private string GetToken(Usuario usuario)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var llave = Encoding.ASCII.GetBytes(_appSettings.Secreto);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                    new Claim[]
                    {
                        new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                        new Claim(ClaimTypes.Email, usuario.Email)
                    }),
                Expires = DateTime.UtcNow.AddDays(60),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(llave), SecurityAlgorithms.HmacSha256)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
