using ApiPeliculas.Dominio.Entidades;
using ApiPeliculas.Helpers;
using ApiPeliculas.Repository.DTO;
using ApiPeliculas.Repository.DTO.ReviewDto;
using ApiPeliculas.Repository.Services.ReviewService;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ApiPeliculas.Controllers
{
    [ApiController]

    // api/peliculas/3/reviews      Usamos esta para identificar a Review como hijo de la entidad de peliculas
    [Route("api/peliculas/{peliculaId:int}/review")]
    /* Con esto se aplica el filtro de manera automatica */
    [ServiceFilter(typeof(PeliculaExisteAttribute))]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _reviewService;
        private readonly IMapper _mapper;
        public ReviewController(IReviewService reviewService, IMapper mapper)
        {
            _reviewService = reviewService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<ReviewDto>>> Get (int peliculaId , [FromQuery] PaginacionDto paginacionDto)
        {
            var queryReview = await _reviewService.Get(peliculaId, paginacionDto);
            await HttpContext.InsertarParametrosPaginacion(queryReview, paginacionDto.CantidadRegistrosPorPagina);
            return await _reviewService.GetListPaginada(queryReview,paginacionDto);
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> Post(int peliculaId , [FromBody] ReviewCreacionDto reviewCreacionDto)
        {
            /* ESTO YA NO ES NECESARIO POR EL ATTRIBUTE
            var existePelicula = await _reviewService.AnyExists(peliculaId);
            if (!existePelicula)
                return NotFound();
            */

            // En reviewCreacionDto no tenemos un atributo de UsuarioId, ya que es una mala practica
            // no se debe permitir al cliente indicar a traves de de un simple valor quien es.
            // Esa información debe venir del JWT
                                                        /* Esto viene de cuentas controller */
            var userId = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;

            // validar que no existan dos reviews
             var reviewExiste = await _reviewService.ExistenDosReviewsMismoUsuario(peliculaId, userId);

            if (reviewExiste)
                return BadRequest("El usuario ya ha escrito una review para esta pelicula");
 
            var review = _mapper.Map<Review>(reviewCreacionDto);
            review.PeliculaId = peliculaId;
            review.UsuarioId = userId;
            await _reviewService.Add(review);
            await _reviewService.Save();
            return NoContent();
        }

        [HttpPut("{reviewId:int}")]

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> Put(int peliculaId, int reviewId, [FromBody] ReviewCreacionDto reviewCreacionDto)
        {
            /*
            var existePelicula = await _reviewService.AnyExists(peliculaId);
            if (!existePelicula)
                return NotFound();
            */
            var reviewDb = await _reviewService.GetByIdEntity(reviewId);

            if(reviewDb == null) { return NotFound(); }

            var usuarioId = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
            
            /* si el usuario actual, no es el mismo que escribio el review, no se le va a permitir editar */
            /* solo el usuario quien creo ese comentario, puede editarlo */
            if(reviewDb.UsuarioId != usuarioId) { return BadRequest("No tienes permisos para editar esta reseña"); }


            reviewDb = _mapper.Map(reviewCreacionDto, reviewDb);
            reviewDb.Id = reviewId;
            await _reviewService.Save();

            return NoContent();
        }

        [HttpDelete("{reviewId:int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> Delete(int reviewId)
        {

            var reviewDb = await _reviewService.GetByIdEntity(reviewId);

            if (reviewDb == null) { return NotFound(); }

            var usuarioId = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;

            /* si el usuario actual, no es el mismo que escribio el review, no se le va a permitir borrar */
            /* solo el usuario quien creo ese comentario, puede borrarlos */
            if (reviewDb.UsuarioId != usuarioId) { return Forbid(); }

            await _reviewService.DeleteAsync(reviewId);

            await _reviewService.Save();
            return NoContent();

        }


    }
}
