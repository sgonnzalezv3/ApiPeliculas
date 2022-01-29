using ApiPeliculas.Dominio.Entidades;
using ApiPeliculas.Repository.DTO.SalaDeCine;
using ApiPeliculas.Repository.Repository.SalaDeCineRepository;
using AutoMapper;

namespace ApiPeliculas.Repository.Services.SalasDeCineService
{
    public class SalaDeCineService : Service<SalaDeCine, SalaDeCineCercanoFiltroDto>,ISalaDeCineService
    {
        private readonly IMapper _mapper;
        private readonly ISalaDeCineRepository _salaDeCineRepository;
        public SalaDeCineService(ISalaDeCineRepository salaDeCineRepository, IMapper mapper)

            /* esto es para pasarle a la clase base lo que necesita */
            :base(mapper, salaDeCineRepository)
        {
            _salaDeCineRepository = salaDeCineRepository;
            _mapper = mapper;
        }

        public Task<List<SalaDeCineCercanoDto>> ObtenerCinesCercanos(SalaDeCineCercanoFiltroDto filtro)
        {
            return _salaDeCineRepository.ObtenerCinesCercanos(filtro);
        }
    }
}
