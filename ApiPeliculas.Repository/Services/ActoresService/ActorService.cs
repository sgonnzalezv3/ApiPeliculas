using ApiPeliculas.Dominio.Entidades;
using ApiPeliculas.Repository.DTO.ActoresDto;
using ApiPeliculas.Repository.Repository;
using AutoMapper;

namespace ApiPeliculas.Repository.Services.ActoresService
{
    public class ActorService : Service<Actor, ActorDto>,IActorService
    {
        private readonly IRepository<Actor> _actorRepository;
        private readonly IMapper _mapper;
        public ActorService(IRepository<Actor> actorRepository, IMapper mapper)
            : base(mapper, actorRepository)
        {
            _actorRepository = actorRepository;
            _mapper = mapper;
        }
    }
}
