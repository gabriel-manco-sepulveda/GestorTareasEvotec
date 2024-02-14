using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Evotec.Server.Models;
using Evotec.Shared;
using Microsoft.EntityFrameworkCore;

namespace Evotec.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TareaController : ControllerBase
    {

        private readonly DbevotecContext _dbContext;

        public TareaController(DbevotecContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> Lista()
        {
            var responseApi = new ResponseAPI<List<TareaDTO>>();
            var listaTareaDTO = new List<TareaDTO>();

            try
            {
                foreach (var item in await _dbContext.Tareas.Include(d => d.IdEstadoNavigation).ToListAsync())
                {
                    listaTareaDTO.Add(new TareaDTO
                    {
                        IdTarea = item.IdTarea,
                        Descripcion = item.Descripcion,
                        IdEstado = item.IdEstado,
                        FechaInicio = item.FechaInicio,
                        Estado = new EstadoDTO
                        {
                            IdEstado = item.IdEstadoNavigation.IdEstado,
                            Nombre = item.IdEstadoNavigation.Nombre
                        }

                    });
                }

                responseApi.EsCorrecto = true;
                responseApi.Valor = listaTareaDTO;
            }
            catch (Exception ex)
            {

                responseApi.EsCorrecto = false;
                responseApi.Mensaje = ex.Message;
            }

            return Ok(responseApi);
        }

        [HttpGet]
        [Route("Buscar/{id}")]
        public async Task<IActionResult> Buscar(int id)
        {
            var responseApi = new ResponseAPI<TareaDTO>();
            var TareaDTO = new TareaDTO();

            try
            {
                var dbTarea = await _dbContext.Tareas.FirstOrDefaultAsync(x => x.IdTarea == id);

                if (dbTarea != null)
                {
                    TareaDTO.IdTarea = dbTarea.IdTarea;
                    TareaDTO.Descripcion = dbTarea.Descripcion;
                    TareaDTO.IdEstado = dbTarea.IdEstado;
                    TareaDTO.FechaInicio = dbTarea.FechaInicio;


                    responseApi.EsCorrecto = true;
                    responseApi.Valor = TareaDTO;
                }
                else
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensaje = "No encontrado";
                }

            }
            catch (Exception ex)
            {

                responseApi.EsCorrecto = false;
                responseApi.Mensaje = ex.Message;
            }

            return Ok(responseApi);
        }

        [HttpPost]
        [Route("Guardar")]
        public async Task<IActionResult> Guardar(TareaDTO tarea)
        {
            var responseApi = new ResponseAPI<int>();

            try
            {
                var dbTarea = new Tarea
                {
                    Descripcion = tarea.Descripcion,
                    IdEstado = tarea.IdEstado,
                    FechaInicio = tarea.FechaInicio,
                };

                _dbContext.Tareas.Add(dbTarea);
                await _dbContext.SaveChangesAsync();

                if (dbTarea.IdTarea != 0)
                {
                    responseApi.EsCorrecto = true;
                    responseApi.Valor = dbTarea.IdTarea;
                }
                else
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensaje = "No guardado";
                }

            }
            catch (Exception ex)
            {

                responseApi.EsCorrecto = false;
                responseApi.Mensaje = ex.Message;
            }

            return Ok(responseApi);
        }


        [HttpPut]
        [Route("Editar/{id}")]
        public async Task<IActionResult> Editar(TareaDTO tarea, int id)
        {
            var responseApi = new ResponseAPI<int>();

            try
            {

                var dbTarea = await _dbContext.Tareas.FirstOrDefaultAsync(e => e.IdTarea == id);

                if (dbTarea != null)
                {

                    dbTarea.Descripcion = tarea.Descripcion;
                    dbTarea.IdEstado = tarea.IdEstado;
                    dbTarea.FechaInicio = tarea.FechaInicio;


                    _dbContext.Tareas.Update(dbTarea);
                    await _dbContext.SaveChangesAsync();


                    responseApi.EsCorrecto = true;
                    responseApi.Valor = dbTarea.IdTarea;


                }
                else
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensaje = "Tarea no econtrada";
                }

            }
            catch (Exception ex)
            {

                responseApi.EsCorrecto = false;
                responseApi.Mensaje = ex.Message;
            }

            return Ok(responseApi);
        }


        [HttpDelete]
        [Route("Eliminar/{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var responseApi = new ResponseAPI<int>();

            try
            {

                var dbTarea = await _dbContext.Tareas.FirstOrDefaultAsync(e => e.IdTarea == id);

                if (dbTarea != null)
                {
                    _dbContext.Tareas.Remove(dbTarea);
                    await _dbContext.SaveChangesAsync();


                    responseApi.EsCorrecto = true;
                }
                else
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensaje = "Tarea no econtrada";
                }

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