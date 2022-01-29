using ApiPeliculas.Dominio.Entidades;
using ApiPeliculas.Repository.DTO.ActorPeliculaDto;
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
        /* Mapeando tambien la data de generos */
        public static List<GeneroDto> MapPeliculasGenerosDetalle ( Pelicula pelicula , PeliculaDetalleDto peliculaDetalleDto)
        {
            var resultado = new List<GeneroDto>();
            if(pelicula.PeliculasGeneros == null) { return resultado; }

            foreach(var generoPelicula in pelicula.PeliculasGeneros)
            {
                resultado.Add(new GeneroDto() { Id = generoPelicula.GeneroId, Nombre = generoPelicula.Genero.Nombre });
            }
            return resultado;
        }

        public static List<ActorPeliculaDetalleDto> MapPeliculasActoresDetalle(Dominio.Entidades.Pelicula pelicula , PeliculaDetalleDto peliculaDetalleDto)
        {
            var resultado  = new List<ActorPeliculaDetalleDto>();
            if(pelicula.PeliculasActores == null) { return resultado; }

            foreach(var peliculaActores in pelicula.PeliculasActores)
            {
                resultado.Add(new ActorPeliculaDetalleDto() { ActorId = peliculaActores.ActorId, NombrePersona = peliculaActores.Actor.Nombre, Personaje = peliculaActores.Personaje });
            }
            return resultado;
        }
    }
}
