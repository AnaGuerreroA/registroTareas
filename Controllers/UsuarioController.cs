using BETareas.Models;
using Microsoft.AspNetCore.Mvc;

namespace BETareas.Controllers
{
    public class UsuarioController : ControllerBase
    {
        private readonly TaskContext _context;

        public UsuarioController(TaskContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("api/usuarios")]
        public IActionResult GetUsuarios([FromQuery]int limit, [FromQuery]int offset)
        {
            var usuarios = new List<Usuario>();
            //if limit and offset are 0, return all categories
            if (limit == 0 && offset == 0)
            {
                usuarios = _context.Usuarios.ToList();               
            }else  {
                usuarios = _context.Usuarios.Skip(offset).Take(limit).ToList();
            }       
            return Ok(usuarios);
        }   

        [HttpGet]
        [Route("api/usuarios/{id}")]
        public IActionResult GetUsuario(int id)
        {
            var usuario = _context.Usuarios.FirstOrDefault(u => u.usuarioId == id);
            if (usuario == null)
            {
                return NotFound();
            }
            return Ok(usuario);
        }

        [HttpPost]
        [Route("api/usuarios")]
        public IActionResult CreateUsuario([FromBody] Usuario usuario)
        {
            if (usuario == null)
            {
                return BadRequest();
            }
            _context.Usuarios.Add(usuario);
            _context.SaveChanges();
            return Ok(usuario);
        }

        [HttpPut]
        [Route("api/usuarios/{id}")]
        public IActionResult UpdateUsuario(int id, [FromBody] Usuario usuario)
        {
            if (usuario == null || usuario.usuarioId != id)
            {
                return BadRequest();
            }
            var usuarioToUpdate = _context.Usuarios.FirstOrDefault(u => u.usuarioId == id);
            if (usuarioToUpdate == null)
            {
                return NotFound();
            }
            usuarioToUpdate.nombre = usuario.nombre;
            usuarioToUpdate.apellido = usuario.apellido;
            usuarioToUpdate.email = usuario.email;
            usuarioToUpdate.password = usuario.password;
            usuarioToUpdate.rol = usuario.rol;
            _context.Usuarios.Update(usuarioToUpdate);
            _context.SaveChanges();
            return Ok(usuarioToUpdate);
        }

        [HttpDelete]
        [Route("api/usuarios/{id}")]
        public IActionResult DeleteUsuario(int id)
        {
            var usuario = _context.Usuarios.FirstOrDefault(u => u.usuarioId == id);
            if (usuario == null)
            {
                return NotFound();
            }
            _context.Usuarios.Remove(usuario);
            _context.SaveChanges();
            return Ok(usuario);
        }


        }

}