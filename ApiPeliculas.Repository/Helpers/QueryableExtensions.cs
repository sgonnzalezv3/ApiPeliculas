using ApiPeliculas.Repository.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiPeliculas.Repository.Helpers
{
    public static class QueryableExtensions
    {

        public static IQueryable<T> Paginar<T>(this IQueryable<T> queryable , PaginacionDto paginacionDto)
        {
            return queryable
                .Skip((paginacionDto.Pagina - 1) * paginacionDto.CantidadRegistrosPorPagina)
                .Take(paginacionDto.CantidadRegistrosPorPagina);
        }
    }
}
