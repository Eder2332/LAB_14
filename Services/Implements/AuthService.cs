using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Lab05_Ederson_Vilca.Models;
using Lab05_Ederson_Vilca.Repositories;
using Microsoft.IdentityModel.Tokens;

namespace Lab05_Ederson_Vilca.Services.Implements
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;

        public AuthService(IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
        }

        public async Task<string?> LoginAsync(LoginModel model)
        {
            var usuario = _unitOfWork.Repository<Usuario>()
                .Find(u => u.Username == model.Username)
                .FirstOrDefault();

            if (usuario == null) return null;

            // Bcrypt
            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(model.Password, usuario.PasswordHash);
            if (!isPasswordValid) return null;
            //Token JWT
            return GenerateJwtToken(usuario);
        }

        public async Task<Usuario> RegisterAsync(RegisterModel model)
        {
            // Encriptacion
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(model.Password);

            var nuevoUsuario = new Usuario
            {
                Username = model.Username,
                PasswordHash = passwordHash,
                Rol = string.IsNullOrEmpty(model.Rol) ? "User" : model.Rol
            };

            await _unitOfWork.Repository<Usuario>().AddAsync(nuevoUsuario);
            await _unitOfWork.Complete();

            return nuevoUsuario;
        }

        private string GenerateJwtToken(Usuario usuario)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.IdUsuario.ToString()),
                new Claim(ClaimTypes.Name, usuario.Username),
                new Claim(ClaimTypes.Role, usuario.Rol)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["jwt"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "TuIssuer",
                audience: "TuAudience",
                claims: claims,
                expires: DateTime.Now.AddHours(2),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}