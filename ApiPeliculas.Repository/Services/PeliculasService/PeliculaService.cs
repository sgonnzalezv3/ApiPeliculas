using ApiPeliculas.Dominio.Entidades;
using ApiPeliculas.Repository.DTO.PeliculasDto;
using ApiPeliculas.Repository.Repository.PeliculaRepository;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;

namespace ApiPeliculas.Repository.Services.PeliculasService
{
    internal class PeliculaService : Service<Pelicula,PeliculaCreacionDto>,IPeliculaService
    {
        private readonly IMapper _mapper;
        private readonly IPeliculaRepository _peliculaRepository;
        public PeliculaService(IPeliculaRepository peliculaRepository , IMapper mapper)

            /* esto es para pasarle a la clase base lo que necesita */
            :base(mapper, peliculaRepository)
        {
            _peliculaRepository = peliculaRepository;
            _mapper = mapper;
        }
        public async Task<PeliculaDto> AddWithFile(PeliculaCreacionDto dto)
        {
            var entity = _mapper.Map<Pelicula>(dto);
            return await _peliculaRepository.AddWithFile(dto, entity);
        }

        public async Task<List<PeliculaDto>> ObtenerListaFiltrada(FiltroPeliculasDto filtroPeliculasDto, IQueryable<Pelicula> queryPelicula)
        {
            return await _peliculaRepository.ObtenerListaFiltrada(filtroPeliculasDto,queryPelicula);

        }

        public async Task<IQueryable<Pelicula>> ObtenerTodoConFiltro(FiltroPeliculasDto filtroPeliculasDto)
        {
            return await _peliculaRepository.ObtenerTodoConFiltro(filtroPeliculasDto);
        }

        public async Task<List<PeliculaDetalleDto>> ObtenerTodoDetalle()
        {
            return await _peliculaRepository.ObtenerTodoConFiltro();
        }

        public async Task UpdateWithFile(int id, PeliculaCreacionDto dto)
        {
            var entity = _mapper.Map<Pelicula>(dto);
            await _peliculaRepository.UpdateWithFile(id, dto, entity);
        }

        public async Task UpdateWithPatch(int id, JsonPatchDocument<PeliculaPatchDto> patchDocument)
        {
            await _peliculaRepository.UpdatePatch(id, patchDocument);
        }
    }
}
