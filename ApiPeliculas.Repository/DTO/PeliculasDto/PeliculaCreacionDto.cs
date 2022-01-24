using ApiPeliculas.Dominio.Entidades;
using ApiPeliculas.Repository.DTO.ActorPeliculaDto;
using ApiPeliculas.Repository.Helpers;
using ApiPeliculas.Repository.Mapping;
using ApiPeliculas.Repository.Validaciones;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiPeliculas.Repository.DTO.PeliculasDto
{
    public class PeliculaCreacionDto : EntityDto, IMapFrom
    {

        [Required]
        [StringLength(300)]
        public string Titulo { get; set; }
        public bool EnCines { get; set; }
        public DateTime FechaEstreno { get; set; }

        [TamanoArchivoValidacion(pesoMaximoMb: 4)]
        [TipoArchivoValidacion(grupoTipoArchivo: GrupoTipoArchivo.Imagen)]
        public IFormFile Poster { get; set; }

        /* validacion que permite serializar en un dato una lista de int proveniente de una solictud */
        [ModelBinder(BinderType =typeof(TypeBinder<List<int>>))]
        public List<int> GenerosId { get; set; }
        [ModelBinder(BinderType = typeof(TypeBinder<List<ActorPeliculasCreacionDto>>))]
        public List<ActorPeliculasCreacionDto> Actores { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Pelicula, PeliculaCreacionDto>().ReverseMap()
                .ForMember(x => x.Poster, options => options.Ignore())
                .ForMember(x => x.Id, options => options.Ignore())
                /* Mapeo personalizado  */
                        /* mapeando listado de generos ids hacia la propiedad peliculasgeneros de Pelicula*/
                .ForMember(x => x.PeliculasGeneros, options => options.MapFrom(MapperHelpers.MapPeliculasGeneros))
                        /* mapeando listado de actores  hacia la propiedad peliculactores de Pelicula*/
                .ForMember(x => x.PeliculasActores, options => options.MapFrom(MapperHelpers.MapPeliculasActores));
        }
    }
}
