using ApiPeliculas.Dominio.Entidades;
using ApiPeliculas.Persistencia;
using ApiPeliculas.Repository.Azure;
using ApiPeliculas.Repository.DTO.ActoresDto;
using ApiPeliculas.Repository.DTO.PeliculasDto;
using ApiPeliculas.Repository.Helpers;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;

namespace ApiPeliculas.Repository.Repository.PeliculaRepository
{
    public class PeliculaRepository : Repository<Pelicula> , IPeliculaRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IAlmacenadorArchivos _almacenadorArchivos;
        private readonly IMapper _mapper;
        private readonly string contenedor = "peliculas";
        public PeliculaRepository(ApplicationDbContext context , IAlmacenadorArchivos almacenadorArchivos, IMapper mapper)
            :base(context)
        {
            _context = context;
            _almacenadorArchivos = almacenadorArchivos;
            _mapper = mapper;
        }

        public async Task<PeliculaDto> AddWithFile(PeliculaCreacionDto dto, Pelicula entity)
        {
            var movie = _mapper.Map<Pelicula>(dto);
            if(dto.Poster != null)
            {
                using(var memoryStream = new MemoryStream())
                {
                    await dto.Poster.CopyToAsync(memoryStream);
                    /* almacenarlo en arreglo de bytes */
                    var contenido = memoryStream.ToArray();
                    /* Obtener la extension del archivo */
                    var extension = Path.GetExtension(dto.Poster.FileName);
                    /* Guardando la url de la imagen en azure que devuelve el metodo */
                    movie.Poster = await _almacenadorArchivos.GuardarArchivo(contenido, extension, contenedor, dto.Poster.ContentType);
                    //var dtoObject = _mapper.Map<ActorDto>(entity);
                    //return new CreatedAtRouteResult("obtenerActor", new { id = entity.Id }, dtoObject);
                }
            }
            AsignarOrdenActores(movie);
            _context.Add(movie);
            await _context.SaveChangesAsync();
            return _mapper.Map<PeliculaDto>(movie);
        }

        /* ordenamiento de actores */
        /* el cliente va a poder enviar el orden el que quiere que salgan los actores */
        public void AsignarOrdenActores(Pelicula pelicula)
        {
            if(pelicula != null)
            {
                for (int i = 0; i < pelicula.PeliculasActores.Count; i++)
                {
                    pelicula.PeliculasActores[i].Orden = i;
                }
            }
        }

        public async Task<List<PeliculaDto>> ObtenerListaFiltrada(FiltroPeliculasDto filtroPeliculasDto, IQueryable<Pelicula> queryPelicula)
        {
            var peliculas = await queryPelicula.Paginar(filtroPeliculasDto.paginacion).ToListAsync();
            return _mapper.Map<List<PeliculaDto>>(peliculas);
        }

        public async Task<IQueryable<Pelicula>> ObtenerTodoConFiltro(FiltroPeliculasDto filtroPeliculasDto)
        {
            /* Ejecucion diferida para aplicar el filtro paso a paso */
            /* Dinamicamente aplicar filtros */
            var peliculasQueryable = _context.Peliculas.AsQueryable();
            /* si viene info en el titulo */
            if (!string.IsNullOrEmpty(filtroPeliculasDto.Titulo))
            {
                peliculasQueryable = peliculasQueryable.Where(x => x.Titulo.Contains(filtroPeliculasDto.Titulo));
            }

            /* Filtro validando si esta en cines */
            if (filtroPeliculasDto.EnCines)
            {
                peliculasQueryable = peliculasQueryable.Where(x => x.EnCines);
            }

            /* Filtro por fecha */
            if (filtroPeliculasDto.ProximosEstrenos)
            {
                var hoy = DateTime.Today;
                peliculasQueryable = peliculasQueryable.Where(x => x.FechaEstreno > hoy);
            }

            /* Filtrado por genero */
            if(filtroPeliculasDto.GeneroId != 0)
            {
                peliculasQueryable = peliculasQueryable.Where(x => x.PeliculasGeneros.Select(y => y.GeneroId).Contains(filtroPeliculasDto.GeneroId));
            }
            return peliculasQueryable;
        }

        public async Task UpdatePatch(int id, JsonPatchDocument<PeliculaPatchDto> patchDocument)
        {
            try
            {
                if (patchDocument == null)
                {
                    throw new Exception();
                }
                var entidadBd = await _context.Peliculas.FindAsync(id);
                if (entidadBd == null)
                    throw new Exception("No existe dato con ese id en la Bd");
                var entidadDto = _mapper.Map<PeliculaPatchDto>(entidadBd);

                patchDocument.ApplyTo(entidadDto);
                /* Pendiente por definir */
                //patchDocument.ApplyTo(entidadDto, ModelState);
                //var esValido = TryValidateModel(entidadDto);
                _mapper.Map(entidadDto, entidadBd);
            }
            catch (Exception e)
            {
                throw new Exception($"Ha Ocurrido un error {e.Message}");
            }

        }

        public async Task UpdateWithFile(int id, PeliculaCreacionDto dto, Pelicula entity)
        {
            var peliculaDb = await _context.Peliculas
                .Include(x=> x.PeliculasGeneros)
                .Include(x=> x.PeliculasActores)
                .FirstOrDefaultAsync(x=> x.Id == id);

            if (peliculaDb == null) throw new Exception();

            /* los campos de dto los va a mapear hacia actorDb y con eso los campos distintos van a ser actualizado*/
            /* Y cuando hagamos un savechanges, por como funciona EF, solo los campos diferentes entre dto y actorDb van a ser los actualizados por EF */

            /* Tambien tenemos que ignorar el campo foto, para que no se este actualizando en esta linea de codigo, sobre todo, en actorDb es un string y en dto es un IFormFile*/
            peliculaDb = _mapper.Map(dto, peliculaDb);

            if (dto.Poster != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await dto.Poster.CopyToAsync(memoryStream);
                    var contenido = memoryStream.ToArray();
                    var extension = Path.GetExtension(dto.Poster.FileName);
                    peliculaDb.Poster = await _almacenadorArchivos.EditarArchivo(contenido, extension, contenedor, peliculaDb.Poster, dto.Poster.ContentType);
                }
            }
            AsignarOrdenActores(peliculaDb);
            //await _context.SaveChangesAsync();
        }
    }
}
