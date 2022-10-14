using Newtonsoft.Json;
using Sindicato.common.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using WSSindicato.Models.Request;
using WSSindicato.Models.Response;

namespace Sindicato.common.Services
{
    public class ApiService : IApiService
    {
        public async Task<Respuesta> GetComunidad(string urlBase, string ServicePrefix, string controller)
        {
            try
            {
                HttpClient client = new HttpClient
                {
                    BaseAddress = new Uri(urlBase)
                };
                string url = $"{ServicePrefix}{controller}";
                HttpResponseMessage response = await client.GetAsync(url);
                string result = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                {
                    return new Respuesta
                    {
                        Exito = 0,
                        Mensaje = result
                    };
                }
                ComunidadResponse grupoResponse = JsonConvert.DeserializeObject<ComunidadResponse>(result);
                return new Respuesta
                {
                    Exito = 1,
                    Data = grupoResponse.Data
                };

            }
            catch (Exception ex)
            {
                return new Respuesta
                {
                    Exito = 0,
                    Mensaje = ex.Message
                };
            }
        }

        public async Task<Respuesta> GetGrupos(string urlBase, string ServicePrefix, string controller)
        {
            try
            {
                HttpClient client = new HttpClient
                {
                    BaseAddress = new Uri(urlBase)
                };
                string url = $"{ServicePrefix}{controller}";
                HttpResponseMessage response = await client.GetAsync(url);
                string result = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                {
                    return new Respuesta
                    {
                        Exito = 0,
                        Mensaje = result
                    };
                }
                GrupoResponse grupoResponse = JsonConvert.DeserializeObject<GrupoResponse>(result);
                return new Respuesta
                {
                    Exito=1,
                    Data=grupoResponse.Data
                };

            }
            catch (Exception ex)
            {
                return new Respuesta
                {
                    Exito=0,
                    Mensaje=ex.Message
                };
            }
        }

        public string GetIpAdress()
        {
            try
            {
                var IpAddress = Dns.GetHostAddresses(Dns.GetHostName()).FirstOrDefault();
                return IpAddress.ToString();
            }
            catch (Exception)
            {
                return "No se puso obtner la direccion ip";
            }
        }

        public async Task<Respuesta> GetListRutasAsync<T>(string urlBase, string servicePrefix, string controller)
        {
            try
            {
                HttpClient cliente = new HttpClient
                {
                    BaseAddress = new Uri(urlBase),
                };
                string url = $"{servicePrefix}{controller}";
                HttpResponseMessage response = await cliente.GetAsync(url);
                string result = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                {
                    return new Respuesta
                    {
                        Exito = 0,
                        Mensaje = result
                    };
                }
                List<T> list = JsonConvert.DeserializeObject<List<T>>(result);
                return new Respuesta
                {
                    Exito = 1,
                    Data = list
                };
            }
            catch (Exception ex)
            {
                return new Respuesta
                {
                    Exito = 0,
                    Mensaje=ex.Message
                };
            }
        }

        public async Task<Respuesta> GetTokenAsync(string urlBase, string ServicePrefix, string controller, AuthRequest request)
        {
            try
            {
                string requestString = JsonConvert.SerializeObject(request);
                StringContent content = new StringContent(requestString, Encoding.UTF8, "application/json");
                HttpClient client = new HttpClient
                {
                    BaseAddress = new Uri(urlBase)
                };
                string url = $"{ServicePrefix}{controller}";
                HttpResponseMessage response = await client.PostAsync(url, content);
                string result = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                {
                    return new Respuesta
                    {
                        Exito = 0,
                        Mensaje=result
                    };
                }
                LoginResponse Token = JsonConvert.DeserializeObject<LoginResponse>(result);
                return new Respuesta {
                    Exito = 1,
                    Data = Token.Data
                };
            }
            catch (Exception ex)
            {
                return new Respuesta {
                    Exito = 0,
                    Mensaje = ex.Message
                };
            }
        }

        public async Task<Respuesta> GetUserByEmail(string urlBase, string ServicePrefix, string controller, string tokenType, string accessToken, EmailRequest request)
        {
            try
            {
                string requestString = JsonConvert.SerializeObject(request);
                StringContent content = new StringContent(requestString, Encoding.UTF8, "application/json");
                HttpClient client = new HttpClient
                {
                    BaseAddress = new Uri(urlBase)
                };
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(tokenType, accessToken);
                string url = $"{ServicePrefix}{controller}";
                HttpResponseMessage response = await client.PostAsync(url, content);
                string result = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                {
                    return new Respuesta
                    {
                        Exito = 0,
                        Mensaje = result
                    };
                }
                UserResponse userResponse = JsonConvert.DeserializeObject<UserResponse>(result);
                return new Respuesta
                {
                    Exito = 1,
                    Data = userResponse.Data
                };
            }
            catch (Exception ex)
            {
                return new Respuesta
                {
                    Exito=0,
                    Mensaje=ex.Message
                };
            }
        }
    }
}
