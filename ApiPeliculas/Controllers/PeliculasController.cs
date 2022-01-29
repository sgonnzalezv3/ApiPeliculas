using ApiPeliculas.Dominio.Entidades;
using ApiPeliculas.Helpers;
using ApiPeliculas.Repository.DTO.PeliculasDto;
using ApiPeliculas.Repository.Services;
using ApiPeliculas.Repository.Services.PeliculasService;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace ApiPeliculas.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PeliculasController : ControllerBase
    {
        private readonly IService<Pelicula, PeliculaDto> _service;
        private readonly IPeliculaService _peliculaService;

        public PeliculasController(IService<Pelicula, PeliculaDto> service, IPeliculaService peliculaService)
        {
            _service = service;
            _peliculaService = peliculaService;
        }

        [HttpGet]
        public async Task<ActionResult<List<PeliculaDetalleDto>>>Get()
        {
            return await _peliculaService.ObtenerTodoDetalle();
        }

        [HttpGet("filtro")]
        public async Task<ActionResult<List<PeliculaDto>>> Filtrar([FromQuery] FiltroPeliculasDto filtroPeliculasDto)
        {

            var queryBusqueda = await _peliculaService.ObtenerTodoConFiltro(filtroPeliculasDto);
            await HttpContext.InsertarParametrosPaginacion(queryBusqueda, filtroPeliculasDto.CantidadRegistrosPorPagina);
            return await _peliculaService.ObtenerListaFiltrada(filtroPeliculasDto, queryBusqueda);

        }

        [HttpGet("{id}", Name ="obtenerPelicula")]
        public async Task<ActionResult<PeliculaDto>> GetById(int id)
        {
            return await _service.GetById(id);
        }

        [HttpPost]
        public async Task<IActionResult> Crear([FromForm] PeliculaCreacionDto pelicula)
        {
            var pelculaCreada = await _peliculaService.AddWithFile(pelicula);
            await _peliculaService.Save();
            return new CreatedAtRouteResult("obtenerPelicula", new {id = pelculaCreada.Id} , pelculaCreada);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromForm] PeliculaCreacionDto pelicula)
        {
            try
            {
                await _peliculaService.UpdateWithFile(id, pelicula);
                await _peliculaService.Save();
                return NoContent();
            }
            catch (Exception)
            {
                throw new Exception();
            }

        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdatePatch(int id, [FromBody] JsonPatchDocument<PeliculaPatchDto> patchDocument)
        {
            try
            {
                await _peliculaService.UpdateWithPatch(id, patchDocument);
                await _peliculaService.Save();
                return NoContent();
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
                return NoContent();

            }
            catch (Exception)
            {
                throw new Exception();
            }

        }
    }
}
