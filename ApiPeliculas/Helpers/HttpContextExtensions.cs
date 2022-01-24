using Microsoft.EntityFrameworkCore;

namespace ApiPeliculas.Helpers
{
    /*  PARA PAGINAR SE necesita la cantidad total de registros que hay en la tabla
     para indicarle al cliente, la cantidad total de paginas a mostrar

    pero, donde va esa informacion? ya que hace parte de la metadata
    Esto debe ir en la cabecera de la respuesta http

     */
    public static class HttpContextExtensions
    {
        /* 
         HttpContext :extendemos del context, para agregarle las cabeceras http en la respuesta
        IQUERYABLE: a traves de este determinamos la cantidad total de registros en la tabla
        cantidadRegistrosPorPagina : hacer el calculo de las paginas a mostrar.
         */
        public async static Task InsertarParametrosPaginacion<T>(this HttpContext httpContext, IQueryable<T> queryable, int cantidadRegistrosPorPagina)
        {
            /* conteo de registros */
            double cantidad = await queryable.CountAsync();

            /* Calculo */
            double cantidadPaginas = Math.Ceiling(cantidad / cantidadRegistrosPorPagina);
            /* agregando a la cabecera */
            httpContext.Response.Headers.Add("cantidadPaginas", cantidadPaginas.ToString());
        }
    }
}
