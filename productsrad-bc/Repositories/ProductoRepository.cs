using System.Data;
using Dapper;
using productsrad_bc.Models;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace productsrad_bc.Repositories;
public class ProductoRepository : IProductoRepository
{
    private readonly IConfiguration _config;
    

    public ProductoRepository(IConfiguration config)
    {
        _config = config;        
    }

    private IDbConnection CrearConexion()
    {
        var connectionString = _config.GetConnectionString("DefaultConnection");
        return new NpgsqlConnection(connectionString);
    }

    public async Task<IEnumerable<Producto>> ObtenerTodosAsync()
    {
        using var db = CrearConexion();
        var sql = "SELECT * FROM productos";
        return await db.QueryAsync<Producto>(sql);
    }

    public async Task<Producto?> ObtenerPorIdAsync(int id)
    {
        using var db = CrearConexion();
        var sql = "SELECT * FROM productos WHERE id = @Id";
        return await db.QueryFirstOrDefaultAsync<Producto>(sql, new { Id = id });
    }

    public async Task<int> CrearAsync(Producto producto)
    {
        using var db = CrearConexion();
        var sql = @"INSERT INTO productos (nombre, descripcion, precio, stock, fechacreacion)
                    VALUES (@Nombre, @Descripcion, @Precio, @Stock, @FechaCreacion)
                    RETURNING id";
        return await db.ExecuteScalarAsync<int>(sql, producto);
    }

    public async Task<bool> ActualizarAsync(int id, Producto producto)
    {
        using var db = CrearConexion();
        var sql = @"UPDATE productos 
                    SET nombre = @Nombre, descripcion = @Descripcion, precio = @Precio, stock = @Stock
                    WHERE id = @Id";
        var filas = await db.ExecuteAsync(sql, new { producto.Nombre, producto.Descripcion, producto.Precio, producto.Stock, Id = id });
        return filas > 0;
    }

    public async Task<bool> EliminarAsync(int id)
    {
        using var db = CrearConexion();
        var sql = "DELETE FROM productos WHERE id = @Id";
        var filas = await db.ExecuteAsync(sql, new { Id = id });
        return filas > 0;
    }
}
