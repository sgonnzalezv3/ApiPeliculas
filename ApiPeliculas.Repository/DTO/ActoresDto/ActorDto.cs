using ApiPeliculas.Dominio.Entidades;
using ApiPeliculas.Repository.Mapping;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiPeliculas.Repository.DTO.ActoresDto
{
    public class ActorDto : EntityDto, IMapFrom
    {
        public string Nombre { get; set; }
        public DateTime FechaNacimiento { get; set; }
        /*lectura : queremos enviar es la url de la foto*/
        public string Foto { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Actor, ActorDto>().ReverseMap();
        }
    }
}
