using ApiPeliculas.Dominio.Entidades;
using ApiPeliculas.Helpers;
using ApiPeliculas.Repository.DTO;
using ApiPeliculas.Repository.DTO.ActoresDto;
using ApiPeliculas.Repository.Services;
using ApiPeliculas.Repository.Services.ActoresService;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace ApiPeliculas.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ActoresController : ControllerBase
    {
        private readonly IService<Actor, ActorDto> _service;
        private readonly IActorService _serviceCreacion;
        public ActoresController(IService<Actor, ActorDto> service, IActorService serviceCreacion)
        {
            _service = service;
            _serviceCreacion = serviceCreacion;
        }

        [HttpPost]
        public async Task<IActionResult> Crear([FromForm] ActorCreacionDto actor)
        {
            await _serviceCreacion.AddWithFile(actor);
            await _serviceCreacion.Save();

            return Ok();
        }
        [HttpGet]
        // Get sin paginar 
        /*
        public async Task<ActionResult<List<ActorDto>>> Get()
        {
            return _service.Get();

        }
        */

        //Get Paginado
        public async Task<ActionResult<List<ActorDto>>> Get([FromQuery] PaginacionDto paginacionDto)
        {
            var cantidad = await _service.GetQueryable();
            /* Agregado en la cabecera de la respuesta la cantidad de paginas de la busqueda del usuario */
            await HttpContext.InsertarParametrosPaginacion(cantidad, paginacionDto.CantidadRegistrosPorPagina);
            return await _service.GetPaginado(paginacionDto);
        }

        [HttpGet("{id}")]

        public async Task<ActionResult<ActorDto>> Get(int id)
        {
            return await _service.GetById(id);

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromForm] ActorCreacionDto genero)
        {
            try
            {
                await _serviceCreacion.UpdateWithFile(id,genero);
                await _serviceCreacion.Save();
                return Ok();
            }
            catch (Exception)
            {
                throw new Exception();
            }

        }
        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdatePatch(int id, [FromBody] JsonPatchDocument<ActorPatchDto> patchDocument)
        {
            try
            {
                await _serviceCreacion.UpdateWithPatch(id, patchDocument);
                await _serviceCreacion.Save();
                return Ok();
            }
            catch (Exception)
            {
                throw new Exception();
            }

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _service.DeleteAsync(id);
                await _service.Save();
                return Ok();

            }
            catch (Exception)
            {
                throw new Exception();
            }

        }
    }
}
