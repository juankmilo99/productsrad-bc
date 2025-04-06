using productsrad_bc.Models;

namespace productsrad_bc.Repositories;

public interface IProductoRepository
{
	Task<IEnumerable<Producto>> ObtenerTodosAsync();
	Task<Producto?> ObtenerPorIdAsync(int id);
	Task<int> CrearAsync(Producto producto);
	Task<bool> ActualizarAsync(int id, Producto producto);
	Task<bool> EliminarAsync(int id);
}
