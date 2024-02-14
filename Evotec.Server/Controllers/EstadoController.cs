using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Evotec.Server.Models;
using Evotec.Shared;
using Microsoft.EntityFrameworkCore;

namespace Evotec.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstadoController : ControllerBase
    {

        private readonly DbevotecContext _dbContext;

        public EstadoController(DbevotecContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> Lista()
        {
            var responseApi = new ResponseAPI<List<EstadoDTO>>();
            var listaEstadoDTO = new List<EstadoDTO>();

            try
            {
                foreach (var item in await _dbContext.Estados.ToListAsync())
                {
                    listaEstadoDTO.Add(new EstadoDTO
                    {

                        IdEstado = item.IdEstado,
                        Nombre = item.Nombre,
                    });
                }

                responseApi.EsCorrecto = true;
                responseApi.Valor = listaEstadoDTO;
            }
            catch (Exception ex)
            {

                responseApi.EsCorrecto = false;
                responseApi.Mensaje = ex.Message;
            }

            return Ok(responseApi);
        }



    }
}