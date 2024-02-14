using Evotec.Shared;

namespace Evotec.Client.Services
{
    public interface IEstadoService
    {
        Task<List<EstadoDTO>> Lista();
    }
}