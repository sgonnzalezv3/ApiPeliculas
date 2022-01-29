using ApiPeliculas.Persistencia;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;

namespace ApiPeliculas.Helpers
{
    /* Encapsulamiento de una funcionalidad o lógica que se repite que se repite */

    /* Qué son los filtros? Son etapas del ciclo de vida de una peticion http dentro del sistema de una aplicacion MVC o ruteo
     * Esto permite ejecutar una pieza de codigo antes de que se ejecute una peticion como PUT, POST 
     
     */
                                                      //Filtro de recurso
    public class PeliculaExisteAttribute : Attribute, IAsyncResourceFilter
    {
        private readonly ApplicationDbContext _dbContext;

        public PeliculaExisteAttribute(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task OnResourceExecutionAsync(ResourceExecutingContext context, ResourceExecutionDelegate next)
        {
            /* Obtener el valor de Pelicula Id */
                                                    /* peliculaId Viene de la ruta del controlador Review "api/peliculas/{peliculaId:int}/review" */
            var peliculaIdDb = context.HttpContext.Request.RouteValues["peliculaId"];

            if (peliculaIdDb == null)
                return;
            var peliculaId = int.Parse(peliculaIdDb.ToString());

            // ESTO SE PUEDE MEJORAR PONIENDOLO EN EL REPOSITORY :TODO
            var existePelicula = await _dbContext.Peliculas.AnyAsync(x => x.Id == peliculaId);

            if (!existePelicula)
            {
                /* Si se cumple esto, se corta el ciclo de vida de la peticion http, ni siquiera ejecutando la accion en el caso de que no exista la pelicula */
                context.Result = new NotFoundResult();
            }
            else
            {
                //next quiere decir que siga con el ciclo de vida de la peticion http
                await next();
            }

        }
    }
}
