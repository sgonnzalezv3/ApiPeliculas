using ApiPeliculas.Dominio.Entidades;
using ApiPeliculas.Repository.DTO;
using ApiPeliculas.Repository.Repository;
using AutoMapper;

namespace ApiPeliculas.Repository.Services
{
    /* Esto es para emplear automapper con patron repository generico (TEntity es como tal la entidad y TDto es el dto) */
    public class Service<TEntity, TDto> : IService<TEntity, TDto>
        where TDto : EntityDto where TEntity : Entity
    {
        private readonly IMapper _mapper;
        private readonly IRepository<TEntity> _repository;
        public Service(IMapper mapper , IRepository<TEntity> repository)
        {
            _mapper = mapper;
            _repository = repository;
        }
        public async Task<TDto> Add(TDto dto)
        {
            var entity = _mapper.Map<TEntity>(dto);
            var entitySaved = await _repository.Add(entity);
            
            return _mapper.Map<TDto>(entitySaved);

        }

        public async Task<TDto> Add(TEntity dto)
        {
            var entitySaved = await _repository.Add(dto);

            return _mapper.Map<TDto>(entitySaved);
        }

        public Task<bool> AnyExists(int entityId)
        {
            return _repository.AnyExists(entityId);
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.Delete(id);
        }

        public List<TDto> Get()
        {
            return _repository.GetAll().Select(_mapper.Map<TDto>).ToList();
            
        }

        public async Task<TDto> GetById(int id)
        {
            var entity = await _repository.Get(id);
            return _mapper.Map<TDto>(entity);
        }

        public Task<TEntity> GetByIdEntity(int id)
        {
            return _repository.Get(id);
        }

        public async Task<List<TDto>> GetPaginado(PaginacionDto paginacionDto)
        {
            var cantidadPaginada = await _repository.GetPaginado(paginacionDto);
            
            return  _mapper.Map<List<TDto>>(cantidadPaginada);
        }

        public async Task<IQueryable<TEntity>> GetQueryable()
        {
            var cantidad = await _repository.GetQueryable();
            return cantidad;
        }

        public async Task Save()
        {
            await _repository.Save();
        }

        public async Task Update(TDto dto)
        {

            await _repository.Update(_mapper.Map<TEntity>(dto));
        }
    }
}
