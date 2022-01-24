using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiPeliculas.Dominio.Entidades
{
    public class PeliculasGeneros
    {
        public int GeneroId { get; set; }
        public int PeliculaId { get; set; }
        public Genero Genero { get; set; }
        public Pelicula Pelicula { get; set; }

    }
}
