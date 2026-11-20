using Lab05_Ederson_Vilca.Models;
using Lab05_Ederson_Vilca.Repositories;

namespace Lab05_Ederson_Vilca.Services.Implements
{
    public class CursoService : ICursoService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CursoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<Curso> GetAll() 
            => _unitOfWork.Repository<Curso>().GetAll();

        public Curso? GetById(int id) 
            => _unitOfWork.Repository<Curso>().GetById(id);

        public async Task CreateAsync(Curso curso)
        {
            await _unitOfWork.Repository<Curso>().AddAsync(curso);
            await _unitOfWork.Complete();
        }

        public async Task UpdateAsync(Curso curso)
        {
            _unitOfWork.Repository<Curso>().Update(curso);
            await _unitOfWork.Complete();
        }

        public async Task DeleteAsync(int id)
        {
            _unitOfWork.Repository<Curso>().Delete(id);
            await _unitOfWork.Complete();
        }
        public IEnumerable<Curso> GetByNombre(string nombre)
        {
           
            return _unitOfWork.Repository<Curso>()
                .Find(c => c.Nombre.ToLower().Contains(nombre.ToLower()));
        }
        
    }
}