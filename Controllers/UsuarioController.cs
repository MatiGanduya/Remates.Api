using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Remates.Api.Data;
using Remates.Api.Domain.Entites;
using Remates.Api.Domain.EntitiesDTO;

namespace Remates.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly DemoDbContext _demoDbContext;

        public UsuarioController(DemoDbContext demoDbContext) => _demoDbContext = demoDbContext;

        [HttpGet]
        public ActionResult<IEnumerable<Usuario>> Get()
        {
            return _demoDbContext.Usuarios;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario?>> GetById(int id)
        {
            return await _demoDbContext.Usuarios.Where(x => x.IdUsuario == id).SingleOrDefaultAsync();
        }

        [HttpPost]
        public async Task<ActionResult> Create(UsuarioCreateDto usuarioDto)
        {
            var usuario = new Usuario
            {
                Nombre = usuarioDto.Nombre,
                Apellido = usuarioDto.Apellido,
                Direccion = usuarioDto.Direccion,
                Ciudad = usuarioDto.Ciudad,
                Email = usuarioDto.Email,
                Contrasena = usuarioDto.Contrasena
            };

            if (!ModelState.IsValid) //Valida que sean correctas las entradas
            {
                return BadRequest(ModelState);
            }

            await _demoDbContext.Usuarios.AddAsync(usuario);
            await _demoDbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = usuario.IdUsuario }, usuario);
        }

        [HttpPut]
        public async Task<ActionResult> Update(int id, Usuario usuario)
        {
            if (id != usuario.IdUsuario)
            {
                return BadRequest("El ID del usuario no coincide con el ID de la URL.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _demoDbContext.Usuarios.Update(usuario);
            await _demoDbContext.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var usuarioGetByYdResult = await GetById(id);
            if (usuarioGetByYdResult.Value is null)
                return NotFound();

            _demoDbContext.Remove(usuarioGetByYdResult.Value);
            await _demoDbContext.SaveChangesAsync();
            return Ok();
        }

    }
}
