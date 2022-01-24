using ApiPeliculas.Dominio.Entidades;
using System.ComponentModel.DataAnnotations;

namespace ApiPeliculas.Dominio
{
    public class Genero : Entity
    {
        public int Id { get; set; }
        [Required]
        [StringLength(40)]
        public string Nombre { get; set; }
        public List<PeliculasGeneros> PeliculaGeneros { get; set; }



    }
}
