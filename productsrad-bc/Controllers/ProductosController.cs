using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using productsrad_bc.Models;
using productsrad_bc.Repositories;

namespace productsrad_bc.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ProductosController : ControllerBase
{
    private readonly IProductoRepository _repo;

    public ProductosController(IProductoRepository repo)
    {
        _repo = repo;
    }

    [HttpGet]
    public async Task<IActionResult> GetTodos()
    {
        var productos = await _repo.ObtenerTodosAsync();
        return Ok(productos);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPorId(int id)
    {
        var producto = await _repo.ObtenerPorIdAsync(id);
        if (producto == null) return NotFound();
        return Ok(producto);
    }

    [HttpPost]
    public async Task<IActionResult> Crear([FromBody] Producto producto)
    {
        if (producto.Precio <= 0 || producto.Stock < 0)
            return BadRequest("El precio y el stock deben ser mayores a cero.");

        producto.FechaCreacion = DateTime.UtcNow;
        var nuevoId = await _repo.CrearAsync(producto);
        return CreatedAtAction(nameof(GetPorId), new { id = nuevoId }, producto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Actualizar(int id, [FromBody] Producto producto)
    {
        var existente = await _repo.ObtenerPorIdAsync(id);
        if (existente == null) return NotFound();

        if (producto.Precio <= 0 || producto.Stock < 0)
            return BadRequest("El precio y el stock deben ser mayores a cero.");

        var actualizado = await _repo.ActualizarAsync(id, producto);
        return actualizado ? NoContent() : StatusCode(500, "Error al actualizar el producto.");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Eliminar(int id)
    {
        var eliminado = await _repo.EliminarAsync(id);
        return eliminado ? NoContent() : NotFound();
    }
}