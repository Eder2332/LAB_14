using Lab05_Ederson_Vilca.Models;
using Lab05_Ederson_Vilca.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lab05_Ederson_Vilca.Controllers
{
    [ApiController]
    [Route("api/Ederson-Vilca/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        // POST-Registro de nuevos usuarios encriptados
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            var usuario = await _authService.RegisterAsync(model);
            return Ok(new { message = "Usuario registrado con éxito", username = usuario.Username, rol = usuario.Rol });
        }

        // POST-Endpoint de Login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var token = await _authService.LoginAsync(model);
            if (token == null)
            {
                return Unauthorized(new { message = "Usuario o contraseña incorrectos" });
            }
            return Ok(new { token });
        }

        // GET-publico
        [HttpGet("publico")]
        public IActionResult PublicEndpoint()
        {
            return Ok(new { message = "Este endpoint es público y accesible sin autenticación." });
        }

        // GET- para rol "Admin"
        [HttpGet("admin")]
        [Authorize(Roles = "Admin")]
        public IActionResult AdminEndpoint()
        {
            return Ok(new { message = "Bienvenido Admin. Tienes acceso exclusivo a este recurso." });
        }

        // GET-protegido para roles "Admin" o "Docente"
        [HttpGet("docentes")]
        [Authorize(Roles = "Admin,Docente")]
        public IActionResult DocenteEndpoint()
        {
            return Ok(new { message = "Acceso concedido para la sección de Docentes / Administración." });
        }

        // GET-cualquier usuario autenticado
        [HttpGet("perfil")]
        [Authorize]
        public IActionResult PerfilEndpoint()
        {
            var username = User.Identity?.Name;
            return Ok(new { message = $"Hola {username}, estas viendo tu información privada." });
        }
    }
}