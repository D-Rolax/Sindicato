﻿using Sindicato.common.Models.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WSSindicato.Models.Request;
using WSSindicato.Models.Response;

namespace Sindicato.common.Services
{
    public interface IApiService
    {
        Task<Respuesta> AddRutas(string urlBase,string servicePrefix,string controller,RutasDetailsRequest model,string TokenTipe,string accesToken);
        Task<Respuesta> GetListAsync<T>(string urlBase, string servicePrefix, string controller);
        string GetIpAdress();
        Task<Respuesta> GetTokenAsync(string urlBase, string ServicePrefix, string controller, AuthRequest request);
        Task<Respuesta> GetUserByEmail(string urlBase, string ServicePrefix, string controller, string tokenType, string accessToken, EmailRequest emailRequest);
        Task<Respuesta> GetGrupos(string urlBase, string ServicePrefix, string controller);
        Task<Respuesta> GetComunidad(string urlBase, string ServicePrefix, string controller);
        bool CheckConnection();
        Task<Respuesta> DeleteRutasAsync(string urlBase, string ServicePrefix, string controller, RutasResponse model);
    }
}
