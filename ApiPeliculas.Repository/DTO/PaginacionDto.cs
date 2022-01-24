using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiPeliculas.Repository.DTO
{
    public class PaginacionDto
    {
        public int Pagina { get; set; } = 1;
        private int cantidadRegistrosPorPagina { get; set; } = 10;
        private readonly int CantidadMaximaRegistrosPagina = 50;

        /* Validacion para que cuando el cliente solicite una cantidad de registros por pagina,
        devuelva hasta maximo 50 registros por pagina
         */
        public int CantidadRegistrosPorPagina
        {
            get => cantidadRegistrosPorPagina;
            set
            {
                cantidadRegistrosPorPagina = (value > CantidadMaximaRegistrosPagina) ? CantidadMaximaRegistrosPagina : value;
            }
        }


    }
}
