using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Remates.Api.Data;
using Remates.Api.Domain.Entites;
using Remates.Api.Domain.EntitiesDTO;

namespace Remates.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly DemoDbContext _demoDbContext;

        public ProductoController(DemoDbContext demoDbContext) => _demoDbContext = demoDbContext;

        // Obtener todos los productos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Producto>>> Get()
        {
            return await _demoDbContext.Productos.ToListAsync();
        }

        // Obtener un producto por ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Producto?>> GetById(int id)
        {
            var producto = await _demoDbContext.Productos
                .Include(p => p.Usuario)
                .SingleOrDefaultAsync(p => p.IdProducto == id);

            if (producto == null)
            {
                return NotFound();
            }

            return producto;
        }

        // Crear un nuevo producto
        [HttpPost]
        public async Task<ActionResult> Create(ProductoCreateDto productoDto)
        {
            var producto = new Producto
            {
                Nombre = productoDto.Nombre,
                PrecioBase = productoDto.PrecioBase,
                Imagen = productoDto.Imagen,
                Descripcion = productoDto.Descripcion,
                Estado = productoDto.Estado,
                IdUsuario = productoDto.IdUsuario // Relaciona el producto con el usuario mediante el ID
            };
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _demoDbContext.Productos.AddAsync(producto);
            await _demoDbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = producto.IdProducto }, producto);
        }

        // Actualizar un producto existente
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, Producto producto)
        {
            if (id != producto.IdProducto)
            {
                return BadRequest("El ID del producto no coincide con el ID de la URL.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _demoDbContext.Productos.Update(producto);
            await _demoDbContext.SaveChangesAsync();
            return NoContent();
        }

        // Eliminar un producto
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var producto = await _demoDbContext.Productos.FindAsync(id);

            if (producto == null)
            {
                return NotFound();
            }

            _demoDbContext.Productos.Remove(producto);
            await _demoDbContext.SaveChangesAsync();

            return NoContent();
        }
    }
}
