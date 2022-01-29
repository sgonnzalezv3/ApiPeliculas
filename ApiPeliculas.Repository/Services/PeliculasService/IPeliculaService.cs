using ApiPeliculas.Dominio.Entidades;
using ApiPeliculas.Repository.DTO.PeliculasDto;
using Microsoft.AspNetCore.JsonPatch;

namespace ApiPeliculas.Repository.Services.PeliculasService
{
    public interface IPeliculaService : IService<Pelicula, PeliculaCreacionDto>
    {
        Task<PeliculaDto> AddWithFile(PeliculaCreacionDto dto);
        Task UpdateWithFile(int id, PeliculaCreacionDto dto);
        Task UpdateWithPatch(int id,JsonPatchDocument<PeliculaPatchDto> patchDocument);
        Task<IQueryable<Pelicula>> ObtenerTodoConFiltro(FiltroPeliculasDto filtroPeliculasDto);
        Task<List<PeliculaDto>> ObtenerListaFiltrada(FiltroPeliculasDto filtroPeliculasDto, IQueryable<Pelicula> queryPelicula);

        Task<List<PeliculaDetalleDto>> ObtenerTodoDetalle();
    }
}
