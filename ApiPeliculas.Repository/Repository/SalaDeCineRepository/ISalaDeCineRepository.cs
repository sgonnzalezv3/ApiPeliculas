using ApiPeliculas.Dominio.Entidades;
using ApiPeliculas.Repository.DTO.SalaDeCine;

namespace ApiPeliculas.Repository.Repository.SalaDeCineRepository
{
    public interface ISalaDeCineRepository : IRepository<SalaDeCine>
    {
        Task<List<SalaDeCineCercanoDto>> ObtenerCinesCercanos(SalaDeCineCercanoFiltroDto filtro);

    }
}
