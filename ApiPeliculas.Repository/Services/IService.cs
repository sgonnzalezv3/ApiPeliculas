using ApiPeliculas.Repository.DTO;
using System.Linq.Expressions;

namespace ApiPeliculas.Repository.Services
{
    public interface IService<TEntity ,TDto>
    {
        Task Add(TDto dto);
        Task DeleteAsync(int id);
        List<TDto> Get();
        Task<List<TDto>> GetPaginado(PaginacionDto paginacionDto);
        Task<IQueryable<TEntity>> GetQueryable();
        Task<TDto> GetById(int id);
        Task Update(TDto dto);

        Task Save();

    }
}
