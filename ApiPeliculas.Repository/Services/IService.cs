using ApiPeliculas.Repository.DTO;
using System.Linq.Expressions;

namespace ApiPeliculas.Repository.Services
{
    public interface IService<TEntity ,TDto>
    {
        Task<TDto> Add(TDto dto);
        Task<TDto> Add(TEntity dto);
        Task DeleteAsync(int id);
        List<TDto> Get();
        Task<List<TDto>> GetPaginado(PaginacionDto paginacionDto);
        Task<IQueryable<TEntity>> GetQueryable();
        Task<TDto> GetById(int id);
        Task<TEntity> GetByIdEntity(int id);
        Task Update(TDto dto);
        Task<bool> AnyExists(int entityId);
        Task Save();

    }
}
