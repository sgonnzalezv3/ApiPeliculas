﻿
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace ApiPeliculas.Dominio.Entidades
{
    public class Review : Entity
    {
        public int Id { get; set; }
        public string Comentario { get; set; }
        [Range(1,5)]
        public int Puntuacion { get; set; }
        public int PeliculaId { get; set; }
        public Pelicula Pelicula { get; set; }
        public string UsuarioId { get; set; }
        public IdentityUser Usuario { get; set; }
    }
}
