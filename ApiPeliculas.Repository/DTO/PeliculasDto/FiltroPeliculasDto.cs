
namespace ApiPeliculas.Repository.DTO.PeliculasDto
{
    public class FiltroPeliculasDto
    {
        /* Paginacion */
        public int Pagina { get; set; } = 1;
        public int CantidadRegistrosPorPagina { get; set; } = 10;

        public PaginacionDto paginacion
        {
            get { return new PaginacionDto() {Pagina = Pagina, CantidadRegistrosPorPagina = CantidadRegistrosPorPagina }; }

        }
        /* El cliente va a poder filtrar por:  */
        public string Titulo { get; set; }
        public int GeneroId { get; set; }
        public bool EnCines { get; set; }
        public bool ProximosEstrenos { get; set; }

        public string CampoOrdenar { get; set; }
        public bool OrdenAscendente { get; set; } = true;
    }
}
