using ApiPeliculas.Dominio.Entidades;
using ApiPeliculas.Repository.DTO.ActorPeliculaDto;
using ApiPeliculas.Repository.Mapping;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiPeliculas.Repository.DTO.PeliculasDto
{
    public class PeliculaDetalleDto : EntityDto,IMapFrom
    {
        public string Titulo { get; set; }
        public bool EnCines { get; set; }
        public DateTime FechaEstreno { get; set; }
        public string Poster { get; set; }
        public List<GeneroDto> Generos { get; set; }
        public List<ActorPeliculaDetalleDto> Actores { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Pelicula, PeliculaDetalleDto>()
                /* Mapeo personalizado, agregando detalle de los generos y actores a la pelicula */
                .ForMember(x => x.Generos, options => options.MapFrom(MapperHelpers.MapPeliculasGenerosDetalle))
                .ForMember(x => x.Actores, options => options.MapFrom(MapperHelpers.MapPeliculasActoresDetalle));

        }
    }
}
