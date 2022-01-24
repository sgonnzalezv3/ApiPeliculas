using ApiPeliculas.Dominio.Entidades;
using ApiPeliculas.Repository.DTO.PeliculasDto;

namespace ApiPeliculas.Repository.DTO
{
    public static class MapperHelpers
    {
        public static List<PeliculasGeneros> MapPeliculasGeneros(PeliculaCreacionDto peliculaCreacionDto , Pelicula pelicula)
        {
            var resultado = new List<PeliculasGeneros>();

            if(peliculaCreacionDto.GenerosId == null) { return resultado; }

            foreach(var id in peliculaCreacionDto.GenerosId)
            {
                resultado.Add(new PeliculasGeneros() { GeneroId = id });
            }
            return resultado;
        }

        public static List<PeliculasActores> MapPeliculasActores(PeliculaCreacionDto peliculaCreacionDto, Pelicula pelicula)
        {
            var resultado = new List<PeliculasActores>();

            if (peliculaCreacionDto.Actores == null) { return resultado; }

            foreach (var actor in peliculaCreacionDto.Actores)
            {
                resultado.Add(new PeliculasActores() { ActorId = actor.ActorId , Personaje = actor.Personaje});
            }
            return resultado;
        }
    }
}
