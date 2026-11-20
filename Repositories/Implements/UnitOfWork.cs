using System.Collections;
using Lab05_Ederson_Vilca.Models;

namespace Lab05_Ederson_Vilca.Repositories.Implements
{
    public class UnitOfWork : IUnitOfWork
    {
        private Hashtable? _repositories;
        private readonly ColegioDbContext _context;

        public UnitOfWork(ColegioDbContext context)
        {
            _context = context;
            _repositories = new Hashtable();
        }

        public Task<int> Complete()
        {
            return _context.SaveChangesAsync();
        }

        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : class
        {
            _repositories ??= new Hashtable();

            var type = typeof(TEntity).Name;

            if (_repositories.ContainsKey(type))
            {
                return (IGenericRepository<TEntity>)_repositories[type]!;
            }

            var repositoryType = typeof(GenericRepository<>);
            var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)), _context);

            if (repositoryInstance != null)
            {
                _repositories.Add(type, repositoryInstance);
                return (IGenericRepository<TEntity>)repositoryInstance;
            }

            throw new Exception($"Could not create repository instance for type {type}");
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}