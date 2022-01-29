using ApiPeliculas.Dominio.Entidades;
using ApiPeliculas.Repository.DTO;
using ApiPeliculas.Repository.DTO.ReviewDto;

namespace ApiPeliculas.Repository.Repository.ReviewRepository
{
    public interface IReviewRepository : IRepository<Review>
    {
        Task<IQueryable<Review>> GetAllPaginado( int peliculaId, PaginacionDto paginacionDto);
        Task<List<ReviewDto>> GetAllPaginadoList( IQueryable<Review> queryable, PaginacionDto paginacionDto);
        Task<bool> ExistenDosReviewsMismoUsuario(int peliculaId , string usuarioId);
    }
}
