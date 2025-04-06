using productsrad_bc.Models;

namespace productsrad_bc.Repositories
{
    public interface IUsuarioRepository
    {
        Task<Usuario?> ObtenerPorUsernameAsync(string username);

        Task CrearAsync(Usuario usuario);

    }
}
