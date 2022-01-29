using ApiPeliculas.Dominio.Entidades;
using ApiPeliculas.Persistencia;
using ApiPeliculas.Repository.DTO;
using ApiPeliculas.Repository.Helpers;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ApiPeliculas.Repository.Repository
{
    public class Repository<T> : IRepository<T> where T : Entity
    {
        private readonly ApplicationDbContext _context;

        public Repository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<T> Add(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await Save();
            return entity;

        }

        public async Task<bool> AnyExists(int entityId)
        {
            return await _context.Set<T>().AnyAsync(x => x.Id == entityId);
        }

        public async Task Delete(int id)
        {
            var objectToDelete = await _context.Set<T>().FindAsync(id);
            if(objectToDelete != null)
                _context.Set<T>().Remove(objectToDelete);
        }
        public async Task<T> Get(int id)
        {
            var queryable = _context.Set<T>().AsQueryable();
            return await _context.Set<T>().FindAsync(id);
        }

        public IQueryable<T> GetAll()
        {
            var result = _context.Set<T>().AsNoTracking();
            return result;
        }

        public async Task<List<T>> GetPaginado(PaginacionDto paginacionDto)
        {
            var queryable =  _context.Set<T>().AsQueryable();
            var entidades =  queryable.Paginar(paginacionDto);
            return await entidades.ToListAsync();
        }

        public async Task<IQueryable<T>> GetQueryable()
        {
            return _context.Set<T>().AsQueryable();
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public async Task Update(T entity)
        {
            _context.Set<T>().Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }
    }
}
