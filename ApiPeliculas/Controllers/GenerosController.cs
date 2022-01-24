using ApiPeliculas.Dominio;
using ApiPeliculas.Repository;
using ApiPeliculas.Repository.Services;
using Microsoft.AspNetCore.Mvc;

namespace ApiPeliculas.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GenerosController : ControllerBase
    {
        private readonly IService<Genero, GeneroDto> _service;
        public GenerosController(IService<Genero, GeneroDto> service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Crear(GeneroDto genero)
        {
            await _service.Add(genero);
            await _service.Save();

            return Ok();
        }
        [HttpGet]

        public async Task<ActionResult<List<GeneroDto>>> Get()
        {
            return _service.Get();

        }

        [HttpGet("{id}")]

        public async Task<ActionResult<GeneroDto>> Get(int id)
        {
            return await _service.GetById(id);

        }

        [HttpPut]
        public async Task<IActionResult> Update(GeneroDto genero)
        {
            try
            {
                await _service.Update(genero);
                await _service.Save();

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
