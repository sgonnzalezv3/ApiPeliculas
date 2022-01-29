using ApiPeliculas.Dominio.Entidades;
using ApiPeliculas.Repository.DTO;
using ApiPeliculas.Repository.DTO.ReviewDto;

namespace ApiPeliculas.Repository.Services.ReviewService
{
    public interface IReviewService : IService<Review, ReviewDto>
    {
        Task<IQueryable<Review>> Get(int peliculaId, PaginacionDto paginacionDto);
        Task<List<ReviewDto>> GetListPaginada(IQueryable<Review> queryable , PaginacionDto paginacionDto);
        Task<bool> ExistenDosReviewsMismoUsuario(int peliculaId, string usuarioId);

    }
}
