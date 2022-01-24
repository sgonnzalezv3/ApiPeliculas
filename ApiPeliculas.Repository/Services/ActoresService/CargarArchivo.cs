using ApiPeliculas.Dominio.Entidades;
using ApiPeliculas.Repository.DTO.ActoresDto;
using ApiPeliculas.Repository.Repository;
using ApiPeliculas.Repository.Repository.ActorRepository;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;

namespace ApiPeliculas.Repository.Services.ActoresService
{
    internal class ActorService : Service<Actor,ActorCreacionDto>,IActorService
    {
        private readonly IMapper _mapper;
        private readonly IActorRepository _actorRepository;
        public ActorService(IActorRepository actorRepository , IMapper mapper)
            :base(mapper, actorRepository)
        {
            _actorRepository = actorRepository;
            _mapper = mapper;
        }
        public async Task AddWithFile(ActorCreacionDto dto)
        {
            var entity = _mapper.Map<Actor>(dto);
            await _actorRepository.AddWithFile(dto, entity);
        }

        public async Task UpdateWithFile(int id, ActorCreacionDto dto)
        {
            var entity = _mapper.Map<Actor>(dto);
            await _actorRepository.UpdateWithFile(id, dto, entity);
        }

        public async Task UpdateWithPatch(int id, JsonPatchDocument<ActorPatchDto> patchDocument)
        {
            await _actorRepository.UpdatePatch(id, patchDocument);
        }
    }
}
