using System.Data;
using productsrad_bc.Models;
using Dapper;
using Npgsql;


namespace productsrad_bc.Repositories{

   

    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly IConfiguration _config;

        public UsuarioRepository(IConfiguration config)
        {
            _config = config;
        }

        private IDbConnection CrearConexion()
        {
            var connectionString = _config.GetConnectionString("DefaultConnection");
            return new NpgsqlConnection(connectionString);
        }

        public async Task<Usuario?> ObtenerPorUsernameAsync(string username)
        {
            using var conexion = CrearConexion();
            var sql = "SELECT * FROM usuarios WHERE username = @username LIMIT 1";
            return await conexion.QueryFirstOrDefaultAsync<Usuario>(sql, new { username });
        }

        public async Task CrearAsync(Usuario usuario)
        {
            using var conexion = CrearConexion();
            var sql = "INSERT INTO usuarios (nombre, username, correo, password, fechacreacion, activo) VALUES (@Nombre, @Username, @Correo, @Password, @FechaCreacion, @Activo)";
            await conexion.ExecuteAsync(sql, usuario);
        }

    }
}
