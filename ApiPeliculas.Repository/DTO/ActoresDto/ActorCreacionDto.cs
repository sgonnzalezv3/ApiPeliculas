using ApiPeliculas.Dominio.Entidades;
using ApiPeliculas.Repository.Mapping;
using ApiPeliculas.Repository.Validaciones;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiPeliculas.Repository.DTO.ActoresDto
{
    public class ActorCreacionDto : EntityDto, IMapFrom
    {
        public string Nombre { get; set; }
        public DateTime FechaNacimiento { get; set; }
        /* Aqui recibimos la foto como tal */

        [TamanoArchivoValidacion(pesoMaximoMb:4)]
        [TipoArchivoValidacion(grupoTipoArchivo:GrupoTipoArchivo.Imagen)]
        public IFormFile Foto { get; set; }

        public void Mapping(Profile profile)
        {
            /* Ignorar el campo foto y Id , evitarnos el mapeo de la foto*/
            profile.CreateMap<Actor, ActorCreacionDto>().ReverseMap()
                .ForMember(x => x.Foto, options => options.Ignore())
                .ForMember(x => x.Id, options => options.Ignore());
        }
    }
}
