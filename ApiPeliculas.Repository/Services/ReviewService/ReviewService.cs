using ApiPeliculas.Dominio.Entidades;
using ApiPeliculas.Repository.DTO;
using ApiPeliculas.Repository.DTO.ReviewDto;
using ApiPeliculas.Repository.Repository;
using ApiPeliculas.Repository.Repository.ReviewRepository;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiPeliculas.Repository.Services.ReviewService
{
    public class ReviewService : Service<Review, ReviewDto>, IReviewService
    {
        private readonly IMapper _mapper;
        private readonly IReviewRepository _repository;
        public ReviewService(IMapper mapper , IReviewRepository repository)
        :base(mapper,repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<bool> ExistenDosReviewsMismoUsuario(int peliculaId, string usuarioId)
        {
            return await _repository.ExistenDosReviewsMismoUsuario(peliculaId, usuarioId);
        }

        public async Task<IQueryable<Review>> Get(int peliculaId, PaginacionDto paginacionDto)
        {
            return await _repository.GetAllPaginado(peliculaId,paginacionDto);
        }

        public async Task<List<ReviewDto>> GetListPaginada(IQueryable<Review> queryable, PaginacionDto paginacionDto)
        {
            return await _repository.GetAllPaginadoList(queryable, paginacionDto);
        }
    }
}
