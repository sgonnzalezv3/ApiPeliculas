using ApiPeliculas.Dominio;
using ApiPeliculas.Repository.Repository;
using AutoMapper;

namespace ApiPeliculas.Repository.Services
{
    public class GeneroService : Service<ApiPeliculas.Dominio.Genero,GeneroDto> , IGeneroService
    {
        private readonly IRepository<ApiPeliculas.Dominio.Genero> _generoRepository;
        private readonly IMapper _mapper;
        public GeneroService(IRepository<ApiPeliculas.Dominio.Genero> generoRepository, IMapper mapper)
            :base( mapper,generoRepository)
        {
            _generoRepository = generoRepository;
            _mapper = mapper;
        }
        public async Task Add(Genero dto)
        {
            await _generoRepository.Add(dto);
        }

        public async Task DeleteAsync(int id)
        {
            await _generoRepository.Delete(id);
        }

        public  IEnumerable<GeneroDto> Get()
        {
            var listaGeneros = _generoRepository.GetAll().ToList();
            IEnumerable<GeneroDto> listaGenerosDto = _mapper.Map<List<GeneroDto>>(listaGeneros);
            return listaGenerosDto;
        }

        public async Task<GeneroDto> GetById(int id)
        {
            var genero = await _generoRepository.Get(id);
            return _mapper.Map<GeneroDto>(genero);
        }

        public async Task Update(Genero dto)
        {
            await _generoRepository.Update(dto);
        }
    }
}
