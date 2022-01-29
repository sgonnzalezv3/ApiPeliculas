using System.ComponentModel.DataAnnotations;
using NetTopologySuite.Geometries;

namespace ApiPeliculas.Dominio.Entidades
{
    public class SalaDeCine : Entity
    {
        public int Id { get; set; }
        [Required]
        [StringLength(120)]
        public string Nombre { get; set; }
        public Point Ubicacion { get; set; }
        public List<PeliculasSalasDeCine> PeliculasSalasDeCines { get; set; }
    }
}
