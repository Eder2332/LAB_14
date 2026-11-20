using Lab05_Ederson_Vilca.Models;

namespace Lab05_Ederson_Vilca.Repositories
{
    public interface ICursoRepository : IGenericRepository<Curso>
    {
        IEnumerable<Curso> GetCursosByNombre(string nombre);
    }
}