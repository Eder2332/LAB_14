using Lab05_Ederson_Vilca.Models;

namespace Lab05_Ederson_Vilca.Services
{
    public interface ICursoService
    {
        IEnumerable<Curso> GetAll();
        IEnumerable<Curso> GetByNombre(string nombre);
        Curso? GetById(int id);
        Task CreateAsync(Curso curso);
        Task UpdateAsync(Curso curso);
        Task DeleteAsync(int id);
    }
}