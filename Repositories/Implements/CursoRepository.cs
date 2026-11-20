using Lab05_Ederson_Vilca.Models;

namespace Lab05_Ederson_Vilca.Repositories.Implements
{
    public class CursoRepository : GenericRepository<Curso>, ICursoRepository
    {
        public CursoRepository(ColegioDbContext context) : base(context)
        {
        }

        public IEnumerable<Curso> GetCursosByNombre(string nombre)
        {
            return _context.Cursos
                .Where(c => c.Nombre.ToLower().Contains(nombre.ToLower()))
                .ToList();
        }
    }
}