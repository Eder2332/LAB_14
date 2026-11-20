using System.Linq.Expressions;
using Lab05_Ederson_Vilca.Models;
using Microsoft.EntityFrameworkCore;

namespace Lab05_Ederson_Vilca.Repositories.Implements
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        protected readonly ColegioDbContext _context;

        public GenericRepository(ColegioDbContext context)
        {
            _context = context;
        }

        public IEnumerable<TEntity> GetAll() => _context.Set<TEntity>().ToList();

        public TEntity? GetById(int id) => _context.Set<TEntity>().Find(id);

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate) 
            => _context.Set<TEntity>().Where(predicate).ToList();

        public void Add(TEntity entity) => _context.Set<TEntity>().Add(entity);

        public async Task AddAsync(TEntity entity) => await _context.Set<TEntity>().AddAsync(entity);

        public void Update(TEntity entity) => _context.Set<TEntity>().Update(entity);

        public void Delete(int id)
        {
            var entity = GetById(id);
            if (entity != null)
            {
                _context.Set<TEntity>().Remove(entity);
            }
        }
    }
}