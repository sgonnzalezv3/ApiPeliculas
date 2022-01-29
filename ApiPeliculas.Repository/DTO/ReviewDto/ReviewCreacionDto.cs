using ApiPeliculas.Dominio.Entidades;
using ApiPeliculas.Repository.Mapping;
using AutoMapper;
using System.ComponentModel.DataAnnotations;

namespace ApiPeliculas.Repository.DTO.ReviewDto
{
    public class ReviewCreacionDto : EntityDto , IMapFrom
    {
        public string Comentario { get; set; }
        [Range(1,5)]
        public int Puntuacion { get; set;}

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ReviewCreacionDto,Review>();
        }
    }
}
