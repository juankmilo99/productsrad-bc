
using Microsoft.AspNetCore.Mvc;
using productsrad_bc.Models;
using productsrad_bc.Repositories;
using productsrad_bc.Services;
using BCrypt.Net;

namespace productsrad_bc.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly JwtService _jwtService;
        private readonly IUsuarioRepository _usuarioRepo;

        public AuthController(JwtService jwtService, IUsuarioRepository usuarioRepo)
        {
            _jwtService = jwtService;
            _usuarioRepo = usuarioRepo;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var usuario = await _usuarioRepo.ObtenerPorUsernameAsync(request.Username);

            if (usuario == null || !BCrypt.Net.BCrypt.Verify(request.Password, usuario.Password))
                return Unauthorized("Credenciales inválidas");

            var token = _jwtService.GenerarToken(usuario.Username);
            return Ok(new { token });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.Password);
            var nuevoUsuario = new Usuario
            {
                Nombre = request.Nombre,
                Username = request.Username,
                Correo = request.Correo,
                Password = hashedPassword,
                FechaCreacion = DateTime.UtcNow,
                Activo = true
            };

            await _usuarioRepo.CrearAsync(nuevoUsuario);

            var token = _jwtService.GenerarToken(nuevoUsuario.Username);
            return Ok(new { token });
        }
    }
}
