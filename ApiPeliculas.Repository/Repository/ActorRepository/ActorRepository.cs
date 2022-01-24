using ApiPeliculas.Dominio.Entidades;
using ApiPeliculas.Persistencia;
using ApiPeliculas.Repository.Azure;
using ApiPeliculas.Repository.DTO.ActoresDto;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;

namespace ApiPeliculas.Repository.Repository.ActorRepository
{
    public class ActorRepository : Repository<Actor> , IActorRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IAlmacenadorArchivos _almacenadorArchivos;
        private readonly IMapper _mapper;
        private readonly string contenedor = "actores";
        public ActorRepository(ApplicationDbContext context , IAlmacenadorArchivos almacenadorArchivos, IMapper mapper)
            :base(context)
        {
            _context = context;
            _almacenadorArchivos = almacenadorArchivos;
            _mapper = mapper;
        }

        public async Task AddWithFile(ActorCreacionDto dto, Actor entity)
        {
            if(dto.Foto != null)
            {
                using(var memoryStream = new MemoryStream())
                {
                    await dto.Foto.CopyToAsync(memoryStream);
                    /* almacenarlo en arreglo de bytes */
                    var contenido = memoryStream.ToArray();
                    /* Obtener la extension del archivo */
                    var extension = Path.GetExtension(dto.Foto.FileName);
                    /* Guardando la url de la imagen en azure que devuelve el metodo */
                    entity.Foto = await _almacenadorArchivos.GuardarArchivo(contenido, extension, contenedor, dto.Foto.ContentType);
                    //var dtoObject = _mapper.Map<ActorDto>(entity);
                    //return new CreatedAtRouteResult("obtenerActor", new { id = entity.Id }, dtoObject);
                }
            }
            _context.Add(entity);
        }

        public async Task UpdatePatch(int id, JsonPatchDocument<ActorPatchDto> patchDocument)
        {
            try
            {
                if (patchDocument == null)
                {
                    throw new Exception();
                }
                var entidadBd = await _context.Actores.FindAsync(id);
                if (entidadBd == null)
                    throw new Exception("No existe dato con ese id en la Bd");
                var entidadDto = _mapper.Map<ActorPatchDto>(entidadBd);

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

        public async Task UpdateWithFile(int id, ActorCreacionDto dto, Actor entity)
        {
            var actorDb = await _context.Actores.FindAsync(id);

            if (actorDb == null) throw new Exception();

            /* los campos de dto los va a mapear hacia actorDb y con eso los campos distintos van a ser actualizado*/
            /* Y cuando hagamos un savechanges, por como funciona EF, solo los campos diferentes entre dto y actorDb van a ser los actualizados por EF */
            
            /* Tambien tenemos que ignorar el campo foto, para que no se este actualizando en esta linea de codigo, sobre todo, en actorDb es un string y en dto es un IFormFile*/
            actorDb = _mapper.Map(dto, actorDb);

            if (dto.Foto != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await dto.Foto.CopyToAsync(memoryStream);
                    var contenido = memoryStream.ToArray();
                    var extension = Path.GetExtension(dto.Foto.FileName);
                    actorDb.Foto = await _almacenadorArchivos.EditarArchivo(contenido, extension, contenedor,actorDb.Foto, dto.Foto.ContentType);
                }
            }

            //await _context.SaveChangesAsync();
        }
    }
}
