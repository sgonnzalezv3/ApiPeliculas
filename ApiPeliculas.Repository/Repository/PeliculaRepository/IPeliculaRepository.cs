using ApiPeliculas.Dominio.Entidades;
using ApiPeliculas.Repository.DTO.PeliculasDto;
using Microsoft.AspNetCore.JsonPatch;

namespace ApiPeliculas.Repository.Repository.PeliculaRepository
{
    public interface IPeliculaRepository : IRepository<Pelicula>
    {
        Task<PeliculaDto> AddWithFile(PeliculaCreacionDto dto , Pelicula entity);
        Task UpdateWithFile(int id, PeliculaCreacionDto dto , Pelicula entity);
        Task UpdatePatch(int id, JsonPatchDocument<PeliculaPatchDto> patchDocument);
        void AsignarOrdenActores(Pelicula pelicula);
        Task<IQueryable<Pelicula>> ObtenerTodoConFiltro(FiltroPeliculasDto filtroPeliculasDto);
        Task<List<PeliculaDto>> ObtenerListaFiltrada(FiltroPeliculasDto filtroPeliculasDto, IQueryable<Pelicula> queryPelicula);
        Task<List<PeliculaDetalleDto>> ObtenerTodoConFiltro();




    }
}
