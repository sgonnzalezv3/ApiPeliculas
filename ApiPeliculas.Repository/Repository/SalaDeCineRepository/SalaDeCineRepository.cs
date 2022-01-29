using ApiPeliculas.Dominio.Entidades;
using ApiPeliculas.Persistencia;
using ApiPeliculas.Repository.DTO;
using ApiPeliculas.Repository.DTO.SalaDeCine;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;

namespace ApiPeliculas.Repository.Repository.SalaDeCineRepository
{
    public class SalaDeCineRepository : Repository<SalaDeCine>, ISalaDeCineRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly GeometryFactory _geometryFactory;
        public SalaDeCineRepository(ApplicationDbContext context,IMapper mapper , GeometryFactory geometryFactory)
            :base(context)
        {
            _context = context;
            _mapper = mapper;
            _geometryFactory = geometryFactory;
        }

        public async Task<List<SalaDeCineCercanoDto>> ObtenerCinesCercanos(SalaDeCineCercanoFiltroDto filtro)
        {
            /* Obtener el punto */
            var ubicacionUsuario = _geometryFactory.CreatePoint(new Coordinate(filtro.Longitud, filtro.Latitud));

            var salasDeCine = await _context.SalasDeCine
                /* Ordenar de manera ascendente */
                .OrderBy(x => x.Ubicacion.Distance(ubicacionUsuario))
                                      /*Está dentro de la distancia                 Distancia */
                .Where(x => x.Ubicacion.IsWithinDistance(ubicacionUsuario, filtro.DistanciaEnKm * 1000))
                /* Mapeo */
                .Select(x => new SalaDeCineCercanoDto
                {
                    Id = x.Id,
                    Nombre = x.Nombre,
                    DistanciaEnMetros = Math.Round(x.Ubicacion.Distance(ubicacionUsuario)),
                    Latitud = x.Ubicacion.Y,
                    Longitud = x.Ubicacion.X
                }).ToListAsync();
            return salasDeCine;
        }
    }
}
