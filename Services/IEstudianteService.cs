using Lab05_Ederson_Vilca.Models;

namespace Lab05_Ederson_Vilca.Services
{
    public interface IEstudianteService
    {
        IEnumerable<Estudiante> GetAll();
        Estudiante? GetById(int id);
        Task CreateAsync(Estudiante estudiante);
        Task UpdateAsync(Estudiante estudiante);
        Task DeleteAsync(int id);
    }
}