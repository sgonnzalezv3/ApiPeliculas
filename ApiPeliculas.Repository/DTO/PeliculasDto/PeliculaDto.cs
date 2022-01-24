using ApiPeliculas.Dominio.Entidades;
using ApiPeliculas.Repository.Mapping;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiPeliculas.Repository.DTO.PeliculasDto
{
    public class PeliculaDto : EntityDto, IMapFrom
    {

        public string Titulo { get; set; }
        public bool EnCines { get; set; }
        public DateTime FechaEstreno { get; set; }
        public string Poster { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Pelicula, PeliculaDto>().ReverseMap();
        }
    }
}
