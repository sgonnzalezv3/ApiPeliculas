using ApiPeliculas.Dominio;
using ApiPeliculas.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;

namespace ApiPeliculas.Persistencia
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {

        }
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Genero> Generos { get; set; }
        public DbSet<Actor> Actores { get; set; }
        public DbSet<Pelicula> Peliculas { get; set; }
        public DbSet<PeliculasActores> PeliculasActores { get; set; }
        public DbSet<PeliculasGeneros> PeliculasGeneros { get; set; }

        /* tenemos que parametrizar una llave compuesta (2 Ids) en el api fluente */
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PeliculasActores>().HasKey(x => new { x.ActorId, x.PeliculaId });
            modelBuilder.Entity<PeliculasGeneros>().HasKey(x => new { x.GeneroId, x.PeliculaId });
            base.OnModelCreating(modelBuilder);
        }


        /* Este metodo va hacer que , va atraer una cadena de conexion temporal hasta que no ejecute la aplicacion Education.Api  */
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {

            /* si el api aun no te da la cadena de conexion, seteame tu la cadena */
            if (!options.IsConfigured)
            {
                options.UseSqlServer("Server=.;database=ApiPeliculas;Trusted_Connection=True;MultipleActiveResultSets=True;");
            }
        }
    }
}
