using ApiPeliculas.Dominio.Entidades;
using ApiPeliculas.Repository.Mapping;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiPeliculas.Repository.DTO.PeliculasDto
{
    public class PeliculaPatchDto : EntityDto, IMapFrom
    {
        [Required]
        [StringLength(300)]
        public string Titulo { get; set; }
        public bool EnCines { get; set; }
        public DateTime FechaEstreno { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Pelicula, PeliculaPatchDto>().ReverseMap();
        }
    }
}
