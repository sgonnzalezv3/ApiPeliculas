using ApiPeliculas.Dominio.Entidades;
using ApiPeliculas.Repository.DTO.SalaDeCine;

namespace ApiPeliculas.Repository.Services.SalasDeCineService
{
    public interface ISalaDeCineService : IService<SalaDeCine, SalaDeCineCercanoFiltroDto>
    {
        Task<List<SalaDeCineCercanoDto>> ObtenerCinesCercanos(SalaDeCineCercanoFiltroDto filtro);
    }
}
