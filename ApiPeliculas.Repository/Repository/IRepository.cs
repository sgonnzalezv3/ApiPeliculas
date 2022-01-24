using ApiPeliculas.Dominio.Entidades;
using ApiPeliculas.Repository.DTO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ApiPeliculas.Repository.Repository
{
    public interface IRepository<T> where T: Entity
    {
        Task <T> Get(int id);
        IQueryable<T> GetAll();
        Task<List<T>> GetPaginado(PaginacionDto paginacionDto);
        Task<IQueryable<T>> GetQueryable();
        Task Add(T entity);
        Task Delete(int id);
        Task Update(T entity);

        Task Save();
    }
}
