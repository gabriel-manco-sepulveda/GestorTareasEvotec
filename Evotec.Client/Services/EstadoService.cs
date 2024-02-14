using Evotec.Shared;
using System.Net.Http.Json;

namespace Evotec.Client.Services
{
    public class EstadoService : IEstadoService
    {

        private readonly HttpClient _http;

        public EstadoService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<EstadoDTO>> Lista()
        {
            var result = await _http.GetFromJsonAsync<ResponseAPI<List<EstadoDTO>>>("api/Estado/Lista");

            if (result!.EsCorrecto)
                return result.Valor!;
            else
                throw new Exception(result.Mensaje);
        }
    }
}