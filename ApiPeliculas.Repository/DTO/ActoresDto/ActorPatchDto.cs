using ApiPeliculas.Dominio.Entidades;
using ApiPeliculas.Repository.Mapping;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiPeliculas.Repository.DTO.ActoresDto
{
    public class ActorPatchDto : EntityDto, IMapFrom
    {
        [Required]
        [StringLength(120)]
        public string Nombre { get; set; }
        public DateTime FechaNacimiento { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Actor,ActorPatchDto>().ReverseMap();
        }
    }
}
