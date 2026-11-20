using Lab05_Ederson_Vilca.Models;
using Lab05_Ederson_Vilca.Services;
using Microsoft.AspNetCore.Mvc;

namespace Lab05_Ederson_Vilca.Controllers
{
    [ApiController]
    [Route("api/Ederson-Vilca/[controller]")]
    public class CursosController : ControllerBase
    {
        private readonly ICursoService _cursoService;

        public CursosController(ICursoService cursoService)
        {
            _cursoService = cursoService;
        }

        [HttpGet]
        public IActionResult GetAll() => Ok(_cursoService.GetAll());

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var curso = _cursoService.GetById(id);
            if (curso == null) return NotFound();
            return Ok(curso);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Curso curso)
        {
            await _cursoService.CreateAsync(curso);
            return Ok(new { message = "Curso registrado con éxito" });
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Curso curso)
        {
            await _cursoService.UpdateAsync(curso);
            return Ok(new { message = "Curso actualizado con éxito" });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _cursoService.DeleteAsync(id);
            return Ok(new { message = "Curso eliminado con éxito" });
        }
        
        [HttpGet("buscar/{nombre}")]
        public IActionResult GetByNombre(string nombre)
        {
            var cursos = _cursoService.GetByNombre(nombre);
            return Ok(cursos);
        }
        
    }
}