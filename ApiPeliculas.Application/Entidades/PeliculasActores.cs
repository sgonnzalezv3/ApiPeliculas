﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiPeliculas.Dominio.Entidades
{
    public class PeliculasActores
    {
        public int ActorId { get; set; }
        public int PeliculaId { get; set; }
        public string Personaje { get; set; }
        /* Orden establecido, protagonista primero, villano segundo... */
        public int Orden { get; set; }
        public Actor Actor { get; set; }
        public Pelicula Pelicula { get; set; }
    }
}
