using ApiPeliculas.Dominio.Entidades;
using ApiPeliculas.Repository.DTO.ActoresDto;
using Microsoft.AspNetCore.JsonPatch;

namespace ApiPeliculas.Repository.Services.ActoresService
{
    public interface IActorService : IService<Actor, ActorCreacionDto>
    {
        Task AddWithFile(ActorCreacionDto dto);
        Task UpdateWithFile(int id,ActorCreacionDto dto);
        Task UpdateWithPatch(int id,JsonPatchDocument<ActorPatchDto> patchDocument);
    }
}
