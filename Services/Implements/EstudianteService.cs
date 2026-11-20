using Lab05_Ederson_Vilca.Models;
using Lab05_Ederson_Vilca.Repositories;

namespace Lab05_Ederson_Vilca.Services.Implements
{
    public class EstudianteService : IEstudianteService
    {
        private readonly IUnitOfWork _unitOfWork;

        public EstudianteService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<Estudiante> GetAll() 
            => _unitOfWork.Repository<Estudiante>().GetAll();

        public Estudiante? GetById(int id) 
            => _unitOfWork.Repository<Estudiante>().GetById(id);

        public async Task CreateAsync(Estudiante estudiante)
        {
            await _unitOfWork.Repository<Estudiante>().AddAsync(estudiante);
            await _unitOfWork.Complete();
        }

        public async Task UpdateAsync(Estudiante estudiante)
        {
            _unitOfWork.Repository<Estudiante>().Update(estudiante);
            await _unitOfWork.Complete();
        }

        public async Task DeleteAsync(int id)
        {
            _unitOfWork.Repository<Estudiante>().Delete(id);
            await _unitOfWork.Complete();
        }
    }
}