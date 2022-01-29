using ApiPeliculas.Dominio.Entidades;
using ApiPeliculas.Repository.DTO.SalaDeCine;
using ApiPeliculas.Repository.Repository;
using ApiPeliculas.Repository.Services;
using ApiPeliculas.Repository.Services.SalasDeCineService;
using Microsoft.AspNetCore.Mvc;

namespace ApiPeliculas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class SalaDeCineController : ControllerBase
    {
        private readonly IService<SalaDeCine, SalaDeCineDto> _service;
        private readonly IService<SalaDeCine, SalaDeCineCreacionDto> _serviceCreacion;
        private readonly ISalaDeCineService _salaDeCineService;

        public SalaDeCineController(IService<SalaDeCine, SalaDeCineDto> service, IService<SalaDeCine, SalaDeCineCreacionDto> serviceCreacion, ISalaDeCineService salaDeCineService)
        {
            _service = service;
            _serviceCreacion = serviceCreacion;
            _salaDeCineService = salaDeCineService;
        }
        [HttpGet]
        public async Task<ActionResult<List<SalaDeCineDto>>> Get()
        {
            return _service.Get();
        }
        [HttpGet("{id:int}", Name ="obtenerSalaDeCine")]
        public async Task<ActionResult<SalaDeCineDto>> Get(int id)
        {
            return await _service.GetById(id);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] SalaDeCineCreacionDto salaDeCineCreacionDto)
        {
            var entidad = await _serviceCreacion.Add(salaDeCineCreacionDto);
            return new CreatedAtRouteResult("obtenerSalaDeCine", new { id = entidad.Id }, entidad);

        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id , [FromBody] SalaDeCineCreacionDto salaDeCineCreacionDto)
        {
            salaDeCineCreacionDto.Id = id;
            await _serviceCreacion.Update(salaDeCineCreacionDto);
            return NoContent();

        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _serviceCreacion.DeleteAsync(id);
            _serviceCreacion.Save();
            return NoContent();
        }
        [HttpGet("cercanos")]
        public async Task<ActionResult<List<SalaDeCineCercanoDto>>> Cercanos([FromQuery] SalaDeCineCercanoFiltroDto filtro)
        {
            return await _salaDeCineService.ObtenerCinesCercanos(filtro);
        }
    }
}
