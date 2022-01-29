using ApiPeliculas.Repository.Mapping;
using AutoMapper;
using NetTopologySuite;
using NetTopologySuite.Geometries;

namespace ApiPeliculas.Repository.DTO.SalaDeCine
{
    public class SalaDeCineDto : EntityDto, IMapFrom
    {

        public int Id
        {
            get;set;
        }
        public string Nombre { get; set; }
        public double Latitud { get; set; }
        public double Longitud { get; set; }

        public void Mapping(Profile profile)
        {
                var _geomtryFactory = NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326);

                profile.CreateMap<ApiPeliculas.Dominio.Entidades.SalaDeCine, SalaDeCineDto>()
                /* especificar un punto hacia una latitud y un punto hacia una longitud */
                .ForMember(x => x.Latitud, x => x.MapFrom(y => y.Ubicacion.Y))
                .ForMember(x => x.Longitud, x => x.MapFrom(y => y.Ubicacion.X));

                profile.CreateMap<SalaDeCineDto, ApiPeliculas.Dominio.Entidades.SalaDeCine>()
                /* mapeo de logitud y latitud hacia un punto */
                .ForMember(x => x.Ubicacion, x => x.MapFrom(y => _geomtryFactory.CreatePoint(new Coordinate(y.Longitud, y.Latitud))));

        }
    }
}
