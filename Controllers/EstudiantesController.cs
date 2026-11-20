using Lab05_Ederson_Vilca.Models;
using Lab05_Ederson_Vilca.Services;
using Microsoft.AspNetCore.Mvc;

namespace Lab05_Ederson_Vilca.Controllers
{
    [ApiController]
    [Route("api/Ederson-Vilca/[controller]")]
    public class EstudiantesController : ControllerBase
    {
        private readonly IEstudianteService _estudianteService;

        public EstudiantesController(IEstudianteService estudianteService)
        {
            _estudianteService = estudianteService;
        }

        [HttpGet]
        public IActionResult GetAll() => Ok(_estudianteService.GetAll());

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var estudiante = _estudianteService.GetById(id);
            if (estudiante == null) return NotFound();
            return Ok(estudiante);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Estudiante estudiante)
        {
            await _estudianteService.CreateAsync(estudiante);
            return Ok(new { message = "Estudiante registrado con éxito" });
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Estudiante estudiante)
        {
            await _estudianteService.UpdateAsync(estudiante);
            return Ok(new { message = "Estudiante actualizado con éxito" });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _estudianteService.DeleteAsync(id);
            return Ok(new { message = "Estudiante eliminado con éxito" });
        }
    }
}