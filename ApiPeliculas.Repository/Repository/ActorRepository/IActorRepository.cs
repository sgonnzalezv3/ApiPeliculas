using ApiPeliculas.Dominio.Entidades;
using ApiPeliculas.Repository.DTO.ActoresDto;
using Microsoft.AspNetCore.JsonPatch;

namespace ApiPeliculas.Repository.Repository.ActorRepository
{
    public interface IActorRepository : IRepository<Actor>
    {
        Task AddWithFile(ActorCreacionDto dto , Actor entity);
        Task UpdateWithFile(int id, ActorCreacionDto dto , Actor entity);
        Task UpdatePatch(int id, JsonPatchDocument<ActorPatchDto> patchDocument);

    }
}
