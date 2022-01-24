using System.ComponentModel.DataAnnotations;

namespace ApiPeliculas.Dominio.Entidades
{
    public class Actor : Entity
    {
        [Required]
        [StringLength(120)]
        public string Nombre { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Foto { get; set; }

        public List<PeliculasActores> PeliculasActores { get; set; }
    }
}
