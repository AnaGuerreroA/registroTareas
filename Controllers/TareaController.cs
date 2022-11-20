using System.Net.Mime;
using BETareas.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BETareas.Controllers
{
    public class TareaController : ControllerBase
    {
         private readonly TaskContext _context;

        public TareaController(TaskContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("api/tareas")]
        public IActionResult GetTareas([FromQuery]int limit, [FromQuery]int offset)
        {
            var tareas = new List<Tarea>();
            //if limit and offset are 0, return all categories
            if (limit == 0 && offset == 0)
            {
                //tareas inner join usuario


                tareas = _context.Tareas.Include(t => t.Usuario).ToList();
            }else  {
                tareas = _context.Tareas.Include(t => t.Usuario).Skip(offset).Take(limit).ToList();
            }       
            return Ok(tareas);
        }

        [HttpGet]
        [Route("api/tareas/{id}")]
        public IActionResult GetTarea(int id)
        {
            var tarea = _context.Tareas.FirstOrDefault(t => t.tareaId == id);
            if (tarea == null)
            {
                return NotFound();
            }
            return Ok(tarea);
        }

        [HttpPost]
        [Route("api/tareas")]
        public IActionResult CreateTarea([FromBody] Tarea tarea)
        {
            if (tarea == null)
            {
                return BadRequest();
            }
            _context.Tareas.Add(tarea);
            _context.SaveChanges();
            return Ok(tarea);
        }

        [HttpPut]
        [Route("api/tareas/{id}")]
        public IActionResult UpdateTarea(int id, [FromBody] Tarea tarea)
        {
            if (tarea == null || tarea.tareaId != id)
            {
                return BadRequest();
            }
            var tareaToUpdate = _context.Tareas.FirstOrDefault(t => t.tareaId == id);
            if (tareaToUpdate == null)
            {
                return NotFound();
            }
            tareaToUpdate.nombre = tarea.nombre;
            tareaToUpdate.descripcion = tarea.descripcion;
            tareaToUpdate.estado = tarea.estado;
            tareaToUpdate.fecha = tarea.fecha;
            tareaToUpdate.usuarioId = tarea.usuarioId;
            _context.SaveChanges();
            return Ok(tareaToUpdate);
        }

        [HttpDelete]
        [Route("api/tareas/{id}")]
        public IActionResult DeleteTarea(int id)
        {
            var tarea = _context.Tareas.FirstOrDefault(t => t.tareaId == id);
            if (tarea == null)
            {
                return NotFound();
            }
            _context.Tareas.Remove(tarea);
            _context.SaveChanges();
            return Ok(tarea);
        }




    }
}