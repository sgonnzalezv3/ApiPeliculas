using ApiPeliculas.Dominio.Entidades;
using ApiPeliculas.Persistencia;
using ApiPeliculas.Repository.DTO;
using ApiPeliculas.Repository.DTO.ReviewDto;
using ApiPeliculas.Repository.Helpers;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace ApiPeliculas.Repository.Repository.ReviewRepository
{
    public class ReviewRepository : Repository<Review>, IReviewRepository
    {
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;
        public ReviewRepository(IMapper mapper , ApplicationDbContext context)
            :base(context)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<bool> ExistenDosReviewsMismoUsuario(int peliculaId, string usuarioId)
        {
            return  await _context.Reviews.AnyAsync(x => x.PeliculaId == peliculaId && x.UsuarioId == usuarioId);

        }

        public async Task<IQueryable<Review>> GetAllPaginado(int peliculaId, PaginacionDto paginacionDto)
        {
            var queryable = _context.Reviews.Include(x => x.Usuario).AsQueryable();
            queryable = queryable.Where(x=> x.PeliculaId == peliculaId);
            return  queryable;
        }

        public async Task<List<ReviewDto>> GetAllPaginadoList(IQueryable<Review> queryable, PaginacionDto paginacionDto)
        {
            var reviews = await queryable.Paginar(paginacionDto).ToListAsync();
            return _mapper.Map<List<ReviewDto>>(reviews);
        }
    }
}
