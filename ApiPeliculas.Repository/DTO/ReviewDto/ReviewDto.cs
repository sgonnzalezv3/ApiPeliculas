
using ApiPeliculas.Dominio.Entidades;
using ApiPeliculas.Repository.Mapping;
using AutoMapper;
using System.ComponentModel.DataAnnotations;

namespace ApiPeliculas.Repository.DTO.ReviewDto
{
    public class ReviewDto : EntityDto, IMapFrom
    {
        public int Id { get; set; }
        public string Comentario { get; set; }
        [Range(1, 5)]
        public int Puntuacion { get; set; }
        public int PeliculaId { get; set; }
        public string UsuarioId { get; set; }
        public string NombreUsuario { get; set; }

        public void Mapping(Profile profile)
        {
            
            profile.CreateMap<Review, ReviewDto>()
                /* obteniendo el nombre del usuario */
                .ForMember(x => x.NombreUsuario, x => x.MapFrom(y => y.Usuario.UserName));
            profile.CreateMap<ReviewDto, Review>();
        }
    }
}
