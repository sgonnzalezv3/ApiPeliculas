using ApiPeliculas.Repository.Mapping;
using AutoMapper;
using NetTopologySuite;
using NetTopologySuite.Geometries;
using System.ComponentModel.DataAnnotations;

namespace ApiPeliculas.Repository.DTO.SalaDeCine
{
    public class SalaDeCineCreacionDto : EntityDto, IMapFrom
    {
        [Required]
        [StringLength(120)]
        public string Nombre { get; set; }

        [Range(-90,90)]
        public double Latitud { get; set; }
        [Range(-180, 180)]
        public double Longitud { get; set; }

        public void Mapping(Profile profile)
        {
            var _geomtryFactory = NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326);

            profile.CreateMap<ApiPeliculas.Dominio.Entidades.SalaDeCine, SalaDeCineCreacionDto>().ReverseMap();

            profile.CreateMap<SalaDeCineCreacionDto, ApiPeliculas.Dominio.Entidades.SalaDeCine>()
            /* mapeo de logitud y latitud hacia un punto */
            .ForMember(x => x.Ubicacion, x => x.MapFrom(y => _geomtryFactory.CreatePoint(new Coordinate(y.Longitud, y.Latitud))));
        }
    }
}
